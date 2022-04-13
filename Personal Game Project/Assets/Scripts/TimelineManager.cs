using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector director;


    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.Play();
    }

    private void Update()
    {
        if (director.state == PlayState.Paused)
        {
            SceneManager.LoadScene("GamePlay01");
        }
    }
}
