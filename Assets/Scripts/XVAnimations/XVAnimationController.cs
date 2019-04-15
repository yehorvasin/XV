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

    private bool isPlaying = false;
    private List<EnvironmentObject> animatedObjects = new List<EnvironmentObject>();

    private IEnumerator Start()
    {
        GameController.Instance.AnimController = this;
        yield return null;
        playAnimCallback += Next;
        
    }

    public void LinkDependencies(SetupAnimationsState _setupAnimationsState)
    {
        setupAnimationsState = _setupAnimationsState;
        setupAnimationsState.mouseInputEvent.AddListener(OnInputMouse);
    }

    public void Delink()
    {
        setupAnimationsState.mouseInputEvent.RemoveListener(OnInputMouse);
    }

    public void StartSetupAnimation(Type animType)
    {
        currentSetupedAnim = (XVAnimation) gameObject.AddComponent(animType);
        currentSetupedAnim.go1 = setupAnimationsState.CurrentObjectToEdit.gameObject;
        onAnimationSelected?.Invoke();
        animatedObjects.Add(setupAnimationsState.CurrentObjectToEdit);
        CheckSetupedAnim();
        DisplayToUser(currentSetupedAnim.GetDescription());
    }

    private void CheckSetupedAnim()
    {
        if (currentSetupedAnim.IsSecondObjectNeeded())
        {
            needObject = true;
            //print msg
            DisplayToUser("Need second object");
        }
        else
        {
            needObject = false;
            
        }

        if (currentSetupedAnim.points.Count < currentSetupedAnim.NumberOfPOintsNeeded() && !needObject)
        {
            needPoint = true;
            DisplayToUser("Need point");
        }
        else
        {
            needPoint = false;
        }

        if (!needObject && !needPoint)
        {
            DisplayToUser("Animation setuped succesfully!!");
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

        else if (needPoint)
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
                   animatedObjects.Add(obj);
                   DisplayToUser("Second object selected!");
                   return hit.transform.gameObject;
                    
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

            DisplayToUser("Point selected");
            res = hit.point;
            return true;
        }
        res = Vector3.zero;
        return false;
    }
    
    public void StartSeq()
    {
        if (!isPlaying)
        {isPlaying = true;
        stack[0].Animate(playAnimCallback);}
        else
        {
            Debug.Log("Already Playing");
        }
    }
    
    public void AddToStack(XVAnimation a)
    {
        stack.Add(a);
    }

    public void Next()
    {
        i++;
        if (i < stack.Count)
            {
                stack[i].Animate(playAnimCallback);
                Debug.Log("Next anim: " + i);
            }
        else
        {
            isPlaying = false;
            ResetObjectPOsistions();
            i = 0;
            //if cycled
            if (isCycled)
            {
                isPlaying = true;
                stack[i].Animate(playAnimCallback);
            }
            
        }
    }

    private void DisplayToUser(string str)
    {
        //some ui msg
        Debug.Log(str);
    }

    public void ResetObjectPOsistions()
    {
        foreach (EnvironmentObject i in animatedObjects)
        {
            i.ResetToStartPos();
        }
    }

    private void OnDestroy()
    {
        setupAnimationsState.mouseInputEvent.RemoveListener(OnInputMouse);
    }
}
