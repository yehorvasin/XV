using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupAnimationsState : MonoBehaviour, IState
{
    public EnvironmentObject CurrentObjectToEdit;

    private enum AnimState {NO_OBJECT, CHOOSE_ANIM, SETUP}
    private AnimState _innerState = AnimState.NO_OBJECT;

    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void MouseInputAction()
    {
       switch (_innerState)
       {
           case AnimState.NO_OBJECT:
           {
               SelectObject();
               break;
           }
       }
    }

    public void KeysInputAction()
    {
        throw new System.NotImplementedException();
    }

    private void SelectObject()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var obj = hit.collider.GetComponent<EnvironmentObject>();
            CurrentObjectToEdit = obj;
            _innerState = AnimState.CHOOSE_ANIM;
        }
    }

}
