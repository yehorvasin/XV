using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMoveAnimation : XVAnimation
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
        
        //go to object
        yield return new WaitForSeconds(1);
        go1.transform.position = go2.transform.position - go1.transform.forward;
       
        //take second object
        yield return new WaitForSeconds(1);
        go2.SetActive(false);
        
        //go to next pos
        yield return new WaitForSeconds(1);
        go1.transform.position = points[0];
        
        //place second object on second point
        yield return new WaitForSeconds(1);
        go2.transform.position = points[1];
        go2.SetActive(true);
        
       yield return new WaitForSeconds(1);
        onEnd.Invoke();
        Debug.Log("End Animate");
        yield return null;
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
