using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public Light lightSource;
    public GameObject door;

    private AudioSource audioSource;

    private bool isDoorOpen;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        if(!isDoorOpen) {
            audioSource.Play();
            door.GetComponent<Animation>().Play("open");
            lightSource.enabled = true;
            isDoorOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && isDoorOpen) {
            SceneManager.LoadScene(GameState.CurrentScene + 1);
        }

    }
}
