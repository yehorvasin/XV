﻿using UnityEngine;

public class BuildingEnvironmatentState : MonoBehaviour, IState
{
    public string objectName;
    
    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void InputAction()
    {
        var prefab = EnvironmentResourcesManager.Instance.GetObjectByName(objectName);

        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var go = Instantiate(prefab, hit.point, Quaternion.identity);
            go.name = go.name.Replace("(Clone)", "");
            go.tag = "spawned";
        }
    }
}
