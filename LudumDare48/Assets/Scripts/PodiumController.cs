using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumController : MonoBehaviour
{

    // How far above powerup floating text should be
    public Vector3 floatingTextOffset = new Vector3(0f, 2f, 0f);

    public GameObject floatingTextPrefab;
    // Update to set the floating text above the powerup
    public string cantOpenDoorText = "Can't open door";
    public string openDoorText = "Press [E] to open door";

    private TextMesh textMesh;
    private GameObject floatingText;
    private bool isTextVisible;
    private GameObject player;

    private RoomManager roomManager;

    private bool doorIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        InitializeFloatingText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeFloatingText()
    {
        floatingText = Instantiate(floatingTextPrefab, transform.position + floatingTextOffset, Quaternion.identity, transform);
        textMesh = floatingText.GetComponent<TextMesh>();
        textMesh.text = cantOpenDoorText;
        textMesh.fontSize = 300;
        HideFloatingText();
    }

    void TextFacePlayer(Vector3 playerPos)
    {
        if (isTextVisible)
        {
            floatingText.transform.LookAt(2 * floatingText.transform.position - playerPos);
        }

    }

    public void ShowFloatingText()
    {
        if (!doorIsOpen)
        {
            if (GameState.CanOpenDoor())
            {
                textMesh.text = openDoorText;
            }
            isTextVisible = true;
            floatingText.SetActive(true);
        }
        
    }

    public void HideFloatingText()
    {
        isTextVisible = false;
        floatingText.SetActive(false);
    }

    public void OpenDoor()
    {
        if (GameState.CanOpenDoor())
        {
            roomManager.OpenDoor();
            doorIsOpen = true;
            HideFloatingText();
        }
        
    }
}