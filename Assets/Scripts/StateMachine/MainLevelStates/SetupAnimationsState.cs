using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetupAnimationsState : MonoBehaviour, IState
{
    public EnvironmentObject CurrentObjectToEdit;

    [HideInInspector] public UnityEvent mouseInputEvent;

    private enum AnimState {NO_OBJECT, CHOOSE_ANIM, SETUP}
    private AnimState _innerState = AnimState.NO_OBJECT;

    public void ActivateState()
    {
        GameController.Instance.AnimController.onAnimationSelected += AnimationSelected;
        GameController.Instance.AnimController.onAnimationSelected += AnimationSetaped;

    }

    public void DeactivateState()
    {
        GameController.Instance.AnimController.onAnimationSelected -= AnimationSelected;
        GameController.Instance.AnimController.onAnimationSelected -= AnimationSetaped;
    }

    public void MouseInputAction()
    {
       mouseInputEvent.Invoke();
       
       switch (_innerState)
       {
           case AnimState.NO_OBJECT:
           {
               SelectObject();
               break;
           }
           case AnimState.CHOOSE_ANIM:
           {
               SelectObject();//if fail to chose object -> curent objectt = null
               break;
           }
       }
    }

    public void AnimationSelected()
    {
        _innerState = AnimState.SETUP;
    }
    
    public void AnimationSetaped()
    {
        _innerState = AnimState.CHOOSE_ANIM;
    }

    public void KeysInputAction()
    {
       // throw new System.NotImplementedException();
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
        else
        {
            CurrentObjectToEdit = null;
            _innerState = AnimState.NO_OBJECT;
        }
    }

}
