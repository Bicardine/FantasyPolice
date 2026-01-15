using System.Linq;
using Model.Data;

namespace Model.Definitions
{
    public class IdDefinition<TDefinitionType> : Definition<TDefinitionType> where TDefinitionType : IHaveId
    {
        public TDefinitionType Get(string id)
        {
            return _collection.First(data => data.Id == id);
        }

        public bool TryGet(string id, out TDefinitionType definition)
        {
            definition = _collection.FirstOrDefault(data => data.Id == id);
            
            return definition != null;
        }

        public bool Exists(string id)
        {
            return _collection.Any(data => data.Id == id);
        }
    }
}