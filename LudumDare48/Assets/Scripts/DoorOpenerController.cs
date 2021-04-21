using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenerController : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            //TODO, PLAY OPENING SOUND
            gameManager.PlayDoorNoise();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
