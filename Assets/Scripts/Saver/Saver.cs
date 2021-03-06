﻿using UnityEngine;

public class Saver : MonoBehaviour
{
    public string CreateAndSaveNewScene(int id)
    {
        //TODO: Get old list and add to new
        
        var list = new XVScenesDataList();
        list.Add("scene" + id);

        var json = JsonUtility.ToJson(list);
        return json;
    }

    public string SaveSceneData()
    {
        var all = FindObjectsOfType<EnvironmentObject>();
        var saveList = new XVObjectDataList();
        
        foreach (var obj in all)
        {
            var xvDataObj = new XVObjectData(obj.transform.position, obj.transform.rotation)
            {
                objectName = obj.name,
                displayName = obj.nameToDisplay
            };
            xvDataObj.SetColor(obj.color);
            saveList.Add(xvDataObj);
        }

        var json = JsonUtility.ToJson(saveList);
        
        PlayerPrefs.SetString(GameController.Instance.DontDestroy.SceneId, json);
        
        Debug.Log("CONTENT SAVED!!!");
        return json;
    }

    [ContextMenu("DeletePlayerPrefs")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
