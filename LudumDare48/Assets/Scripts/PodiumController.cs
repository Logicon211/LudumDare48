using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumController : MonoBehaviour
{

    // How far above powerup floating text should be
    public Vector3 floatingTextOffset = new Vector3(0f, 2f, 0f);

    public float bobDistance = 0.38f, bobSpeed = 2.99f;
    public float rotationSpeed = 50f;

    public GameObject floatingTextPrefab;
    // Update to set the floating text above the powerup
    public string cantOpenDoorText = "Can't open door";
    public string openDoorText = "Press [E] to open door";

    public GameObject diamond;

    private TextMesh textMesh;
    private GameObject floatingText;
    private bool isTextVisible;
    private GameObject player;
    private float originalDiamondY;
    

    private RoomManager roomManager;
    private CraigsChoiceInHand craigsHand;

    private bool doorIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        craigsHand = GameObject.Find("CraigsChoice").GetComponent<CraigsChoiceInHand>();
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        InitializeFloatingText();
        this.originalDiamondY = diamond.transform.position.y;
        HideDiamond();
    }

    // Update is called once per frame
    void Update()
    {
        TextFacePlayer(player.transform.position);
        FloatAnimation();
        if (!doorIsOpen && GameState.CanOpenDoor())
        {
            ShowDiamond();
        }
    }

    void FloatAnimation()
    {
        diamond.transform.position = new Vector3(diamond.transform.position.x,
            originalDiamondY + ((float)System.Math.Sin(bobSpeed * Time.time) * bobDistance),
            diamond.transform.position.z);
        diamond.transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f, Space.World);
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

    public void ShowDiamond()
    {
        diamond.SetActive(true);
    }

    public void HideDiamond()
    {
        diamond.SetActive(false);
    }

    public void OpenDoor()
    {
        if (GameState.CanOpenDoor())
        {
            roomManager.OpenDoor();
            doorIsOpen = true;
            HideFloatingText();
            HideDiamond();
            craigsHand.DisableRenderer();
        }
        
    }
}
