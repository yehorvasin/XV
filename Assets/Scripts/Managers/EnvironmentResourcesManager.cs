using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironmentResourcesManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _resourcesObjects = new List<GameObject>();
    public List<GameObject> ResourcesObjects => _resourcesObjects;

    public bool isReady = false;
    
    private void Start()
    {
        LoadObjectsFromResources();
        StartCoroutine(LoadObjectsFromBundle());
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

    public IEnumerator LoadObjectsFromBundle()
    {
        XVScenesDataList bundlesData = null;
        if (PlayerPrefs.HasKey("bundles"))
        {
            var json = PlayerPrefs.GetString("bundles");
            bundlesData = JsonUtility.FromJson<XVScenesDataList>(json);
        }

        if (bundlesData != null)
        {
            foreach (var data in bundlesData.list)
            {
                UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(data, 1, 0);
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
        }

        isReady = true;
    }

    public GameObject GetObjectByName(string name)
    {
        return _resourcesObjects.Find(x => x.name == name);;
    }

	[ContextMenu("SPAWN")]
	public void SpawnAllObjects()
	{
		var objects = Resources.LoadAll("Base_3D", typeof(GameObject));

        if (objects.Length <= 0)
        {
            Debug.LogError("XV ERROR: No objects from resources were loaded");
            return;
        }

		var objs = new List<GameObject>();
		
        for (var i = 0; i < objects.Length; i++)
            objs.Add(objects[i] as GameObject);

		foreach (var o in objs)
		{
			Instantiate(o, Vector3.zero, Quaternion.identity);
		}
	}
}
