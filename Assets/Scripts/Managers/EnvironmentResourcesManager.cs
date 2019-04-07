using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void LoadObjectsFromBundle(string bundlePath)
    {
        
    }

    public GameObject GetObjectByName(string name)
    {
        return _resourcesObjects.Find(x => x.name == name);;
    }
}
