using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMoveAnimation : XVAnimation
{
    float start_y;
    
    public override string GetDescription()
    {
        if (string.IsNullOrEmpty(description))
        return ("Move object to second one, take it, go to the point 0 and place second object on point 1");
        else
        {
            return description;
        }
    }
    
    
    
    private bool Check()
    {
        return (go1 != null && go2 != null);
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
        StartCoroutine(moveToFirst(onEnd));
       
        yield return null;
    }
    
    private IEnumerator moveToFirst( AnimCallBack onEnd)
    {
        Vector3 tmp;
        Vector3 target = go2.transform.position;
        float delta = Mathf.Abs(target.y - start_y);
        while (Vector3.Distance(target, go1.transform.position) > delta)
       {
           
           float step =  speed * Time.deltaTime * 5;
           tmp = Vector3.MoveTowards(go1.transform.position, target, step);
           tmp.y = start_y;
           go1.transform.position = tmp;
           yield return null;
       }
        //take sekond object
       go2.transform.position = go1.transform.position +  go1.transform.up * 1.5f;
       go2.transform.parent = go1.transform;
       StartCoroutine(moveToDestination(onEnd));
    }

    private IEnumerator moveToDestination( AnimCallBack onEnd)
    {
        //go to next pos
        Vector3 tmp;
        Vector3 target = points[0];
        float delta = Mathf.Abs(target.y - start_y);
        while (Vector3.Distance(target, go1.transform.position) > delta)
       {
          
           float step =  speed * Time.deltaTime * 5;
           tmp = Vector3.MoveTowards(go1.transform.position, target, step);
           tmp.y = start_y;
           go1.transform.position = tmp;
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
