using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class BuilderUIController : MonoBehaviour
{
    private List<GameObject> resourcesObjects;
    private UIController uiController;
    public Button btn;//prefab
    public GameObject builderScrollView;
   
    private GameObject selectedGameObject;

    void Start()
    {
        uiController = GetComponent<UIController>();
        resourcesObjects = EnvironmentResourcesManager.Instance.ResourcesObjects;
        var parentContent = builderScrollView.GetComponent<ScrollRect>().content;
        foreach (var obj in resourcesObjects)
        {
            Button newButton = Instantiate(btn);
            newButton.transform.SetParent(parentContent.transform, false);
            newButton.GetComponentInChildren<Text>().text = obj.name;
            newButton.onClick.AddListener(() => ShowObject(obj.name));
        }
    }

   void ShowObject(string name)
    {
        if(selectedGameObject!=null)
            Destroy(selectedGameObject);
       selectedGameObject = resourcesObjects.Single(s => s.name == name);
       selectedGameObject =  Instantiate(selectedGameObject);
       selectedGameObject.transform.position = new Vector3(0, 5, 10);
       BuildingEnvironmatentState.objectName = name;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (selectedGameObject != null)
                    Destroy(selectedGameObject);
                if (BuildingEnvironmatentState.objectName == null)
                    uiController.ToggleMenuContent(builderScrollView); 
                Debug.Log("Clicked not on the UI");
            }
        }
    }

   
}