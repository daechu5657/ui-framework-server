using System.Linq.Expressions;
using UiFrameworkServer.Models;

namespace UiFrameworkServer.Databases.Utils
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByColumn<T>(
            this IQueryable<T> source,
            string columnPath
        ) => source.OrderByColumnUsing(columnPath, nameof(Queryable.OrderBy));

        public static IOrderedQueryable<T> OrderByColumnDescending<T>(
            this IQueryable<T> source,
            string columnPath
        ) => source.OrderByColumnUsing(columnPath, nameof(Queryable.OrderByDescending));

        public static IOrderedQueryable<T> ThenByColumn<T>(
            this IOrderedQueryable<T> source,
            string columnPath
        ) => source.OrderByColumnUsing(columnPath, nameof(Queryable.ThenBy));

        public static IOrderedQueryable<T> ThenByColumnDescending<T>(
            this IOrderedQueryable<T> source,
            string columnPath
        ) => source.OrderByColumnUsing(columnPath, nameof(Queryable.ThenByDescending));

        private static IOrderedQueryable<T> OrderByColumnUsing<T>(
            this IQueryable<T> source,
            string columnPath,
            string method
        )
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = columnPath
                .Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(
                typeof(Queryable),
                method,
                new[] { parameter.Type, member.Type },
                source.Expression,
                Expression.Quote(keySelector)
            );

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        public static IOrderedQueryable<T> ApplyOrder<T>(
            this IQueryable<T> source,
            ISort? sort,
            SortType defaultSortType = SortType.Descending,
            string defaultCoulmn = "CreatedTime"
        )
        {
            var column = sort?.Column ?? defaultCoulmn;
            return (sort?.SortType ?? defaultSortType) == SortType.Ascending
                ? source.OrderByColumn(column)
                : source.OrderByColumnDescending(column);
        }

        public static IQueryable<T> ContainKeyword<T>(
            this IQueryable<T> source,
            IEnumerable<string> columnPaths,
            string value
        )
        {
            var parameter = Expression.Parameter(typeof(T), "item");

            var containsMethod = typeof(string).GetMethod(
                nameof(string.Contains),
                new[] { typeof(string) }
            );
            var isNullOrEmptyMethod = typeof(string).GetMethod(
                nameof(string.IsNullOrEmpty),
                new[] { typeof(string) }
            );

            if (containsMethod == null)
            {
                throw new NullReferenceException(nameof(containsMethod));
            }

            if (isNullOrEmptyMethod == null)
            {
                throw new NullReferenceException(nameof(isNullOrEmptyMethod));
            }

            Expression? item = null;
            foreach (var columnPath in columnPaths)
            {
                var member = columnPath
                    .Split('.')
                    .Aggregate((Expression)parameter, Expression.PropertyOrField);
                var isNullOrEmpty = Expression.Call(member, isNullOrEmptyMethod);
                var contains = Expression.Call(member, containsMethod, Expression.Constant(value));
                var expression = Expression.And(Expression.Not(isNullOrEmpty), contains);

                if (item == null)
                {
                    item = expression;
                }
                else
                {
                    item = Expression.Or(item, expression);
                }
            }

            if (item == null)
            {
                return source;
            }

            var methodCall = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Where),
                new[] { parameter.Type },
                source.Expression,
                Expression.Quote(Expression.Lambda(item, parameter))
            );

            return (IQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        public static IQueryable<T> ContainValues<T>(
            this IQueryable<T> source,
            string columnPath,
            List<double> values,
            double defaultValue,
            bool isNullable = true,
            bool rounded = true
        )
        {
            var parameter = Expression.Parameter(typeof(T), "item");

            var containsMethod = typeof(List<double>).GetMethod(
                nameof(List<double>.Contains),
                new[] { typeof(double) }
            );
            var roundMethod = typeof(Math).GetMethod(nameof(Math.Round), new[] { typeof(double) });

            if (containsMethod == null)
            {
                throw new NullReferenceException(nameof(containsMethod));
            }

            if (roundMethod == null)
            {
                throw new NullReferenceException(nameof(roundMethod));
            }

            Expression expression;

            if (isNullable)
            {
                var member = columnPath
                    .Split('.')
                    .Concat(new[] { nameof(Nullable<double>.Value) })
                    .Aggregate((Expression)parameter, Expression.PropertyOrField);
                if (rounded)
                {
                    member = Expression.Call(roundMethod, member);
                }

                var hasValue = columnPath
                    .Split('.')
                    .Concat(new[] { nameof(Nullable<double>.HasValue) })
                    .Aggregate((Expression)parameter, Expression.PropertyOrField);
                var contains = Expression.Call(Expression.Constant(values), containsMethod, member);
                var defaultContains = Expression.Call(
                    Expression.Constant(values),
                    containsMethod,
                    Expression.Constant(defaultValue)
                );
                var hasNotValue = Expression.Not(hasValue);

                expression = Expression.And(hasValue, contains);
                expression = Expression.Or(
                    expression,
                    Expression.And(hasNotValue, defaultContains)
                );
            }
            else
            {
                var member = columnPath
                    .Split('.')
                    .Aggregate((Expression)parameter, Expression.PropertyOrField);
                if (rounded)
                {
                    member = Expression.Call(roundMethod, member);
                }

                expression = Expression.Call(Expression.Constant(values), containsMethod, member);
            }

            var methodCall = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Where),
                new[] { parameter.Type },
                source.Expression,
                Expression.Quote(Expression.Lambda(expression, parameter))
            );

            return (IQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        public static IQueryable<T> WhereOr<T>(
            this IQueryable<T> source,
            IEnumerable<Expression<Func<T, bool>>> predicates
        )
        {
            var parameter = Expression.Parameter(typeof(T), "item");

            Expression? result = null;
            foreach (var item in predicates)
            {
                var replaced = ParameterReplacer.Replace<Func<T, bool>, Func<T, bool>>(
                    item,
                    item.Parameters.First(),
                    parameter
                );

                if (result == null)
                {
                    result = replaced.Body;
                }
                else
                {
                    result = Expression.Or(result, replaced.Body);
                }
            }

            if (result == null)
            {
                return source;
            }

            var methodCall = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Where),
                new[] { parameter.Type },
                source.Expression,
                Expression.Quote(Expression.Lambda(result, parameter))
            );

            return (IQueryable<T>)source.Provider.CreateQuery(methodCall);
        }
    }

    public static class ParameterReplacer
    {
        public static Expression<TOutput> Replace<TInput, TOutput>(
            Expression<TInput> expression,
            ParameterExpression source,
            ParameterExpression target
        )
        {
            return new ParameterReplacerVisitor<TOutput>(source, target).VisitAndConvert(
                expression
            );
        }

        private class ParameterReplacerVisitor<TOutput> : ExpressionVisitor
        {
            private readonly ParameterExpression Source;
            private readonly ParameterExpression Target;

            public ParameterReplacerVisitor(ParameterExpression source, ParameterExpression target)
            {
                Source = source;
                Target = target;
            }

            internal Expression<TOutput> VisitAndConvert<T>(Expression<T> root)
            {
                return (Expression<TOutput>)VisitLambda(root);
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                var parameters = node.Parameters.Select(a => a == Source ? a : Target).ToList();
                return Expression.Lambda<TOutput>(Visit(node.Body), parameters);
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == Source ? Target : base.VisitParameter(node);
            }
        }
    }
}
