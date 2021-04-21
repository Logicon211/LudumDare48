using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hobo : MonoBehaviour
{
    
    // Prefab for powerup overlay
    public GameObject dialogOverlay;

    // Actual overlay used by the object
    private GameObject overlay;
    private SpriteRenderer overlaySpriteRenderer;

    private float overlayVerticalOffset = 12f;
    private float overlayScale = 4f;

    public int voiceIndex = 0;
    private bool voiceLineRead = false;

    private AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[7];
    
    // Start is called before the first frame update
    void Start()
    {
        overlay = InstantiateOverlay();
        audioSource = GetComponent<AudioSource>();
    }

    private GameObject InstantiateOverlay()
    {
        Vector3 overlayPosition = transform.position + new Vector3(0f, overlayVerticalOffset);
        GameObject overlay = Instantiate(dialogOverlay, overlayPosition, Quaternion.identity);
        overlay.transform.localScale = new Vector3(overlayScale, overlayScale);
        overlaySpriteRenderer = overlay.GetComponent<SpriteRenderer>();
        overlaySpriteRenderer.enabled = false;
        return overlay;
    }
    
    private void EnablePowerUpOverlay()
    {
        overlaySpriteRenderer.enabled = true;
    }

    private void DisablePowerUpOverlay()
    {
        overlaySpriteRenderer.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnablePowerUpOverlay();
            if (!voiceLineRead)
            {
                audioSource.PlayOneShot(audioClips[voiceIndex]);
                voiceLineRead = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DisablePowerUpOverlay();
        }
    }
}
