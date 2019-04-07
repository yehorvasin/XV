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
        if (Input.GetKey(KeyCode.W))
        {
        }
        
        if (Input.GetKey(KeyCode.A))
        {
        }
        
        if (Input.GetKey(KeyCode.S))
        {
        }
        
        if (Input.GetKey(KeyCode.D))
        {
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
        }
    }
}
