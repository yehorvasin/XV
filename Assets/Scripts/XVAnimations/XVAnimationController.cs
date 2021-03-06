﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public MsgDelegate newMessage;

    public AnimCallBack onAnimationSelected;
    public AnimCallBack onAnimationSetuped;

    public GameObject particlePrefab;

    private bool isPlaying = false;
    private List<EnvironmentObject> animatedObjects = new List<EnvironmentObject>();
    
   
    
    public delegate void StackAddedElement(XVAnimation stack);
    public StackAddedElement stackAddedElement;
    
    public delegate void StackReload(List<XVAnimation> stack);
    public StackReload stackReload;
    
    public delegate void StackDeleteElem(int index);
    public StackDeleteElem stackDeleteElem;
    
    public delegate void OnSequencePlay(bool playing);
    public OnSequencePlay sequencePlay;
    

    private IEnumerator Start()
    {
        GameController.Instance.AnimController = this;
        yield return null;
        playAnimCallback += Next;
        
    }

    public void LinkDependencies(SetupAnimationsState _setupAnimationsState)
    {
        DisplayToUser("Please select an object to start an animation setup");
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

            DisplayToUser("Animation setup successfully!!");
            if (setupAnimationsState.CurrentObjectToEdit.unityChan)
                setupAnimationsState.CurrentObjectToEdit.gameObject.GetComponent<UChanVoice>().PlayYata();
            AddToStack(currentSetupedAnim);
         
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

        if (!isPlaying && stack.Count > 0)
        {
            isPlaying = true;
            if (sequencePlay != null)
                sequencePlay(isPlaying);
            DisplayToUser(stack[0].GetDescription());
            stack[0].Animate(playAnimCallback);
           

        }
        else
        {
            Debug.Log("Already Playing");
        }
    }
    
    public void AddToStack(XVAnimation a)
    {
        Debug.Log("Added to stack");
        stack.Add(a);
        if (stackAddedElement != null)
            stackAddedElement(a);
    }


    public void StopAnim()
    {
//        stack[i].StopAllCoroutines();
        i = 0;
        isPlaying = false;
        
    }
    
    
    public void Next()
    {
        i++;
        if (i < stack.Count)
            {
                DisplayToUser(stack[i].GetDescription());
                stack[i].Animate(playAnimCallback);
                Debug.Log("anim: " + i);
            }
        else
        {
            isPlaying = false;
            if (sequencePlay != null)
                sequencePlay(isPlaying);
            ResetObjectPOsistions();
            i = 0;
            DisplayToUser("Animations ended");
            //if cycled
            if (isCycled)
            {
                isPlaying = true;
                stack[i].Animate(playAnimCallback);
            }
            
        }
    }

    public void DisplayToUser(string str)
    {
        //some ui msg
        if (newMessage != null)
            newMessage(str);
        Debug.Log(str);
    }

    public void ResetObjectPOsistions()
    {
        foreach (EnvironmentObject i in animatedObjects)
        {
            i.ResetToStartPos();
        }
    }

    

    public void MoveUp(XVAnimation a)
    {
        if (!isPlaying)
        {
            int index = stack.IndexOf(a);
            if (index > 0)
            {
                XVAnimation tmp = stack[index - 1];
                stack[index - 1] = a;
                stack[index] = tmp;
                Debug.Log("Element From Stack Moved UP");
                if (stackReload != null)
                    stackReload(stack);
            }
        }
    }
    
    public void MoveDown(XVAnimation a)
    {
        if (!isPlaying)
        {
            int index = stack.IndexOf(a);
            if (index > -1 && index < stack.Count - 1)
            {
                XVAnimation tmp = stack[index + 1];
                stack[index + 1] = a;
                stack[index] = tmp;
                if (stackReload != null)
                    stackReload(stack);
            }
        }
    }

    public void RemoveAnim(XVAnimation a)
    {
        if (!isPlaying)
        {
            if (stackDeleteElem != null)
                stackDeleteElem(stack.IndexOf(a));

            stack.Remove(a);
            Destroy(a);
        }

    }
    

    private void OnDestroy()
    {
//        setupAnimationsState.mouseInputEvent.RemoveListener(OnInputMouse);
    }
}
