using UnityEngine;
using UnityEngine.SceneManagement;

//class to control the enable of the box collider of the player to only work in menu 
public class PlayerColliderController : MonoBehaviour
{
    public BoxCollider menuCollider; 
    public string menuSceneName = "MenuScene"; 

    private void Start()
    {
        UpdateColliderState(SceneManager.GetActiveScene());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateColliderState(scene);
    }

    private void UpdateColliderState(Scene scene)
    {
        if (scene.name == menuSceneName)
        {
            menuCollider.enabled = true;
        }
        else
        {
            menuCollider.enabled = false;
        }
    }
}
