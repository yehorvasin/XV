using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditEnvironmentState : MonoBehaviour, IState
{
    public EnvironmentObject CurrentObjectToEdit;
    
    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void MouseInputAction()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var obj = hit.collider.GetComponent<EnvironmentObject>();
            CurrentObjectToEdit = obj;
        }
    }

    public void KeysInputAction()
    {
        if (Input.GetKey(KeyCode.W))
            CurrentObjectToEdit.Translate(Vector3.forward);
        else if (Input.GetKey(KeyCode.A))
            CurrentObjectToEdit.Translate(Vector3.left);
        else if (Input.GetKey(KeyCode.S))
            CurrentObjectToEdit.Translate(Vector3.back);
        else if (Input.GetKey(KeyCode.D))
            CurrentObjectToEdit.Translate(Vector3.right);
        else if (Input.GetKey(KeyCode.LeftArrow))
            CurrentObjectToEdit.Rotate(5);
        else if (Input.GetKey(KeyCode.RightArrow))
            CurrentObjectToEdit.Rotate(-5);
        else if (Input.GetKey(KeyCode.Backspace))
        {
            Destroy(CurrentObjectToEdit.gameObject);
        }
    }
}
