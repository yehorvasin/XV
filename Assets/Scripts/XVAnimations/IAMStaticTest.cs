using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMStaticTest : MonoBehaviour
{
    public XVAnimationController contoleer;
    
    void Start()
    {
        contoleer.currentObject = gameObject;
    }

    void Update()
    {
        
    }
}
