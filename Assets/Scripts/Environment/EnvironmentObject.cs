using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public string nameToDisplay;

    
    #region settings

    [Header("Settings")]
    [Range(1, 5)]
    public float TranslationSpeed = 1;

    #endregion

    private void Start()
    {
        nameToDisplay = gameObject.name;
    }

    private void Update()
    {
        
    }

    public void Translate(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * TranslationSpeed;
    }

    public void Rotate(float angle)
    {
        var newAngle = transform.eulerAngles.y + angle /** Time.deltaTime*/;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newAngle, transform.eulerAngles.z);
    }

    public void ChangeDisplayName()
    {
        
    }
}
