using UnityEngine;

[CreateAssetMenu(fileName = "NewPower", menuName = "Player Powers/New Player Power", order = 1)]
public class PlayerPowerScriptable : ScriptableObject
{
    public PowerName powerName;

    public string description;

    public float cooldown;
    public int animationNumber;

    public float[] values;
    [Tooltip("Only used for readability")]
    public string[] valueDescriptions;
    public GameObject[] prefabs;



    public enum PowerName
    {
        Molapeno,
        BreadTrap,
        SpearOfDesticheese,
        PortableLunch,
        Ham_mer,
        _50CheeseStrike,
        Catapasta,
        IceTray,
        SugarRush,
        Glockamole
    }
}