using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesButton : MonoBehaviour
{
    public int id;

    private DontDestroy dntDsrt;
    
    private void Start()
    {
        dntDsrt = FindObjectOfType<DontDestroy>();
    }

    public void LoadScene()
    {
        dntDsrt.SceneId = "scene" + id;
        SceneManager.LoadScene(1);
    }
}
