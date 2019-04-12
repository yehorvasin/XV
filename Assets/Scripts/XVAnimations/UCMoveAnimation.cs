using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UCMoveAnimation : XVAnimation
{
    private Animator animator;
    private NavMeshAgent agent;

     public override string GetDescription()
    {
        return ("Unity-chan walk to point 0");
    }
    public override void Animate(AnimCallBack onEnd)
    {
        Debug.Log("Animate");
        // StartCoroutine(move(onEnd));
        if (go1.GetComponent<EnvironmentObject>().unityChan)
        {
            agent = go1.GetComponent<NavMeshAgent>();
            animator = go1.GetComponent<Animator>();

            
            
            agent.destination = points[0];
            agent.isStopped = false;
            animator.SetBool("walk", true);
             StartCoroutine(CheckIfOnPlace(onEnd));
        }
        else
        {
            Debug.Log("Not a unity chan!");
        }
    }

    // IEnumerator move( AnimCallBack onEnd)
    // {
       
       
    //     yield return null;
    // }

    IEnumerator CheckIfOnPlace( AnimCallBack onEnd)
    {
        Vector3 target = points[0];
        while (Vector3.Distance(target, go1.transform.position) > 0.01f)
       {
           
           yield return new WaitForSeconds(0.1f);
       }
       
        agent.isStopped = true;
        animator.SetBool("walk", false);
        yield return new WaitForSeconds(1);
        onEnd.Invoke();
        Debug.Log("End Animate");
       
        yield return null;
    }
    
    // private IEnumerator moveToFirst( AnimCallBack onEnd)
    // {
    //     Vector3 target = go2.transform.position;
    //     while (Vector3.Distance(target, go1.transform.position) > 0.0001f)
    //    {
    //        float speed = 5;
    //        float step =  speed * Time.deltaTime;
    //        go1.transform.position = Vector3.MoveTowards(go1.transform.position, target, step);
    //        yield return null;
    //    }
    //     //take sekond object
    //    go2.transform.position = go1.transform.position +  go1.transform.up;
    //    go2.transform.parent = go1.transform;
    //    StartCoroutine(moveToDestination(onEnd));
    // }

    // private IEnumerator moveToDestination( AnimCallBack onEnd)
    // {
    //     //go to next pos
    //      Vector3 target = points[0];
    //     while (Vector3.Distance(target, go1.transform.position) > 0.0001f)
    //    {
    //        float speed = 5;
    //        float step =  speed * Time.deltaTime;
    //        go1.transform.position = Vector3.MoveTowards(go1.transform.position, target, step);
    //        yield return null;
    //    }

    //    //place second object on second point
    //    go2.transform.parent = null;
    //    go2.transform.position = points[1];


    //    yield return new WaitForSeconds(1);
    //     onEnd.Invoke();
    //     Debug.Log("End Animate");
    // }

    public override bool IsSecondObjectNeeded()
    {

        return false;
    }
    
    public override int NumberOfPOintsNeeded()
    {
        return 1;
    }
}
