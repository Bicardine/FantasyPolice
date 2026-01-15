using System;
using UnityEngine;

namespace Utils
{
    public static class GetComponentExtensions
    {
        public static T GetComponentOrError<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
                throw new Exception($"Component {typeof(T)} from {gameObject.name} not found!");

            return component;
        }
    }
}