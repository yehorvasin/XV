using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetupAnimationsState : MonoBehaviour, IState
{
    public EnvironmentObject CurrentObjectToEdit;

    [HideInInspector] public UnityEvent mouseInputEvent = new UnityEvent();

    private enum AnimState {NO_OBJECT, CHOOSE_ANIM, SETUP}
    private AnimState _innerState = AnimState.NO_OBJECT;

    private GameObject particle;

    public delegate void OnObjectChosen(EnvironmentObject obj);

    public OnObjectChosen objectChosenEvent;

    public void ActivateState()
    {
        GameController.Instance.AnimController.LinkDependencies(this);

        
        particle = Instantiate( GameController.Instance.AnimController.particlePrefab);
        particle.SetActive(false);

        GameController.Instance.AnimController.onAnimationSelected += AnimationSelected;
        GameController.Instance.AnimController.onAnimationSetuped += AnimationSetaped;

    }

    public void DeactivateState()
    {
        GameController.Instance.AnimController.Delink();
        Destroy(particle);
        GameController.Instance.AnimationController.StopAnim();
        GameController.Instance.AnimController.onAnimationSelected -= AnimationSelected;
        GameController.Instance.AnimController.onAnimationSetuped -= AnimationSetaped;
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
        if (CurrentObjectToEdit.unityChan)
            CurrentObjectToEdit.gameObject.GetComponent<UChanVoice>().PlayNice();
    }
    
    public void AnimationSetaped()
    {
        _innerState = AnimState.NO_OBJECT;
        particle.SetActive(false);
        // CurrentObjectToEdit = null;
        // Debug.Log("Choose object to continue");
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
            if (obj != null)
            {
                CurrentObjectToEdit = obj;
                _innerState = AnimState.CHOOSE_ANIM;
                Debug.Log("Object selected! You can now chhose animation for it!");
                GameController.Instance.AnimController.DisplayToUser("Object selected! You can now chhose animation for it!");
                particle.transform.position = CurrentObjectToEdit.transform.position;
                particle.SetActive(true);
                particle.GetComponent<ParticleSystem>().Play();

                if (CurrentObjectToEdit.unityChan)
                    CurrentObjectToEdit.gameObject.GetComponent<UChanVoice>().PlayHuhu();

                if (objectChosenEvent != null)
                    objectChosenEvent(obj);

            }
        }
        else
        {
            // CurrentObjectToEdit = null;
            // _innerState = AnimState.NO_OBJECT;
        }
    }

}
