using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadAssetBundle : MonoBehaviour
{
    public string BundleUrl;

    private EnvironmentResourcesManager _manager;
    
    private void Start()
    {
        _manager = GameController.Instance.EnvironmentResourcesManager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
//            StartCoroutine(EnvironmentResourcesManager.Instance.LoadObjectsFromBundle("file://Users/yvasin/Desktop/cars1/"));
//            StartCoroutine(EnvironmentResourcesManager.Instance.LoadObjectsFromBundle("https://www.dropbox.com/s/12z5d8z21bcp8pr/car3?dl=1"));
        }
    }
}
