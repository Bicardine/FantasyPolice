using UnityEngine;

namespace Infrastructure.AssetManagement
{
  public interface IAssetProvider
  {
    GameObject LoadAsset(string path);
    T LoadAsset<T>(string path) where T : Object;
  }
}