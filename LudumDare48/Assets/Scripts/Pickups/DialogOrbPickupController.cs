using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogOrbPickupController : MonoBehaviour
{

    public enum ChoiceType 
    {
        GOOD,
        OKAY,
        BAD
    }

    public ChoiceType choiceType = ChoiceType.BAD;

    // How high the pickup bobs
    public float bobDistance = 0.38f;
    // How fast the pickup bobs
    public float bobSpeed = 2.99f;

    // How far above powerup floating text should be
    public Vector3 floatingTextOffset = new Vector3(0f, 1f, 0f);

    public GameObject floatingTextPrefab;

    // Update to set the floating text above the powerup
    public string dialog = "Press [E] to pickup";

    private GameObject floatingText;
    private float originalY;
    private bool isTextVisible;
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        this.originalY = transform.position.y;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        InitializeFloatingText();
        InitializeColor();
    }

    // Update is called once per frame
    void Update()
    {
        FloatAnimation();
        TextFacePlayer(camera.transform.position);
    }

    void FloatAnimation()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(bobSpeed * Time.time) * bobDistance),
            transform.position.z);
    }

    void InitializeFloatingText()
    {
        floatingText = Instantiate(floatingTextPrefab, transform.position + floatingTextOffset, Quaternion.identity, transform);
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        textMesh.text = dialog;
        HideFloatingText();
    }

    void TextFacePlayer(Vector3 playerPos)
    {
        if (isTextVisible)
        {
            floatingText.transform.LookAt(2 * floatingText.transform.position - playerPos);
        }
        
    }

    void InitializeColor()
    {
        switch (choiceType)
        {
            case ChoiceType.GOOD:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                break;
            }
            case ChoiceType.OKAY:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                break;
            }
            case ChoiceType.BAD:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            }
            default:
            {
                break;
            }
            

        }
    }

    public void PickupDialogChoice()
    {
        int currentScene = GameState.CurrentScene;
        int choice;
        switch (choiceType)
        {
            case ChoiceType.GOOD:
                {
                    choice = 3;
                    break;
                }
            case ChoiceType.OKAY:
                {
                    choice = 2;
                    break;
                }
            case ChoiceType.BAD:
                {
                    choice = 1;
                    break;
                }
            default:
                {
                    choice = 0;
                    break;
                }


        }

        GameState.UpdateChoice(currentScene, choice);
    }

    public void ShowFloatingText()
    {
        isTextVisible = true;
        floatingText.SetActive(true);
    }

    public void HideFloatingText()
    {
        isTextVisible = false;
        floatingText.SetActive(false);
    }

    public void SetChoiceType(ChoiceType type)
    {
        choiceType = type;
        InitializeColor();
    }

    public static ChoiceType GetChoiceTypeFromIndex(int index)
    {
        switch (index)
        {
            case 1:
                {
                    return ChoiceType.BAD;
                }
            case 2:
                {
                    return ChoiceType.OKAY;
                }
            case 3:
                {
                    return ChoiceType.GOOD;
                }
            default:
                {
                    return ChoiceType.BAD;
                }
        }
    }

}
