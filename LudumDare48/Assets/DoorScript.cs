using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public Light lightSource;
    public GameObject door;

    public bool finalDoor = false;

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
            if(!finalDoor) {
                SceneManager.LoadScene(GameState.CurrentScene + 1);
            } else {
                // TODO: Pick Correct scene. Bad +1, Mid +2, Good +3
                SceneManager.LoadScene(GameState.CurrentScene + 1);
            }
        }

    }
}
