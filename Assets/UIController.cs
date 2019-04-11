using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject messenger;

    public static void Messege(string str)
    {
       
       Debug.Log(str);
    }

    public void ToggleMenuContent(GameObject content)
    {
       content.SetActive(!content.activeSelf);
    }
}
