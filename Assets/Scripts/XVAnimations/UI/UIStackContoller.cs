using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackContoller : MonoBehaviour
{
    public GameObject animPanelPrefab; 
    
    private XVAnimationController _animationController;

    private void Start()
    {
        _animationController = GameController.Instance.AnimController;
        _animationController.stackChangedEvent += ReloadStack;
    }

    private void OnDestroy()
    {
        _animationController.stackChangedEvent -= ReloadStack;
    }

    public void ReloadStack(List<XVAnimation> stack)
    {
        
    }
    
}
