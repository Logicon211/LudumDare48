using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public DoorScript door;
    public float initialTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GameState.CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        door.OpenDoor();
    }

    public float getInitialTime() {
        return initialTime;
    }
}
