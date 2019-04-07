using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void ActivateState();
    void DeactivateState();

    void MouseInputAction();
    void KeysInputAction();
}
