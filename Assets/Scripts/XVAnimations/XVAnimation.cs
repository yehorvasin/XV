using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XVAnimation : MonoBehaviour
{
    public  GameObject go1;
    public GameObject go2;
    public List<Vector3> points = new List<Vector3>();
    public float speed = 1;

    virtual public bool IsSecondObjectNeeded()
    {
        return false;
    }
    
    virtual public int NumberOfPOintsNeeded()
    {
        return 0;
    }

    virtual public string GetDescription()
    {
        return "";
    }

    public AnimCallBack finishSetap;
    virtual public void Animate(AnimCallBack onEnd)
    {
        Debug.Log("Animate parent");
    }

    // virtual public IEnumerator Setup(AnimCallBack callBack)
    // {
    //     Debug.Log("Setup parent");
    //     yield return null;
    // }
    
    
}
