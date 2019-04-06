using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameController.Instance.GetCurrentState().InputAction();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SAVE
        }
    }
}
