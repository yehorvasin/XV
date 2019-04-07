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
            currObject.Translate(Vector3.forward);
        else if (Input.GetKey(KeyCode.A))
            currObject.Translate(Vector3.left);
        else if (Input.GetKey(KeyCode.S))
            currObject.Translate(Vector3.back);
        else if (Input.GetKey(KeyCode.D))
            currObject.Translate(Vector3.right);
        else if (Input.GetKey(KeyCode.LeftArrow))
            currObject.Rotate(5);
        else if (Input.GetKey(KeyCode.RightArrow))
            currObject.Rotate(-5);
    }
}
