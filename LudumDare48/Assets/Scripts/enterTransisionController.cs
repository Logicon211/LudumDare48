using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterTransisionController : MonoBehaviour
{
    GameManager gameController;
    CraigController craig;
    
    
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        craig = GameObject.FindGameObjectWithTag("Player").GetComponent<CraigController>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            craig.startTransition();
            gameController.enableLowPassFilter();
            gameController.PlayDoorNoise();

            door.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
