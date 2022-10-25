
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private GameObject originalArrow;
    [SerializeField] private uint speed;
    [SerializeField] private Vector3 target;
    [SerializeField] private Camera aimCamera;
    [SerializeField] private GameObject crosshair;
    private Vector3 nullVector = new Vector3(0, 0, 0);
    private RaycastHit hit;
    [SerializeField] private Transform mockObject;

    
    void Update()
    {
        if (target != nullVector)
        {
            Vector2 screenPos = new Vector2(Screen.width /2.0f, Screen.height/2.0f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, aimCamera.WorldToScreenPoint(), speed * Time.deltaTime, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void SetTarget(Vector3 tar)
    {
        target = tar;
    }
    
    
    
    
}