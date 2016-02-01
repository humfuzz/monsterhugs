using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {
    private float gameOverTime;
    private float maxCameraHeight = 10f;
    private float maxRadius = 10f;
    private bool cameraTrigger = false;
    private bool gameOver = false;
    private Transform camera;
    private Transform playerPosition;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Player>().isDead && !gameOver)
        {
            cameraTrigger = true;
            gameOver = true;
            gameOverTime = Time.time;
            playerPosition = player.transform;
            camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        if  (cameraTrigger) {
            player.GetComponent<FirstPersonController>().enabled = false;
            float time = Time.time - gameOverTime;
            float height = Mathf.Min(0.25f * time, maxCameraHeight);
            float radius = Mathf.Min(0.25f * time + 2.5f, maxRadius);
            float spinSpeed = 0.25f * time;
            camera.position = new Vector3(radius * Mathf.Sin(spinSpeed) + playerPosition.position.x, height, radius * Mathf.Cos(spinSpeed) + playerPosition.position.z);
            camera.LookAt(playerPosition);
            Destroy(GameObject.Find("weapon"));
            Destroy(GameObject.Find("weaponEnd"));
        }
	}
}
