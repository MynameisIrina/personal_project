using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadCutScene : MonoBehaviour
{
    private PlayableDirector director;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera cutSceneCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject npcFemale;
    
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCamera.enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            playerController.enabled = false;
            director.Play();
            StartCoroutine(FinishCut());
        }
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(12);
        mainCamera.enabled = true;
        cutSceneCamera.enabled = false;
        playerController.enabled = true;
        npcFemale.SetActive(false);


    }
}
