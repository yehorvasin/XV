using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public StateMachine StateMachine;
    public InputHandler InputHandler;
    public EnvironmentResourcesManager EnvironmentResourcesManager;
    public Saver Saver;
    public UIController UiController;

    private XVAnimationController _animationController;

    public XVAnimationController AnimController {get; set;}

    public DontDestroy DontDestroy;
    
    #region states

    private BuildingEnvironmatentState _buildingEnvironmatentState;
    private SetupAnimationsState _setupAnimationsState;
    private EditEnvironmentState _editEnvironmentState;
    private ViewModeState _viewModeState;

    public BuildingEnvironmatentState BuildingEnvironmatentState => _buildingEnvironmatentState;
    public SetupAnimationsState SetupAnimationsState => _setupAnimationsState;
    public EditEnvironmentState EditEnvironmentState => _editEnvironmentState;
    public ViewModeState ViewModeState => _viewModeState;

    #endregion
    
    public CinemachineVirtualCamera OverviewCamera;
    public CinemachineVirtualCamera FirstPersonCamera;
    public CinemachineVirtualCamera FreeLookCamera;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private IEnumerator Start()
    {
        StateMachine = GetComponent<StateMachine>();
        _buildingEnvironmatentState = gameObject.AddComponent<BuildingEnvironmatentState>();
        _setupAnimationsState = gameObject.AddComponent<SetupAnimationsState>();
        _editEnvironmentState = gameObject.AddComponent<EditEnvironmentState>();
        _viewModeState = gameObject.AddComponent<ViewModeState>();
        
        StateMachine.States.Add(_buildingEnvironmatentState);
        StateMachine.States.Add(_setupAnimationsState);
        StateMachine.States.Add(_editEnvironmentState);
        StateMachine.States.Add(_viewModeState);

        StateMachine.CurrentState = _viewModeState;
        StateMachine.CurrentState.ActivateState();

        DontDestroy = FindObjectOfType<DontDestroy>();

        while (EnvironmentResourcesManager.isReady == false)
            yield return null;
        
        DeserializeSceneContent();
    }

    public IState GetCurrentState()
    {
        return StateMachine.CurrentState;
    }

    private void DeserializeSceneContent()
    {
        if (PlayerPrefs.HasKey(DontDestroy.SceneId))
        {
            var json = PlayerPrefs.GetString(DontDestroy.SceneId);
            var list = JsonUtility.FromJson<XVObjectDataList>(json);

            var objects = list.list;
            foreach (var element in objects)
            {
                var go = Instantiate(
                    EnvironmentResourcesManager.ResourcesObjects.Find(x => x.name == element.objectName),
                    new Vector3(element.X_Position, element.Y_Position, element.Z_Position),
                    new Quaternion(element.X_Rotation, element.Y_Rotation, element.Z_Rotation, element.W_Rotation));
                
                go.name = go.name.Replace("(Clone)", "");
                var obj = go.AddComponent<EnvironmentObject>();
                obj.nameToDisplay = element.displayName;
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
