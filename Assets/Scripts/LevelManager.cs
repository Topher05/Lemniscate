using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameOver gameOver;
    public GameObject playerPref;
    public Transform spawnPosition;
    public CinemachineVirtualCamera playerCamera;
    public int sceneToLoad;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPref, spawnPosition.position, Quaternion.identity);
        playerCamera.Follow = player.transform;
    }

    public void GameOver(){
        gameOver.Setup();
    }


    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerHealth>().Health <= 0){
            GameOver();
        }
    }
}
