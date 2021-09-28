using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPreLevel", menuName = "Levels/New Prelevel Info", order = 2)]
public class PreLevelScriptable : ScriptableObject
{
    public string LevelName;
    public List<string> ingredients;
    public string dishName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
