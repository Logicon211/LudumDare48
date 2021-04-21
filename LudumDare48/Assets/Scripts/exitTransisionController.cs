using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitTransisionController : MonoBehaviour
{
    GameManager gameController;
    CraigController craig;
    
    public bool isFirstTransition = false;
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
            craig.endTransition();
            gameController.disableLowPassFilter();

            if(!isFirstTransition) {
                gameController.PlayShopMusic();
            }            

            gameObject.SetActive(false);
        }
    }
}
