using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUIController : MonoBehaviour
{
    public GameObject AnimatorScrollView;
    public GameObject StartAnimText;
    public GameObject UniChanAnimVariants;
    public GameObject SimpleAnimVariants;
    public GameObject animPanel;

    public SetupAnimationsState SetupAnimationsState;

    void Start()
    {

       
    }
    
    public void CheckAnimState(EnvironmentObject curObj)
    {
       Debug.Log(curObj.name);
           
         if(curObj.name.Contains("UniChan"))
        {
            UniChanAnimVariants.SetActive(true);
            SimpleAnimVariants.SetActive(false);
        }
        else
        {
            UniChanAnimVariants.SetActive(false);
            SimpleAnimVariants.SetActive(true); 
        }
        
            
    }

    public void Subscribe()
    {
        GameController.Instance.SetupAnimationsState.objectChosenEvent += CheckAnimState;
        
    }

    public void Unsibscribe()
    {
        GameController.Instance.SetupAnimationsState.objectChosenEvent -= CheckAnimState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
