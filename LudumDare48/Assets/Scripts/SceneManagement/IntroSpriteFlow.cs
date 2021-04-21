using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroSpriteFlow : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public Sprite[] sprite;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private int sceneEnding = 0;
	private SpriteRenderer spriteRenderer;

	public AudioSource audioSource;
	public AudioClip jingle;

	public int levelToLoadIndex;
	public int slideToStopMusicAndJingle = 0;

	private GameManager gameManager;

	public GameObject blackOutSquare;

	void Start ()
	{
		//Screen.SetResolution (1400, 900, true);
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (GameObject.FindWithTag("GameController") != null)
			gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

	void Awake ()
	{
		blackOutSquare.SetActive(true);
	}
	
	
	void Update ()
	{
		foreach (Touch touch in Input.touches) {
			sceneEnding++;
		}

		if (Input.anyKeyDown) {//.GetKeyDown(KeyCode.RightArrow)) {
			if(sceneEnding < sprite.Length){
				sceneEnding++;
				//Stop music and play jingle;
				if (sceneEnding == slideToStopMusicAndJingle && audioSource != null) {
					Debug.Log ("JINGLE");
					audioSource.Stop();
					audioSource.PlayOneShot (jingle);
				}
			}
			//Application.LoadLevel();
		}

		// If the scene is starting...
		if (sceneStarting) {
			// ... call the StartScene function.
			StartScene ();
		}

		//Sprite image navigation
		if (sceneEnding == sprite.Length) {
			EndScene ();
		} else {
			spriteRenderer.sprite = sprite[sceneEnding];
		}

	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		if(Time.deltaTime < 0.1f) {
			blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
			// GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
		} else {
			blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.black, fadeSpeed * Time.deltaTime);
			// GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.black, fadeSpeed * Time.deltaTime);
		}
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		float prevAlpha = blackOutSquare.GetComponent<Image>().color.a;

		blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.black, fadeSpeed * Time.deltaTime);

		float currAlpha = blackOutSquare.GetComponent<Image>().color.a;

		if(currAlpha - prevAlpha < 0.05f) {
			Color mask = blackOutSquare.GetComponent<Image>().color;
			blackOutSquare.GetComponent<Image>().color = new Color(mask.r, mask.g, mask.b, mask.a +0.05f);
		}
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(blackOutSquare.GetComponent<Image>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			blackOutSquare.GetComponent<Image>().color = Color.clear;
			blackOutSquare.GetComponent<Image>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		blackOutSquare.GetComponent<Image>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(blackOutSquare.GetComponent<Image>().color.a >= 0.95f) {
			// ... reload the level.
			//Application.LoadLevel(levelToLoad);
			// if (gameManager != null)
			// 	gameManager.LoadScene(levelToLoadIndex);
			// else
				LoadingScreenManager.LoadScene(levelToLoadIndex);
			
		}
	}
}