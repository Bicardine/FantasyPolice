using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class ResourcesUtils
    {
        public static TResource LoadOrException<TResource>(string resourcePath) where TResource : Object
        {
            var resource = Resources.Load<TResource>(resourcePath);
            if (resource is null)
                throw new NullReferenceException($"Can't find {nameof(TResource)} from {resourcePath} path.");
            
            return resource;
        }
        
        public static IEnumerable<TResource> LoadAllOrException<TResource>(string resourcePath) where TResource : Object
        {
            var resources = Resources.LoadAll<TResource>(resourcePath);
            if (resources.Length.IsLessOrEqualZero())
                throw new NullReferenceException($"Can't find any {nameof(TResource)} from {resourcePath} path.");
            
            return resources;
        }
    }
}
