using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] MenuObjects;
    public GameObject[] LoadUIObjects;

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
}
