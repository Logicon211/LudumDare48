using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogOrbPickup : MonoBehaviour
{

    // How high the powerup bobs
    public float bobStrength = 0.38f;

    // How fast the powerup bobs
    public float bobSpeed = 2.99f;

    float originalY;

    // Start is called before the first frame update
    void Start()
    {
        this.originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    // Floating animation
    void Float()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(bobSpeed * Time.time) * bobStrength),
            transform.position.z);
    }
}
