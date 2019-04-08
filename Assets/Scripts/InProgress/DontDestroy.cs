using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string SceneId;
    
    private void Awake()
    {
        var obj = GameObject.FindObjectsOfType<DontDestroy>();

        if (obj.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
