using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FBPPInitialLoad : MonoBehaviour
{
    public bool allInitialized = false;
    bool fbppInit = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(loadWait());
    }

    IEnumerator loadWait()
    {

        // build your config
        var config = new FBPPConfig()
        {
            SaveFileName = "KOTKT_Save.txt",
            AutoSaveData = false,
            ScrambleSaveData = true,
            EncryptionSecret = "GrilledGames"
        };
        // pass it to FBPP
        FBPP.Start(config);

        yield return new WaitUntil(() => allInitialized);

        SceneManager.LoadScene(1);
    }
}

