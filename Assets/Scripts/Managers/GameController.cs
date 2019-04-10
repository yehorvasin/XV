using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public StateMachine StateMachine;
    public InputHandler InputHandler;
    public EnvironmentResourcesManager EnvironmentResourcesManager;
    public Saver Saver;

    public DontDestroy DontDestroy;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StateMachine = GetComponent<StateMachine>();
        var buildingState = gameObject.AddComponent<BuildingEnvironmatentState>();
        var animationsState = gameObject.AddComponent<SetupAnimationsState>();
        var editEnvState = gameObject.AddComponent<EditEnvironmentState>();
        var viewModeState = gameObject.AddComponent<ViewModeState>();
        
        StateMachine.States.Add(buildingState);
        StateMachine.States.Add(animationsState);
        StateMachine.States.Add(editEnvState);
        StateMachine.States.Add(viewModeState);

       StateMachine.CurrentState = buildingState;
   //     StateMachine.CurrentState = editEnvState;
//        StateMachine.CurrentState = viewModeState;

        DontDestroy = FindObjectOfType<DontDestroy>();
        
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
                    EnvironmentResourcesManager.Instance.ResourcesObjects.Find(x => x.name == element.objectName),
                    new Vector3(element.X_Position, element.Y_Position, element.Z_Position),
                    new Quaternion(element.X_Rotation, element.Y_Rotation, element.Z_Rotation, element.W_Rotation));
                
                go.name = go.name.Replace("(Clone)", "");
                go.AddComponent<EnvironmentObject>();
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
