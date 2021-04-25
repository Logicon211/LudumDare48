using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{
    public AudioClip[] sounds;

    public float soundsCooldown = 10f;

    public float currentSoundsTime = 10f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSoundsTime -= Time.deltaTime;

        if(currentSoundsTime <= 0) {
            currentSoundsTime = soundsCooldown;

            AudioClip chosenReporterClip = sounds[Random.Range(0, sounds.Length)];
            audioSource.PlayOneShot(chosenReporterClip);
        }
    }
}
