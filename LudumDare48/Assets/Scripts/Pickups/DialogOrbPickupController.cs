using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogOrbPickupController : MonoBehaviour
{

    // How high the powerup bobs
    public float bobStrength = 0.38f;

    // How far above powerup floating text should be
    public Vector3 floatingTextOffset = new Vector3(0f, 1f, 0f);

    public GameObject floatingTextPrefab;

    // How fast the powerup bobs
    public float bobSpeed = 2.99f;

    // Update to set the floating text above the powerup
    //public string dialog = "Here's some dialog";

    //private GameObject floatingText;
    private float originalY;
    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.originalY = transform.position.y;
        //player = GameObject.FindGameObjectWithTag("Player");
        //InitializeFloatingText();
    }

    // Update is called once per frame
    void Update()
    {
        FloatAnimation();
        
    }

    // Floating animation
    void FloatAnimation()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(bobSpeed * Time.time) * bobStrength),
            transform.position.z);
    }

    //void InitializeFloatingText()
    //{
    //    floatingText = Instantiate(floatingTextPrefab, transform.position + floatingTextOffset, Quaternion.identity, transform);
    //    TextMesh textMesh = floatingText.GetComponent<TextMesh>();
    //    textMesh.text = dialog;
    //    floatingText.SetActive(false);
    //}

    //public void TextFacePlayer(Vector3 playerPos)
    //{
    //    floatingText.transform.LookAt(2 * floatingText.transform.position - playerPos);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    floatingText.SetActive(true);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    floatingText.SetActive(false);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    TextFacePlayer(player.transform.position);
    //}
}
