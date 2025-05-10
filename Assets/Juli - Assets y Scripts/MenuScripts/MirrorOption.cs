using UnityEngine;

public class MirrorOption : MonoBehaviour
{
    public MenuOptionType optionType;  
    public MenuManager menuManager;    

    void OnTriggerEnter(Collider other)
    {
        
            if ( other.GetType() == typeof(BoxCollider))
            {
                ExecuteOption();
            }
        
    }

    public void ExecuteOption()
    {
        switch (optionType)
        {
            case MenuOptionType.Play:
                menuManager.LoadScene("AgusScene");  
                break;
            case MenuOptionType.Credits:
                menuManager.ShowCredits(); 
                break;
            case MenuOptionType.Quit:
                menuManager.QuitGame();  
                break;
            default:
                Debug.LogError("Invalid option");
                break;
        }
    }
}
public enum MenuOptionType { Play, Credits, Quit }
