using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Definitions
{
    public abstract class Definition<TDefinitionType> : ScriptableObject, IDefinition<TDefinitionType>
    {
        [SerializeField] protected TDefinitionType[] _collection;

        public IReadOnlyList<TDefinitionType> Collection => _collection;

#if UNITY_EDITOR
        public void Transit(IEnumerable<TDefinitionType> collection)
        {
            _collection = collection.ToArray();
        }
#endif
    }
}