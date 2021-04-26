using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public DoorScript door;
    public float initialTime = 10f;
    public BackgroundSounds backgroundSounds;
    
    private AudioSource audioSource;
    public AudioClip badPickupSound;
    public AudioClip okayPickupSound;
    public AudioClip goodPickupSound;

    private AudioLowPassFilter lowPassFilter;
    private AudioReverbFilter reverbFilter;
    // Start is called before the first frame update
    void Start()
    {
        GameState.CurrentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        reverbFilter = GetComponent<AudioReverbFilter>();
        GameState.ResetChoice(GameState.CurrentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        door.OpenDoor();
        if (backgroundSounds != null) {
            backgroundSounds.enabled = false;
        }
    }

    public float getInitialTime() {
        return initialTime;
    }

    public void PlayPickupSound(int pickupId) {
        if(!audioSource.isPlaying) {
            lowPassFilter.enabled = false;
            reverbFilter.enabled = false;
            if (pickupId == 0) {
                audioSource.PlayOneShot(badPickupSound);
            } else if(pickupId == 1) {
                audioSource.PlayOneShot(okayPickupSound);
            } else if(pickupId == 2) {
                audioSource.PlayOneShot(goodPickupSound);
            }
        }
    }
}
