using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    private string currentScene;
    private PlayableDirector director;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        director = GetComponent<PlayableDirector>();
        director.Play();
    }

    private void Update()
    {
        if (currentScene == "CutScene02")
        {
            if (director.state == PlayState.Paused)
            {
                SceneManager.LoadScene("GamePlay01");
            }
        }

        if (currentScene == "CutScene01")
        {
            if (director.state == PlayState.Paused)
            {
                SceneManager.LoadScene("GamePlay01");
            }
            
        }
        
    }
}
