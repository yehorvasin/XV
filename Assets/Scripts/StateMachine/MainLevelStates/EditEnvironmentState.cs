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
}
