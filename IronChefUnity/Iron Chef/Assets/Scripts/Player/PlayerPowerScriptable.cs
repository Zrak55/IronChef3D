using UnityEngine;

[CreateAssetMenu(fileName = "NewPower", menuName = "Player Powers/New Player Power", order = 1)]
public class PlayerPowerScriptable : ScriptableObject
{
    public PowerName powerName;
    public float cooldown;
    public int animationNumber;

    public float[] values;
    public GameObject[] prefabs;



    public enum PowerName
    {
        Molapeno
    }
}