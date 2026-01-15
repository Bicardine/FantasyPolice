using UnityEngine;

namespace Model.Data
{
    public interface IHaveTransform
    {
        Transform Transform { get; }
    }
}