using System;
using UnityEngine;

[Serializable]
public class XVObjectData
{
    public string objectName;
    
    #region Position
    public float X_Position;
    public float Y_Position;
    public float Z_Position;
    #endregion
    
    #region Rotation
    public float X_Rotation;
    public float Y_Rotation;
    public float Z_Rotation;
    public float W_Rotation;
    #endregion

    public LoadFromType LoadFromType;
    public string bundlePath;
    
    public XVObjectData(Vector3 position, Quaternion rotation)
    {
        this.X_Position = position.x;
        this.Y_Position = position.y;
        this.Z_Position = position.z;

        this.X_Rotation = rotation.x;
        this.Y_Rotation = rotation.y;
        this.Z_Rotation = rotation.z;
        this.W_Rotation = rotation.w;
    }

    public void SetPosition(Vector3 position)
    {
        this.X_Position = position.x;
        this.Y_Position = position.y;
        this.Z_Position = position.z;
    }

    public void SetRotation(Quaternion rotation)
    {
        this.X_Rotation = rotation.x;
        this.Y_Rotation = rotation.y;
        this.Z_Rotation = rotation.z;
        this.W_Rotation = rotation.w;
    }
    
    public XVObjectData()
    {
        
    }
}

public enum LoadFromType
{
    Resources,
    Bundle,
}
