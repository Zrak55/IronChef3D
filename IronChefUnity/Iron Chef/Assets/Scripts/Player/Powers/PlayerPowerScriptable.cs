using UnityEngine;

[CreateAssetMenu(fileName = "NewPower", menuName = "Player Powers/New Player Power", order = 1)]
public class PlayerPowerScriptable : ScriptableObject
{
    public string DisplayName;

    public PowerName powerName;

    public string description;
    public string briefDescription;

    public float cooldown;
    public int animationNumber;

    public float[] values;
    [Tooltip("Only used for readability")]
    public string[] valueDescriptions;
    public GameObject[] prefabs;

    public float powerAnimDuration;

    public enum PowerName
    {
        Molapeno,
        CarbUp,
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