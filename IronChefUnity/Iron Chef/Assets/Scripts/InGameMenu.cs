using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject Menu;

    public CharacterMover player;
    public PlayerCameraSetup playerCam;

    bool paused = false;

    private void Awake()
    {
        if(player == null)
            player = FindObjectOfType<CharacterMover>();
        if(playerCam == null)
            playerCam = FindObjectOfType<PlayerCameraSetup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        
        if(paused)
        {
            paused = false;
            Time.timeScale = 1;
            player.enabled = true;
            playerCam.CanMoveCam = true;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            player.enabled = false;
            playerCam.CanMoveCam = false;
        }
    }
}
