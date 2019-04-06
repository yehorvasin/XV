using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public string CreateAndSaveNewScene(int id)
    {
        //TODO: Get old list and add to new
        
        var list = new XVScenesDataList();
        list.Add("scene" + id);

        var json = JsonUtility.ToJson(list);
        return json;
    }

    [ContextMenu("SaveContent")]
    public string SaveSceneData()
    {
        var all = GameObject.FindGameObjectsWithTag("spawned").ToList();
        var saveList = new XVObjectDataList();
        
        foreach (var obj in all)
        {
            var xvDataObj = new XVObjectData(obj.transform.position, obj.transform.rotation);
            xvDataObj.objectName = obj.name;
            saveList.Add(xvDataObj);
        }

        var json = JsonUtility.ToJson(saveList);
        
        PlayerPrefs.SetString(GameController.Instance.DontDestroy.SceneId, json);
        return json;
    }

    [ContextMenu("DeletePlayerPrefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
