using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironmentResourcesManager : MonoBehaviour
{
    public static EnvironmentResourcesManager Instance;
    
    [SerializeField]
    private List<GameObject> _resourcesObjects = new List<GameObject>();
    public List<GameObject> ResourcesObjects => _resourcesObjects;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadObjectsFromResources();
    }

    private void LoadObjectsFromResources()
    {
        var objects = Resources.LoadAll("Base_3D", typeof(GameObject));

        if (objects.Length <= 0)
        {
            Debug.LogError("XV ERROR: No objects from resources were loaded");
            return;
        }

        for (var i = 0; i < objects.Length; i++)
            _resourcesObjects.Add(objects[i] as GameObject);
    }

    public IEnumerator LoadObjectsFromBundle(string bundleURL)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL, 1, 0);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

            var objects = bundle.LoadAllAssets<GameObject>();
            foreach (var obj in objects)
                _resourcesObjects.Add(obj);
        }
    }

    public GameObject GetObjectByName(string name)
    {
        return _resourcesObjects.Find(x => x.name == name);;
    }
}
