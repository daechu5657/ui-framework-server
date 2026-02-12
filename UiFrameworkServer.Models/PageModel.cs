namespace UiFrameworkServer.Models
{
    public class PageModel<T>
    {
        public int Total { get; set; } = 0;
        public List<T> List { get; set; } = new List<T>();

        public PageModel() { }

        public PageModel(IQueryable<T> queryable, int skip = 0, int take = 10)
        {
            Total = queryable.Count();
            List = queryable.Skip(skip).Take(take).ToList();
        }

        public PageModel(IQueryable<T> queryable, Page page)
            : this(queryable, page.Skip, page.Take) { }
    }

    public class PageModel<T2, T> : PageModel<T>
    {
        public PageModel(
            IQueryable<T2> queryable,
            Func<T2, T> selector,
            int skip = 0,
            int take = 10
        )
        {
            Total = queryable.Count();
            List = queryable.Skip(skip).Take(take).ToList().Select(selector).ToList();
        }

        public PageModel(IQueryable<T2> queryable, Func<T2, T> selector, Page page)
            : this(queryable, selector, page.Skip, page.Take) { }
    }

    public class Page
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }

    public interface ISort
    {
        public string Column { get; set; }

        public SortType SortType { get; set; }
    }

    public enum SortType
    {
        Ascending = 1,
        Descending = -1,
    }

    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = true,
        Inherited = true
    )]
    public class SortOptionAttribute : Attribute
    {
        public string Name { get; }

        public SortOptionAttribute(string name)
        {
            Name = name;
        }
    }

    public class DateRange
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
