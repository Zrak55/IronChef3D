using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FBPPInitialLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // build your config
        var config = new FBPPConfig()
        {
            SaveFileName = "my-save-file.txt",
            AutoSaveData = false,
            ScrambleSaveData = true,
            EncryptionSecret = "GrilledGames"
        };
        // pass it to FBPP
        FBPP.Start(config);

        SceneManager.LoadScene(1);
    }

}
