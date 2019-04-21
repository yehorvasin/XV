using System;
using System.Collections;
using System.Collections.Generic;
using GetSocialSdk.Capture.Scripts;
using UnityEngine;

public class GifCaptureController : MonoBehaviour
{
    private GetSocialCapture _capture;
    private XVAnimationController _animationController;
    void Start()
    {
        _animationController = GameController.Instance.AnimationController;
        _animationController.sequencePlay += RecordVideo;
        _capture = GetComponent<GetSocialCapture>();
    }

    void Unsuscribe()
    {
        _animationController.sequencePlay -= RecordVideo;
    }

    public void RecordVideo(bool isPlaying)
    {
       if(isPlaying) 
           _capture.StartCapture();
       else
       {
           _capture.StopCapture();
           ShareResult();
       }
    }
    
    public void ShareResult()
    {
        Debug.Log("Starting gif generation");
        Action<byte[]> result = bytes =>
        {
			
        };  
        _capture.GenerateCapture(result);
    }
}
