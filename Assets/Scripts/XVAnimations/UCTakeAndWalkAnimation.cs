using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UCTakeAndWalkAnimation : XVAnimation
{
    private Animator animator;
    private NavMeshAgent agent;

    private float startAnimSpeed, startAgentSpeed;

    private void Start()
    {
        name = "Unity chan take and walk animation";
    }

    public override string GetDescription()
    {
        if (string.IsNullOrEmpty(description))
        return ("Unity-chan walk to second object, take it, go to point 0 and place object on point 1");
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
        // StartCoroutine(move(onEnd));
        if (go1.GetComponent<EnvironmentObject>().unityChan)
        {
            go1.GetComponent<UChanVoice>().PlayYo();
            agent = go1.GetComponent<NavMeshAgent>();
            animator = go1.GetComponent<Animator>();

            if (go2.transform.position.y > 0f)
                points[0] = new Vector3(go2.transform.position.x, 0f, go2.transform.position.z);
            Debug.Log(points[0]);
                
            NavMeshHit hit;
            if (NavMesh.SamplePosition(go2.transform.position, out hit, 2.0f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(go1.transform.position, hit.position, NavMesh.AllAreas, path))
                {
                    agent.path = path;
                    agent.isStopped = false;
                    animator.SetBool("walk", true);
                    startAnimSpeed = animator.speed;
                    startAgentSpeed = agent.speed;
                    animator.speed = speed;
                    agent.speed = speed * 2;
                    StartCoroutine(CheckIfOnPlace(onEnd));
                }
                else
                {
                    onEnd.Invoke();
                    Debug.Log("Invalid Path. End Animate");
                }
                
                
               // agent.destination = hit.position;
                
            }
            else
            {
                onEnd.Invoke();
                Debug.Log("Invalid Path. End Animate");
            }

           
        }
        else
        {
            onEnd.Invoke();
            Debug.Log("Not a unity chan!");
        }
    }

    IEnumerator CheckIfOnPlace( AnimCallBack onEnd)
    {
        Vector3 target = go2.transform.position;
        target.y = go1.transform.position.y;
        while (Vector3.Distance(target, go1.transform.position) > 2f)
       {
           yield return new WaitForSeconds(0.1f);
           
           NavMeshHit hit;
           if (NavMesh.SamplePosition(target, out hit, 2.0f, NavMesh.AllAreas))
           {
               NavMeshPath path = new NavMeshPath();
               if (NavMesh.CalculatePath(go1.transform.position, hit.position, NavMesh.AllAreas, path))
               {
                   agent.path = path;
                 
                  
               }
               else
               {
                   endAnim(onEnd);
                   Debug.Log("Invalid Path. End Animate");
                   break;
               }
                
                
               // agent.destination = hit.position;
                
           }
           else
           {
               endAnim(onEnd);
               break;
           }
       }
       
        //take sekond object
       go2.transform.position = go1.transform.position +  go1.transform.up;
       go2.transform.parent = go1.transform;
       StartCoroutine(moveToDestination(onEnd));
       
        yield return null;
    }

    private void endAnim(AnimCallBack onEnd)
    {
        go1.GetComponent<UChanVoice>().PlayLaught();
        animator.speed = startAnimSpeed;
        agent.speed = startAgentSpeed;
        agent.isStopped = true;
        animator.SetBool("walk", false);

        onEnd.Invoke();
        Debug.Log("End Animate");
    }
    

    private IEnumerator moveToDestination( AnimCallBack onEnd)
    {
        go1.GetComponent<UChanVoice>().PlayYo();
        //go to next pos
         Vector3 target = points[0];
         target.y = go1.transform.position.y;
        while (Vector3.Distance(target, go1.transform.position) > 2f)
       {
           yield return new WaitForSeconds(0.1f);
           
           NavMeshHit hit;
           if (NavMesh.SamplePosition(target, out hit, 2.0f, NavMesh.AllAreas))
           {
               NavMeshPath path = new NavMeshPath();
               if (NavMesh.CalculatePath(go1.transform.position, hit.position, NavMesh.AllAreas, path))
               {
                   agent.path = path;
                 
                  
               }
               else
               {
                   endAnim(onEnd);
                   go2.GetComponent<EnvironmentObject>().ResetToStartPos();
                   Debug.Log("Invalid Path. End Animate");
                   break;
               }
                
                
               // agent.destination = hit.position;
                
           }
           else
           {
               endAnim(onEnd);
               break;
           }
       }

       //place second object on second point
       go2.transform.parent = null;
       go2.transform.position = points[1];

       yield return new WaitForSeconds(0.5f);
       endAnim(onEnd);
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
