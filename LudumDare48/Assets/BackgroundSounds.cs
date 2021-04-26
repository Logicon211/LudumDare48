using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{
    public AudioClip[] sounds;

    public float soundsCooldown = 10f;

    public float currentSoundsTime = 10f;

    private AudioSource audioSource;
    private GameObject roomManagerObject;
    private RoomManager roomManager;

    // private AudioLowPassFilter lowPassFilter;
    // private AudioReverbFilter reverbFilter;

    // public bool lowPassEnabled = true;
    // public bool reverbEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        roomManagerObject = GameObject.FindWithTag("RoomManager");
        roomManager = roomManagerObject.GetComponent<RoomManager>();
        audioSource = roomManagerObject.GetComponent<AudioSource>();

        // lowPassFilter = roomManagerObject.GetComponent<AudioLowPassFilter>();
        // reverbFilter = roomManagerObject.GetComponent<AudioReverbFilter>();
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSoundsTime -= Time.deltaTime;

        if(currentSoundsTime <= 0) {
            currentSoundsTime = soundsCooldown;

            AudioClip chosenClip = sounds[Random.Range(0, sounds.Length)];
            if(!audioSource.isPlaying && !roomManager.pickupAudioSource.isPlaying) {
                // if (lowPassEnabled) {
                //     lowPassFilter.enabled = true;
                // } else {
                //     lowPassFilter.enabled = false;
                // }

                // if (reverbEnabled) {
                //     reverbFilter.enabled = true;
                // } else {
                //     reverbFilter.enabled = false;
                // }
               
                // audioSource.PlayOneShot(chosenReporterClip);
                audioSource.clip = chosenClip;
                audioSource.Play();
            }
        }
    }
}
