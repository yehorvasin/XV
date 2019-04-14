using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIController : MonoBehaviour
{
    public GameObject EditorUiContent;

    public Button[] PalitraButtons;
    
    public InputField inputField;

    private void Start()
    {
        var state = GameController.Instance.EditEnvironmentState;

        foreach (var button in PalitraButtons)
        {
            var image = button.GetComponent<RawImage>();
            button.onClick.AddListener(() => state.ChangeCurrentObjectColour(image.color));
        }
    }
}
