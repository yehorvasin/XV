using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private StateMachine _stateMachine;
    private InputHandler _inputHandler;
    private EnvironmentResourcesManager _environmentResourcesManager;
    private Saver _saver;

    private DontDestroy _dontDestroy;
    public DontDestroy DontDestroy => _dontDestroy;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        var buildingState = gameObject.AddComponent<BuildingEnvironmatentState>();
        var animationsState = gameObject.AddComponent<SetupAnimationsState>();
        
        _stateMachine.States.Add(buildingState);
        _stateMachine.States.Add(animationsState);
        
        _stateMachine.CurrentState = buildingState;

        _inputHandler = GetComponent<InputHandler>();
        _environmentResourcesManager = GetComponent<EnvironmentResourcesManager>();
        _saver = GetComponent<Saver>();

        _dontDestroy = FindObjectOfType<DontDestroy>();
        
        DeserializeSceneContent();
    }

    public IState GetCurrentState()
    {
        return _stateMachine.CurrentState;
    }

    private void DeserializeSceneContent()
    {
        if (PlayerPrefs.HasKey(_dontDestroy.SceneId))
        {
            var json = PlayerPrefs.GetString(_dontDestroy.SceneId);
            var list = JsonUtility.FromJson<XVObjectDataList>(json);

            var objects = list.list;
            foreach (var element in objects)
            {
                var go = Instantiate(
                    EnvironmentResourcesManager.Instance.ResourcesObjects.Find(x => x.name == element.objectName),
                    new Vector3(element.X_Position, element.Y_Position, element.Z_Position),
                    new Quaternion(element.X_Rotation, element.Y_Rotation, element.Z_Rotation, element.W_Rotation));
                
                go.name = go.name.Replace("(Clone)", "");
                go.tag = "spawned";
            }
        }
    }
    
    //TEMP
    [ContextMenu("Back To Menu")]
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
