using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupInteractionController : MonoBehaviour
{

    public float interactRange = 3f;
    public LayerMask layerMask = new LayerMask();

    private DialogOrbPickupController lookingAt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * interactRange;
        Debug.DrawRay(transform.position, forward, Color.yellow);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, forward, out hit, interactRange, layerMask))
        {
            if (!lookingAt)
            {
                lookingAt = hit.transform.gameObject.GetComponent<DialogOrbPickupController>();
                lookingAt.ShowFloatingText();
            }
            //else if (lookingAt && "Dialog" != hit.transform.tag)
            //{
            //    lookingAt.HideFloatingText();
            //    lookingAt = null;
            //}
        }
        else
        {
            if (lookingAt)
            {
                lookingAt.HideFloatingText();
                lookingAt = null;
            }
        }
    }
}
