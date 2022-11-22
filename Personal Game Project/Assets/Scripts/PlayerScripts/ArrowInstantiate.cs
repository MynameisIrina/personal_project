using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ArrowInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private GameObject originalArrow;
    //[SerializeField] private Transform arrowSpawnPos;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Camera aimCamera;
    [SerializeField] private Transform mockObject;
    private GameObject arrowinst;
    private RaycastHit hit;
    private Vector3 hitPoint;
    private Vector3 target;
    private bool fireInput;
    private bool fire;
    private bool aimInput;
    private Vector3 worldPos;

    private void Start()
    {
        fire = false;
    }

    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            mockObject.position = hit.point;
            worldPos = hit.point;
            Debug.Log("Hit point: " + hit.point);
        }
    }

    /*
     * The Arrow is spawn when the right button is triggered (see InputManager).
     */

    public void SpawnArrow()
    {
        
        
        GameObject arrowInstantiate =
            Instantiate(arrowPrefab, spawnPosition.position, spawnPosition.rotation);
        arrowInstantiate.transform.localScale = new Vector3(2, 2, 8f);
        if (Physics.Raycast(aimCamera.transform.position, aimCamera.transform.right, out hit, 1000f))
        {
            Debug.Log("HITTING: " + hit.collider.gameObject.name);
            //arrowInstantiate.GetComponent<ArrowMove>().SetTarget(hit.point);
        
        }
    }


    public void ReceiveAimInput(bool aim_input)
    {
        aimInput = aim_input;
    }

    public void ReceiveFireInput(bool fire_input)
    {
        fireInput = fire_input;
    }

    public GameObject GetOriginalArrow()
    {
        return originalArrow;
    }


}
