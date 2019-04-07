using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GameController.Instance.GetCurrentState().MouseInputAction();
        
        GameController.Instance.GetCurrentState().KeysInputAction();

        //TODO: remove later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.Saver.SaveSceneData();
        }
    }
}
