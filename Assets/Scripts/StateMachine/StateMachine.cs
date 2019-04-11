using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState;
    public List<IState> States = new List<IState>();

    public string currStateStr;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        currStateStr = CurrentState?.GetType().ToString();
    }
}
