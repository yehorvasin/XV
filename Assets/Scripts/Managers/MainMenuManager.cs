using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] MenuObjects;
    public GameObject[] LoadUIObjects;

    public InputField InputFieldURL;

    private void Start()
    {
        OpenMenuScenesUI();
    }

    public void OpenLoadNewObjectsUI()
    {
        for (var i = 0; i < MenuObjects.Length; i++)
            MenuObjects[i].SetActive(false);
        
        for (var i = 0; i < LoadUIObjects.Length; i++)
            LoadUIObjects[i].SetActive(true);
    }

    public void OpenMenuScenesUI()
    {
        for (var i = 0; i < MenuObjects.Length; i++)
            MenuObjects[i].SetActive(true);
        
        for (var i = 0; i < LoadUIObjects.Length; i++)
            LoadUIObjects[i].SetActive(false);
    }

    public void Load()
    {
        StartCoroutine(SaveLoadedObjectsPathFromBundle());
    }

    private IEnumerator SaveLoadedObjectsPathFromBundle()
    {
        var bundlesData = new XVScenesDataList();
        
        if (PlayerPrefs.HasKey("bundles"))
        {
            var allBundlesJson = PlayerPrefs.GetString("bundles");
            bundlesData = JsonUtility.FromJson<XVScenesDataList>(allBundlesJson);
        }
        
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(InputFieldURL.text, 1, 0);
        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            if (!bundlesData.list.Contains(InputFieldURL.text))
                bundlesData.Add(InputFieldURL.text);
            var json = JsonUtility.ToJson(bundlesData);
            PlayerPrefs.SetString("bundles", json);
        }
        
        InputFieldURL.text = String.Empty;
    }
    
    [ContextMenu("Delete bundles data from PLayerPrefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("bundles");
    }
}
