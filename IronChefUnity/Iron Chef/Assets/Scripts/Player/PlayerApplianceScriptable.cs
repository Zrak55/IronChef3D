using UnityEngine;

[CreateAssetMenu(fileName = "NewAppliance", menuName = "Player Powers/New Appliance", order = 2)]
public class PlayerApplianceScriptable : ScriptableObject
{
    public ApplianceName applianceName;





    public enum ApplianceName
    {
        Fridge
    }
}