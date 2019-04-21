using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : XVAnimation
{
    float start_y;
    
    public override string GetDescription()
    {
        if (string.IsNullOrEmpty(description))
        return ("Move object to point 0");
        else
        {
            return description;
        }
    }
    
    private bool Check()
    {
        return (go1 != null);
    }
    
    private void Start()
    {
        name = "Move animation";
    }
    
    public override void Animate(AnimCallBack onEnd)
    {
        Debug.Log("Animate");

        if (!Check())
        {
            onEnd.Invoke();
            Debug.Log("invalid paramenters");
            return;
        }
        if (!go1.GetComponent<EnvironmentObject>().unityChan)
        {start_y = go1.transform.position.y;
        Debug.Log("Animate");
        StartCoroutine(move(onEnd));}
        else
        {
            onEnd.Invoke();
        }
    }

    IEnumerator move( AnimCallBack onEnd)
    {
        Vector3 tmp;
        Vector3 target = points[0];
        float delta = Mathf.Abs(target.y - start_y);
       while (Vector3.Distance(target, go1.transform.position) > delta)
       {
          
           float step =  speed * Time.deltaTime * 5; // calculate distance to move
           tmp = Vector3.MoveTowards(go1.transform.position, target, step);
           tmp.y = start_y;
           go1.transform.position = tmp;
           yield return null;
       }
        
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
