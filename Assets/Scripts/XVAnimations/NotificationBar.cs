using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void MsgDelegate(string str);

public class NotificationBar : MonoBehaviour
{

    Text text;

    IEnumerator Start()
    {
        text = GetComponent<Text>();
        yield return null;
        GameController.Instance.AnimController.newMessage += SetMesage;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        GameController.Instance.AnimController.newMessage -= SetMesage;
    }

    public void SetMesage(string msg)
    {
        text.text = msg;
    }
}
