using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AnimCallBack();

public class XVAnimationController : MonoBehaviour
{
    public List<XVAnimation> stack;
    private int i = 0;

    public AnimCallBack playAnimCallback;

    public XVAnimation currentSetupedAnim;

    [HideInInspector]public SetupAnimationsState setupAnimationsState;

    public bool needObject = false;
    public bool needPoint = false;

    public bool isCycled = false;

    public AnimCallBack onAnimationSelected;
    public AnimCallBack onAnimationSetuped;

    private IEnumerator Start()
    {
        GameController.Instance.AnimController = this;
        yield return null;
        setupAnimationsState = (SetupAnimationsState)GameController.Instance.StateMachine.CurrentState;
        
         
        playAnimCallback += Next;
        setupAnimationsState.mouseInputEvent.AddListener(OnInputMouse);
    }

    public void StartSetupAnimation(Type animType)
    {
        currentSetupedAnim = (XVAnimation) gameObject.AddComponent(animType);
        currentSetupedAnim.go1 = setupAnimationsState.CurrentObjectToEdit.gameObject;
        onAnimationSelected?.Invoke();
        CheckSetupedAnim();
    }

    private void CheckSetupedAnim()
    {
        if (currentSetupedAnim.IsSecondObjectNeeded())
        {
            needObject = true;
            //print msg
            Debug.Log("Need second object");
        }
        else
        {
            needObject = false;
            
        }

        if (currentSetupedAnim.points.Count < currentSetupedAnim.NumberOfPOintsNeeded())
        {
            needPoint = true;
            Debug.Log("Need point");
        }
        else
        {
            needPoint = false;
        }

        if (!needObject && !needPoint)
        {
            Debug.Log("Animation setuped succesfully!!");
            stack.Add(currentSetupedAnim);
            currentSetupedAnim = null;
            onAnimationSetuped?.Invoke();
        }
    }


    public void Cancel()
    {
        //TODO: implement
    }

    public void OnInputMouse()
    {
        if (needObject)
        {
            var tmp = SelectObject();
            if (tmp != null)
            {
                currentSetupedAnim.go2 = tmp;
                needObject = false;
                CheckSetupedAnim();
            }
            
        }

        if (needPoint)
        {
            Vector3 p;
            if (SelectPoint(out p))
            {
                needPoint = false;
                currentSetupedAnim.points.Add(p);
                CheckSetupedAnim();
            }
        }
        
        
    }
    
    private GameObject SelectObject()
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var obj = hit.collider.GetComponent<EnvironmentObject>();
            if (obj != null)
               {
                    return hit.transform.gameObject;
                    Debug.Log("Second object selected!");
               }
        }

        return null;
    }
    
    private bool SelectPoint(out Vector3 res)
    {
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var obj = hit.collider.GetComponent<EnvironmentObject>();
            res = hit.point;
            return true;
        }
        res = Vector3.zero;
        return false;
    }
    
    public void StartSeq()
    {
        stack[0].Animate(playAnimCallback);
    }
    
    public void AddToStack(XVAnimation a)
    {
        stack.Add(a);
    }

    public void Next()
    {
        i++;
        if (i < stack.Count)
            stack[i].Animate(playAnimCallback);
        else
        {
            //if cycled
            i = 0;
            stack[i].Animate(playAnimCallback);
        }
    }

    private void OnDestroy()
    {
        setupAnimationsState.mouseInputEvent.RemoveListener(OnInputMouse);
    }
}
