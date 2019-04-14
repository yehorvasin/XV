using UnityEngine;
using UnityEngine.EventSystems;

public class EditEnvironmentState : MonoBehaviour, IState
{
    [SerializeField]
    private EnvironmentObject _currentObjectToEdit;
    public EnvironmentObject CurrentObjectToEdit => _currentObjectToEdit;
    
    public void ActivateState()
    {
        
    }

    public void DeactivateState()
    {
        
    }

    public void MouseInputAction()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        var hit = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var obj = hit.collider.GetComponent<EnvironmentObject>();
            _currentObjectToEdit = obj;
        }
    }

    public void KeysInputAction()
    {
        if (_currentObjectToEdit == null) return;
        
        if (Input.GetKey(KeyCode.W))
            _currentObjectToEdit.Translate(Vector3.forward);
        else if (Input.GetKey(KeyCode.A))
            _currentObjectToEdit.Translate(Vector3.left);
        else if (Input.GetKey(KeyCode.S))
            _currentObjectToEdit.Translate(Vector3.back);
        else if (Input.GetKey(KeyCode.D))
            _currentObjectToEdit.Translate(Vector3.right);
        else if (Input.GetKey(KeyCode.LeftArrow))
            _currentObjectToEdit.Rotate(5);
        else if (Input.GetKey(KeyCode.RightArrow))
            _currentObjectToEdit.Rotate(-5);
        else if (Input.GetKey(KeyCode.Backspace))
        {
            Destroy(_currentObjectToEdit.gameObject);
        }
    }

    public void ChangeCurrentObjectColour(Color color)
    {
        if (_currentObjectToEdit == null) return;

        var renderers = _currentObjectToEdit.GetComponentsInChildren<Renderer>();
        for (var i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = color;
        }

        _currentObjectToEdit.color = color;
    }
}
