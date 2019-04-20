using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingEnvironmatentState : MonoBehaviour, IState
{
    public string objectName = String.Empty;

    public void ActivateState()
    {

    }

    public void DeactivateState()
    {

    }

    public void MouseInputAction()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!objectName.Equals(String.Empty))
        {
            var prefab = GameController.Instance.EnvironmentResourcesManager.GetObjectByName(objectName);

            var hit = new RaycastHit();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                var go = Instantiate(prefab, hit.point, Quaternion.identity);
                go.name = go.name.Replace("(Clone)", "");
                go.AddComponent<EnvironmentObject>();

                //AutoSave after new object built on the scene
                GameController.Instance.Saver.SaveSceneData();
                return;
            }
            
            UIController.Messege(Messenger.NoSelectObject);
        }
    }
    
    public void KeysInputAction()
    {

    }
}
