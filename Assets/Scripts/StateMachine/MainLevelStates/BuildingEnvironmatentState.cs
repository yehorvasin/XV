using System;
using UnityEngine;

public class BuildingEnvironmatentState : MonoBehaviour, IState
{
    public static string objectName = String.Empty;
    
    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void MouseInputAction()
    {
        if(!objectName.Equals(String.Empty))
        {
            var prefab = EnvironmentResourcesManager.Instance.GetObjectByName(objectName);

            var hit = new RaycastHit();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                var go = Instantiate(prefab, hit.point, Quaternion.identity);
                go.name = go.name.Replace("(Clone)", "");
                go.AddComponent<EnvironmentObject>();
            }

            return;
        }
        UIController.Messege(Messenger.NoSelectObject);
    }

    public void KeysInputAction()
    {
        
    }
}
