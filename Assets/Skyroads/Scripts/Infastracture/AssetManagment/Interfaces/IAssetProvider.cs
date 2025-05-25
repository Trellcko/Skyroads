using System.Threading.Tasks;
using Trell.Skyroads.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssetProvider : IService
{
    void CleanUp();
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path, Vector3 at, Quaternion rotation);
    GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent);
    Task<T> Load<T>(string path) where T : class;
    Task<T> Load<T>(AssetReference assetReference) where T : class;
}
