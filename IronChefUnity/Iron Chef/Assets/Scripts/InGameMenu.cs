using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject[] MenuItem;


    [Space]
    public AudioMixer audioM;

    [Header("Settings")]
    public Slider mainVolSlider;
    public Slider musicVolSlider;
    public Slider fxVolSlider;
    public Slider sensitivitySlider;
    public Toggle invertVerticalCam;

    [Space]

    public CharacterMover player;
    public PlayerCameraSetup playerCam;

    bool paused = false;

    private void Awake()
    {
        if(player == null)
            player = FindObjectOfType<CharacterMover>();
        if(playerCam == null)
            playerCam = FindObjectOfType<PlayerCameraSetup>();

        LoadSettingsSliderValues();
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

    void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        player.enabled = false;
        playerCam.CanMoveCam = false;
        Menu.SetActive(true);
        IronChefUtils.ShowMouse();
        player.GetComponent<PlayerAttackController>().canAct = false;
        GoToMenu(0);
    }
    void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        player.enabled = true;
        playerCam.CanMoveCam = true;
        Menu.SetActive(false);
        player.GetComponent<PlayerAttackController>().canAct = true;
        IronChefUtils.HideMouse();

    }

    void CheckPause()
    {
        if (InputControls.controls.Gameplay.Pause.triggered)
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Unpause();
    }
    public void GoToMenu(int i)
    {
        foreach(var m in MenuItem)
        {
            m.SetActive(false);
        }
        MenuItem[i].SetActive(true);
    }

    void SetSettingsListeners()
    {
        mainVolSlider.onValueChanged.AddListener(delegate
        {
            SetMainVolume();
        });

        musicVolSlider.onValueChanged.AddListener(delegate
        {
            SetMusicVolume();
        });

        fxVolSlider.onValueChanged.AddListener(delegate
        {
            SetSoundFXVolume();
        });

        sensitivitySlider.onValueChanged.AddListener(delegate
        {
            SetSensitivity();
        });

        invertVerticalCam.onValueChanged.AddListener(delegate
        {
            SetInvertVerticalCam();
        });
    }

    public void PlayGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void LoadSettingsSliderValues()
    {
        mainVolSlider.value = Settings.MainVolume;
        musicVolSlider.value = Settings.MusicVolume;
        fxVolSlider.value = Settings.SoundFXVolume;
        sensitivitySlider.value = Settings.Sensitivity;
        invertVerticalCam.isOn = Settings.InvertVerticalCam;
    }

    public void SetMainVolume()
    {
        Settings.MainVolume = mainVolSlider.value;
    }
    public void SetMusicVolume()
    {
        Settings.MusicVolume = musicVolSlider.value;
    }
    public void SetSoundFXVolume()
    {
        Settings.SoundFXVolume = fxVolSlider.value;
    }
    public void SetSensitivity()
    {
        Settings.Sensitivity = sensitivitySlider.value;
    }
    public void SetInvertVerticalCam()
    {
        Settings.InvertVerticalCam = invertVerticalCam.isOn;
    }
}
