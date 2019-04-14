using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject messenger;

    public BuilderUIController BuilderUiController;
    public EditorUIController EditorUiController;

    private GameController GameController => GameController.Instance;

    private void Start()
    {
        BuilderUiController = GetComponent<BuilderUIController>();
        EditorUiController = GetComponent<EditorUIController>();
    }

    public static void Messege(string str)
    {
        Debug.Log(str);
    }

    public void ToggleMenuContent(GameObject content)
    {
       content.SetActive(!content.activeSelf);
    }

    public void ActivateBuilderUi()
    {
        GameController.StateMachine.CurrentState.DeactivateState();
        GameController.StateMachine.CurrentState = GameController.BuildingEnvironmatentState;
        GameController.StateMachine.CurrentState.ActivateState();
        
        BuilderUiController.builderScrollView.SetActive(true);
        
        EditorUiController.EditorUiContent.SetActive(false);
    }

    public void ActivateEditorUi()
    {
        GameController.StateMachine.CurrentState.DeactivateState();
        GameController.StateMachine.CurrentState = GameController.EditEnvironmentState;
        GameController.StateMachine.CurrentState.ActivateState();
        
        EditorUiController.EditorUiContent.SetActive(true);
        
        BuilderUiController.builderScrollView.SetActive(false);
    }

    public void ActivateViewModesUi()
    {
        GameController.StateMachine.CurrentState.DeactivateState();
        GameController.StateMachine.CurrentState = GameController.ViewModeState;
        GameController.StateMachine.CurrentState.ActivateState();
        
        BuilderUiController.builderScrollView.SetActive(false);
        EditorUiController.EditorUiContent.SetActive(false);
    }
    
    public void ActivateAnimationsUi()
    {
        
    }
}
