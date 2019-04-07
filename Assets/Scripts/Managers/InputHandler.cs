using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameController.Instance.GetCurrentState().MouseInputAction();
        }

        HandleKeys();

        //TODO: remove later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.Saver.SaveSceneData();
        }
    }

    private void HandleKeys()
    {
        var state = GameController.Instance.StateMachine.CurrentState as EditEnvironmentState;
        if (!state) return;
        
        var currObject = state.CurrentObjectToEdit;
        if (currObject == null) return;
        
        if (Input.GetKey(KeyCode.W))
            currObject.Translate();
        else if (Input.GetKey(KeyCode.A))
            currObject.Translate();
        else if (Input.GetKey(KeyCode.S))
            currObject.Translate();
        else if (Input.GetKey(KeyCode.D))
            currObject.Translate();
        else if (Input.GetKey(KeyCode.LeftArrow))
            currObject.Rotate();
        else if (Input.GetKey(KeyCode.RightArrow))
            currObject.Rotate();
    }
}
