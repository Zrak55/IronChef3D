using UnityEngine;

[CreateAssetMenu(fileName = "NewAppliance", menuName = "Player Powers/New Appliance", order = 2)]
public class PlayerApplianceScriptable : ScriptableObject
{
    public ApplianceName applianceName;

    public string description;

    public float[] values;

    



    public enum ApplianceName
    {
        Fridge
    }
}