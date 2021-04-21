using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool running = false;

    public int[] ninjasWaves;
    public int[] crudeCriminalsWaves;
    public int[] oilOrksWaves;
    public int numberOfWaves;
    
    public bool isFirstBossRoom = false;
    public bool isLastBossRoom = false;

    public GameObject ninja;
    public GameObject crudeCriminal;
    public GameObject oilOrk;

    public GameObject fossilFuel;
    public GameObject killJane;

    private int currentWaveNumber = -1;

    public int currentAliveEnemyCount = 0;

    public Transform[] spawnPoints;
    public Transform bossSpawnPoint;
    public GameObject nextDoor;
    public GameObject oldDoor;
    public GameObject roof;

    private GameObject player;
    private GameManager gameManager;

    private bool hasPlayedCutscene = false;
    private bool hasKilledBoss = false;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		player = GameObject.FindGameObjectWithTag("Player");
        nextDoor.SetActive(false);
        nextDoor.SetActive(true);
	}
    // Update is called once per frame
    void Update()
    {
        if(running) {
            if(!hasPlayedCutscene && (isFirstBossRoom || isLastBossRoom)) {
                hasPlayedCutscene = true;

                Quaternion rotation = Quaternion.identity;
                if(isFirstBossRoom) {
                    gameManager.PlayMidBossMusic();
                    gameManager.StartCutScene(0);
                    GameObject spawnedFossilFuel = Instantiate (fossilFuel, bossSpawnPoint.position, rotation);
                    spawnedFossilFuel.GetComponent<FossilFuel>().roomController = this;
                } else if(isLastBossRoom) {
                    gameManager.PlayFinalBossMusic();
                    gameManager.StartCutScene(1);
                    GameObject spawnedKillJane = Instantiate (killJane, bossSpawnPoint.position, rotation);
                    spawnedKillJane.GetComponent<KillJane>().roomController = this;
                }
            }
            if(currentAliveEnemyCount <=0) {
                currentWaveNumber++;

                if(currentWaveNumber >= numberOfWaves) {
                    if(hasKilledBoss || (!isFirstBossRoom && !isLastBossRoom)) {
                        OpenNextDoor();
                        running = false;
                    }
                } else {
                    
                    Quaternion rotation = Quaternion.identity;
                    //Spawn Ninjas
                    for(int i=0; i< ninjasWaves[currentWaveNumber]; i++) {
                        GameObject spawnedNinja = Instantiate (ninja, PickSpawnPointNotOnPlayer(), rotation);
                        spawnedNinja.GetComponent<Ninja>().roomController = this;
                        currentAliveEnemyCount++;
                    }

                    //Spawn Crude Criminals
                    for(int i=0; i< crudeCriminalsWaves[currentWaveNumber]; i++) {
                        GameObject spawnedCrudeCriminal = Instantiate (crudeCriminal, PickSpawnPointNotOnPlayer(), rotation);
                        spawnedCrudeCriminal.GetComponent<CrudeCriminal>().roomController = this;
                        currentAliveEnemyCount++;
                    }

                    //Spawn Orks
                    for(int i=0; i< oilOrksWaves[currentWaveNumber]; i++) {
                        GameObject spawnedCrudeCriminal = Instantiate (oilOrk, PickSpawnPointNotOnPlayer(), rotation);
                        spawnedCrudeCriminal.GetComponent<Ninja>().roomController = this;
                        currentAliveEnemyCount++;
                    }
                }
            }
        }
    }

    public Vector2 PickSpawnPointNotOnPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
		Transform closestPoint = null;
		foreach(Transform point in spawnPoints) {
			if(closestPoint == null || Vector2.Distance(point.position, player.transform.position) < Vector2.Distance(closestPoint.position, player.transform.position)) {
				closestPoint = point;
			}
		}

		Vector2 chosenPosition;
		bool spawnPointChosen = false;
		while (!spawnPointChosen) {
			chosenPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

			if(!chosenPosition.Equals(closestPoint.position)) {
				return OffsetPositionSlightly(chosenPosition);
			}
		}

		return new Vector2();
	}

    public Vector2 OffsetPositionSlightly(Vector2 point) {
        return new Vector2(point.x + Random.Range(-0.1f, 0.1f), point.y + Random.Range(-0.1f, 0.1f));

    }

    public Vector2 PickSpawnPointFurthestFromPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
		Transform furthestPoint = null;
		foreach(Transform point in spawnPoints) {
			if(furthestPoint == null || Vector2.Distance(point.position, player.transform.position) > Vector2.Distance(furthestPoint.position, player.transform.position)) {
				furthestPoint = point;
			}
		}

		return new Vector2(furthestPoint.position.x, furthestPoint.position.y);
	}

    public Vector2 PickSpawnPointNotOnPoint(Vector3 pointIn) {
		Transform closestPoint = null;
		foreach(Transform point in spawnPoints) {
			if(closestPoint == null || Vector2.Distance(point.position, pointIn) < Vector2.Distance(closestPoint.position, pointIn)) {
				closestPoint = point;
			}
		}

		Vector2 chosenPosition;
		bool spawnPointChosen = false;
		while (!spawnPointChosen) {
			chosenPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

			if(!chosenPosition.Equals(closestPoint.position)) {
				return OffsetPositionSlightly(chosenPosition);
			}
		}

		return new Vector2();
	}

    public void DecrementAliveEnemyCount() {
        currentAliveEnemyCount--;
    }

    public void KillBoss() {
        if (isFirstBossRoom) {
            hasKilledBoss = true;
        } else if(isLastBossRoom) {
            //Trigger end of the game.
            gameManager.Victory();
        }
    }

    public void OpenNextDoor() {
        nextDoor.SetActive(false);
        gameManager.PlayDoorNoise();
        //Play open door noise?
    }

    public void closePreviousDoor() {
        if(!running) {
            oldDoor.SetActive(true);
            running = true;
            gameManager.PlayDoorNoise();
            gameManager.ResumeMainMusic();
        }
    }

    public void DeactivateRoom() {
        //Close door behind player and show the roof
        nextDoor.SetActive(true);
        roof.SetActive(true);
        gameManager.PlayMainMusic();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" ) {
            DeactivateRoom();
        }
    }
}
