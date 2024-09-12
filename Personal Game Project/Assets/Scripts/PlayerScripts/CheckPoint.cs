using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CharacterController player;
    private CameraController camera;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] Transform currentCheckPointTransform;

    [Header("Check Points")] [SerializeField] private GameObject point1, point2, point3;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        camera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarManager.getHealthAmount() < 0.1f)
        {
            player.enabled = false;
            player.transform.position = currentCheckPointTransform.position;
            player.transform.rotation = currentCheckPointTransform.rotation;
            camera.ResetCamera();
            player.enabled = true;
            healthBarManager.ReloadHealthBar();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint1"))
        {
            currentCheckPointTransform = point1.transform;
            
        }
        else if (other.CompareTag("checkpoint2"))
        {
            currentCheckPointTransform = point2.transform;
        }
        else if (other.CompareTag("checkpoint3"))
        {
            currentCheckPointTransform = point3.transform;
        }
    }
}
