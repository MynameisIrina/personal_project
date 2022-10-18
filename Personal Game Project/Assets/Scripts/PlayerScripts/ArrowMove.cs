
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private GameObject originalArrow;
    [SerializeField] private uint speed;
    [SerializeField] private Vector3 target;
    private Vector3 nullVector = new Vector3(0, 0, 0);

    
    void Update()
    {
        if (target != nullVector)
        {
            Debug.Log("Target: " + target);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
    }

    public void SetTarget(Vector3 tar)
    {
        target = tar;
    }
    
    
}