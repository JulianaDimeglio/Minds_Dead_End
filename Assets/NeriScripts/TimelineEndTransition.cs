using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimelineEndTransition : MonoBehaviour
{
    public PlayableDirector director;
    public string nextSceneName;

    void Start()
    {
        if (director != null)
        {
            director.stopped += OnTimelineStopped;
        }
    }

    void OnTimelineStopped(PlayableDirector pd)
    {
        if (pd == director)
        {
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1.5f); 
        SceneManager.LoadScene(nextSceneName);
    }
}