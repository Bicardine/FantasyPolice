using UnityEngine;
using Utils;

namespace Infrastructure.AssetManagement
{
    // With Resources implementation is just a syntax sugar. But can ez implement another
    // IAssetProvider with Adressables and other.
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadAsset(string path) =>
            ResourcesUtils.LoadOrException<GameObject>(path);

        public T LoadAsset<T>(string path) where T : Object =>
            ResourcesUtils.LoadOrException<T>(path);
    }
}