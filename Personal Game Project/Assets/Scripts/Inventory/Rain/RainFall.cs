using UnityEngine;

public class RainFall : MonoBehaviour
{
    private Transform playerTransform;
    public float HeightDiff;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + HeightDiff, playerTransform.position.z);
    }
}
