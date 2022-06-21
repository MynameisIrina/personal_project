using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDestination : MonoBehaviour
{
    private int destinationCounter;
    [SerializeField] private List<Vector3> destinations;

    [SerializeField] private AINavigationFemale _aiNavigationFemale;
    // Start is called before the first frame update
    void Start()
    {
        destinationCounter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            // if (destinationCounter == 3)
            // {
            //     this.gameObject.transform.position = new Vector3(153, 3, 637);
            //     destinationCounter = -1;
            // }
            if (destinationCounter == 4)
            {
                this.gameObject.transform.position = new Vector3(186 ,3, 375);
                destinationCounter = -1;
            }
            else if (destinationCounter == 3)
            {
                this.gameObject.transform.position = new Vector3(186 ,3, 355);
                destinationCounter = 4;
            }
            else if (destinationCounter == 2)
            {
                this.gameObject.transform.position = new Vector3(186 ,3, 325);
                destinationCounter = 3;
            }
            else if (destinationCounter == 1)
            {
                this.gameObject.transform.position = new Vector3(186,3,301);
                destinationCounter = 2;
            }
            else if (destinationCounter == 0)
            {
                this.gameObject.transform.position = new Vector3(184,3,282);
                destinationCounter = 1;
            }
            
            
            
        }
            
    }
}
