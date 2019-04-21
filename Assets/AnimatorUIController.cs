using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUIController : MonoBehaviour
{
    public GameObject AnimatorScrollView;
    public GameObject Content;
    public GameObject UniChanAnimVariants;
    public GameObject SimpleAnimVariants;
    public GameObject animPanel;

    public SetupAnimationsState SetupAnimationsState;

    private XVAnimationController _animationController;

    public List<GameObject> animPanels;

    private void Start()
    {
        animPanels = new List<GameObject>();
        _animationController = GameController.Instance.AnimationController;
        
        _animationController.stackAddedElement += CreateStackUI;
        _animationController.stackReload += ReloadStack;
        
        _animationController.stackDeleteElem += DeleteStackElem;
    }

    private void OnDestroy()
    {
        _animationController.stackAddedElement -= CreateStackUI;
        _animationController.stackAddedElement -= CreateStackUI;
        _animationController.stackReload -= ReloadStack;
        
        _animationController.stackDeleteElem -= DeleteStackElem;
        
    }

    public void CreateStackUI(XVAnimation stackElem)
    {
        Debug.Log("stackElem.name "+stackElem.name);
        Debug.Log("stackElem.description "+stackElem.description);
        var g = Instantiate(animPanel, Content.transform);
        g.GetComponent<XVAnimationPanel>().LinkData(stackElem);
        animPanels.Add(g);
    }

    public void ReloadStack(List<XVAnimation> stack)
    {
        Debug.Log("ReloadStack");
        for (int i=0;i<stack.Count;i++)
        {
            animPanels[i].GetComponent<XVAnimationPanel>().LinkData(stack[i]);
          
        }
    }
  
    public void DeleteStackElem(int index)
    {
        Destroy(animPanels[index]);
        animPanels.Remove(animPanels[index]);
        Debug.Log("Removed Element From UI");
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
