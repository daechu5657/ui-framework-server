using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;

namespace UiFrameworkServer.Databases.Utils
{
    public class DictionaryRepresentationConvention : ConventionBase, IMemberMapConvention
    {
        private readonly DictionaryRepresentation _representation;

        public DictionaryRepresentationConvention(DictionaryRepresentation representation)
        {
            _representation = representation;
        }

        public void Apply(BsonMemberMap memberMap)
        {
            var serializer = memberMap.GetSerializer();
            var reconfiguredSerializer = Apply(serializer);
            if (reconfiguredSerializer != null)
            {
                memberMap.SetSerializer(reconfiguredSerializer);
            }
        }

        private IBsonSerializer? Apply(IBsonSerializer serializer)
        {
            var configurableSerializer = serializer as IDictionaryRepresentationConfigurable;
            if (configurableSerializer != null)
            {
                return configurableSerializer.WithDictionaryRepresentation(_representation);
            }

            var childConfigurableSerializer = serializer as IChildSerializerConfigurable;
            if (childConfigurableSerializer != null)
            {
                var reconfiguredChildSerializer = Apply(
                    childConfigurableSerializer.ChildSerializer
                );
                if (reconfiguredChildSerializer != null)
                {
                    return childConfigurableSerializer.WithChildSerializer(
                        reconfiguredChildSerializer
                    );
                }
            }

            return null;
        }
    }
}
