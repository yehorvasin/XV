using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    public XVAnimationController controller;
    public GameObject animHolder;

    public XVAnimation anim;
    public AnimCallBack callBack;
    private XVAnimation a;

    // private void Start()
    // {
    //     callBack += AddToSTack;
    // }

    public void StartSetupAnim()
    {
        controller.StartSetupAnimation(anim.GetType());
    }

    // public void AddToSTack()
    // {
    //     controller.AddToStack(a);
    //     Debug.Log("Added");
    // }
    
    
}
