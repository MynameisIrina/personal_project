using UnityEngine;

public class CheckPointObserver : MonoBehaviour
{
    [SerializeField] GameObject player;
    private CameraController camera;
    private HealthBarManager healthBarManager;
    private CharacterController characterController;
    [SerializeField] Transform currentCheckPointTransform;
    
    void Start()
    {
        healthBarManager = player.GetComponent<HealthBarManager>();
        characterController = player.GetComponent<CharacterController>();
        camera = FindObjectOfType<CameraController>();
    }
    

    public void GoToLastCheckpoint()
    {
        if (healthBarManager.GetHealthAmount() < 0.1f)
        {
            characterController.enabled = false;
            player.transform.position = currentCheckPointTransform.position;
            player.transform.rotation = currentCheckPointTransform.rotation;
            camera.ResetCamera();
            characterController.enabled = true;
            healthBarManager.ReloadHealthBar();
        }
    }
    

    public void UpdateCurrentCheckpoint(Transform checkPointTransform)
    {
        currentCheckPointTransform = checkPointTransform;
    }
}
