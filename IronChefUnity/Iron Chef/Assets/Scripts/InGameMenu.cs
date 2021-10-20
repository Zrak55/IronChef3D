using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [Space]
    public GameObject firstSelectButton;

    bool paused = false;

    private void Awake()
    {
        if(player == null)
            player = FindObjectOfType<CharacterMover>();
        if(playerCam == null)
            playerCam = FindObjectOfType<PlayerCameraSetup>();

        LoadSettingsSliderValues();
        SetSettingsListeners();
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
        IronChefUtils.TurnOffCharacter();
        Menu.SetActive(true);
        GoToMenu(0);

        EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }
    void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        Menu.SetActive(false);
        IronChefUtils.TurnOnCharacter();

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
        EventSystem.current.SetSelectedGameObject(MenuItem[i].GetComponentInChildren<Button>().gameObject);
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
        Time.timeScale = 1;
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
