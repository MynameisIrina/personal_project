using UnityEngine;

public class OpenNote : MonoBehaviour
{
    private PauseControl pauseControl;
    [SerializeField] private GameObject note;
    [SerializeField] private PlayerController playerController;
    private bool showNote;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pauseControl = FindObjectOfType<PauseControl>();
        note.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("show note " + showNote);
        if (showNote)
        {
            if (playerController.pickUp)
            {
                note.GetComponent<Canvas>().enabled = true;
                PauseControl.isPaused = true;
                pauseControl.PauseGame();
            }

            if (playerController.putAway)
            {
                note.GetComponent<Canvas>().enabled = false;
                PauseControl.isPaused = false;
                pauseControl.PauseGame();
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            showNote = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            showNote = false;
        }
    }
}
