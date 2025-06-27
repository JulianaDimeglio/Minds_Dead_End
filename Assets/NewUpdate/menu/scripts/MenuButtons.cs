using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header("Paneles de UI")]
    public GameObject mainMenu;
    public GameObject controlsPanel;

    [Header("Nombre de la escena de juego")]
    public string sceneToLoad = "GameScene";

    // play
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // prototipo
    public void LoadPrototype()
    {
        SceneManager.LoadScene("PrototypeScene"); 
    }

    // quit
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // controls
    public void ShowControls()
    {
        mainMenu.SetActive(false);
        controlsPanel.SetActive(true);
    }

    // back
    public void BackToMenu()
    {
        controlsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }
}