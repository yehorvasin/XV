using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ViewModeState : MonoBehaviour, IState
{
    private CinemachineVirtualCamera _overviewCamera;
    private CinemachineVirtualCamera _firstPersonCamera;
    private CinemachineVirtualCamera _freeLookCamera;

    private ViewMode _currentViewMode;
    
    private void Start()
    {
        _overviewCamera = GameController.Instance.OverviewCamera;
        _firstPersonCamera = GameController.Instance.FirstPersonCamera;
        _freeLookCamera = GameController.Instance.FreeLookCamera;
        
        SwitchToOverview();
    }

    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        SwitchToOverview();
    }

    public void MouseInputAction()
    {
        
    }

    public void KeysInputAction()
    {
        #region handleKeys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToOverview();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToFirstPerson();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToFreeLook();
        #endregion
    }

    public void SwitchToOverview()
    {
        Cursor.lockState = CursorLockMode.None;
        
        _overviewCamera.gameObject.SetActive(true);
        
        _firstPersonCamera.gameObject.SetActive(false);
        _firstPersonCamera.transform.parent.gameObject.SetActive(false);
        
        _freeLookCamera.gameObject.SetActive(false);
        _freeLookCamera.transform.parent.gameObject.SetActive(false);

        _currentViewMode = ViewMode.Overview;
    }

    public void SwitchToFirstPerson()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _overviewCamera.gameObject.SetActive(false);
        
        _firstPersonCamera.gameObject.SetActive(true);
        _firstPersonCamera.transform.parent.gameObject.SetActive(true);
        
        _freeLookCamera.gameObject.SetActive(false);
        _freeLookCamera.transform.parent.gameObject.SetActive(false);

        _currentViewMode = ViewMode.FpView;
    }

    public void SwitchToFreeLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _overviewCamera.gameObject.SetActive(false);
        
        _firstPersonCamera.gameObject.SetActive(false);
        _firstPersonCamera.transform.parent.gameObject.SetActive(false);
        
        _freeLookCamera.gameObject.SetActive(true);
        _freeLookCamera.transform.parent.gameObject.SetActive(true);

        _currentViewMode = ViewMode.FreeLookView;
    }
}

public enum ViewMode
{
    Overview,
    FpView,
    FreeLookView
}
