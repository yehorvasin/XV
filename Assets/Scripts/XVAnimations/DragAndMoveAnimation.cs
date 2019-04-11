using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMoveAnimation : XVAnimation
{
    public override string GetDescription()
    {
        return ("Move object to second one, take it, go to the point 0 and place second object on point 1");
    }
    public override void Animate(AnimCallBack onEnd)
    {
        Debug.Log("Animate");
        StartCoroutine(move(onEnd));
    }

    IEnumerator move( AnimCallBack onEnd)
    {
        StartCoroutine(moveToFirst(onEnd));
       
        yield return null;
    }
    
    private IEnumerator moveToFirst( AnimCallBack onEnd)
    {
        Vector3 target = go2.transform.position;
        while (Vector3.Distance(target, go1.transform.position) > 0.0001f)
       {
           float speed = 5;
           float step =  speed * Time.deltaTime;
           go1.transform.position = Vector3.MoveTowards(go1.transform.position, target, step);
           yield return null;
       }
        //take sekond object
       go2.transform.position = go1.transform.position - 0.5f * go1.transform.forward;
       go2.transform.parent = go1.transform;
       StartCoroutine(moveToDestination(onEnd));
    }

    private IEnumerator moveToDestination( AnimCallBack onEnd)
    {
        //go to next pos
         Vector3 target = points[0];
        while (Vector3.Distance(target, go1.transform.position) > 0.0001f)
       {
           float speed = 5;
           float step =  speed * Time.deltaTime;
           go1.transform.position = Vector3.MoveTowards(go1.transform.position, target, step);
           yield return null;
       }

       //place second object on second point
       go2.transform.parent = null;
       go2.transform.position = points[1];


       yield return new WaitForSeconds(1);
        onEnd.Invoke();
        Debug.Log("End Animate");
    }

    public override bool IsSecondObjectNeeded()
    {

        return (go2 == null);
    }
    
    public override int NumberOfPOintsNeeded()
    {
        return 2;
    }
}
