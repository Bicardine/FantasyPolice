using System.Collections.Generic;

namespace Model.Definitions
{
    public interface IDefinition<TDefinitionType>
    {
        IReadOnlyList<TDefinitionType> Collection { get; }
    }
}