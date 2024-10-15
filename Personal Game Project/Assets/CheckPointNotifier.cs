using UnityEngine;

public class CheckPointNotifier : MonoBehaviour
{
    private CheckPointObserver checkPointObserver;
   
    void Start()
    {
        checkPointObserver = GetComponentInParent<CheckPointObserver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointObserver.UpdateCurrentCheckpoint(this.transform);
        }
    }
}
