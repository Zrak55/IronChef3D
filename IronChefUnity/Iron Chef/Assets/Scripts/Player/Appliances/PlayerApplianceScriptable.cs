using UnityEngine;

[CreateAssetMenu(fileName = "NewAppliance", menuName = "Player Powers/New Appliance", order = 2)]
public class PlayerApplianceScriptable : ScriptableObject
{
    public ApplianceName applianceName;

    public string description;

    public float[] values;
    [Tooltip("No real effect, readability only")]
    public string[] valueDescriptions;

    



    public enum ApplianceName
    {
        Fridge,
        CookingOil,
        CheeseGrater,
        Stove,
        Grill,
        Blender,
        Freezer,
        WardenGamseysHarshInstructions
    }
}