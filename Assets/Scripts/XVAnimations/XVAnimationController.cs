using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AnimCallBack();

public class XVAnimationController : MonoBehaviour
{
    public List<XVAnimation> stack;
    public int i = 0;

    public  GameObject currentObject;

    public AnimCallBack playAnimCallback;

    public XVAnimation currentSetupedAnim;

    public SetupAnimationsState setupAnimationsState;

    private void Start()
    {
        playAnimCallback += Next;
    }

    public void StartSetupAnimation(Type animType)
    {
        currentSetupedAnim = (XVAnimation) gameObject.AddComponent(animType);
        currentSetupedAnim.go1 = setupAnimationsState.CurrentObjectToEdit.gameObject;
        if (currentSetupedAnim.IsSecondObjectNeeded())
        {
            //Get Second object
        }

        if (currentSetupedAnim.points.Count < currentSetupedAnim.NumberOfPOintsNeeded())
        {
            //Get points
        }
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
            i = 0;
            stack[i].Animate(playAnimCallback);
        }
    }
    
    
    
}
