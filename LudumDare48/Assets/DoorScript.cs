using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Light lightSource;
    public GameObject door;

    private bool isDoorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        if(!isDoorOpen) {
            Debug.Log("Trigger was player");
            door.GetComponent<Animation>().Play("open");
            lightSource.enabled = true;
            isDoorOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("HIT");
        if (other.CompareTag("Player") && isDoorOpen) {
            //TODO: Load next room with GameManager
        }

    }
}
