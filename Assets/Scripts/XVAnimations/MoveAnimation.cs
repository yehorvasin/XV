using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : XVAnimation
{
    public override void Animate(AnimCallBack onEnd)
    {
        Debug.Log("Animate");
        StartCoroutine(move(onEnd));
    }

    IEnumerator move( AnimCallBack onEnd)
    {
//        while (Vector3.Distance(points[0], go1.transform.position) > 0.0001f)
//        {
//            float speed = 10;
//            float step =  speed * Time.deltaTime; // calculate distance to move
//            transform.position = Vector3.MoveTowards(go1.transform.position, points[0], step);
//            Debug.Log("...");
//            yield return null;
//        }
        yield return new WaitForSeconds(1);
        go1.transform.position = points[0];
        onEnd.Invoke();
        Debug.Log("End Animate");
        yield return null;
    }
    

     public override bool IsSecondObjectNeeded()
    {
        return false;
    }
    
    public override int NumberOfPOintsNeeded()
    {
        return 1;
    }


    // public override IEnumerator Setup(AnimCallBack callBack)
    // {
    //     Debug.Log("Touch Object floor");
    //     bool set = false;
    //     while (!set)
    //     {
    //         set = CheckTouch();
    //         yield return null;
    //     }
    //     callBack.Invoke();
    //     yield return null;
    // }
    
    
//     private bool IsWasTouch(out Vector3 touchPosition)
//     {
//         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
//         {
//             touchPosition = Input.GetTouch(0).position;
//             return true;
//         }
//         else if (Input.GetMouseButtonDown(0))
//         {
//             touchPosition = Input.mousePosition;
//             return true;
//         }
        
//         touchPosition = Vector3.zero;
//         return false;
//     }//IsWasTouch



//     private bool CheckTouch()
//     {
//         Vector3 pos;

//         if (IsWasTouch(out pos))
//         {
//             Ray ray = Camera.main.ScreenPointToRay(pos);

// //            if (SimpleGraphicRaycaster.instance.IsUIBlocking())
// //	            return false;
//             RaycastHit hit;
//             Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
//             //if (Physics.Raycast(ray, out hit,1000, floorMask))
//             if (Physics.Raycast(ray, out hit))
//             {
//                 if (hit.transform.gameObject.name != "Floor")
//                     return false;
//                 points.Add(hit.point);
                
//                 return true;
//             }
//         }

//         return false;
//     }//CheckTouch()



    
    
}
