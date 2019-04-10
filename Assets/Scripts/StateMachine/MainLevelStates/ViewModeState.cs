using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ViewModeState : MonoBehaviour, IState
{
    private CinemachineVirtualCamera _overviewCamera;
    private CinemachineVirtualCamera _firstPersonCamera;
    private CinemachineVirtualCamera _freeLookCamera;

    private void Start()
    {
        _overviewCamera = GameController.Instance.OverviewCamera;
        _firstPersonCamera = GameController.Instance.FirstPersonCamera;
        _freeLookCamera = GameController.Instance.FreeLookCamera;
    }

    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void MouseInputAction()
    {
        
    }

    public void KeysInputAction()
    {
        #region handleKeys
        if (Input.GetKey(KeyCode.W))
        {
        }
        else if (Input.GetKey(KeyCode.A))
        {
        }
        else if (Input.GetKey(KeyCode.S))
        {
        }
        else if (Input.GetKey(KeyCode.D))
        {
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
        }
        else if (Input.GetKey(KeyCode.Backspace))
        {
        }
        #endregion
    }

    public void SwitchToOverview()
    {
        _overviewCamera.gameObject.SetActive(true);
        _firstPersonCamera.gameObject.SetActive(false);
        _freeLookCamera.gameObject.SetActive(false);
    }

    public void SwitchToFirstPerson()
    {
        _overviewCamera.gameObject.SetActive(false);
        _firstPersonCamera.gameObject.SetActive(true);
        _freeLookCamera.gameObject.SetActive(false);
    }

    public void SwitchToFreeLook()
    {
        _overviewCamera.gameObject.SetActive(false);
        _firstPersonCamera.gameObject.SetActive(false);
        _freeLookCamera.gameObject.SetActive(true);
    }
}
