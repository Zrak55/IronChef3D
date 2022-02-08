using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoodDropper : MonoBehaviour
{
    public FoodType food;
    public GameObject pickupPrefab;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GiveFood(bool spawnDeathParticles)
    {
        if(spawnDeathParticles)
        {
            var pickup = Instantiate(pickupPrefab, transform.position, transform.rotation).GetComponent<EnemyFoodPickup>();
            pickup.SetFood(food);
        }
        else
        {
            LevelProgressManager.levelProgressManager.AwardFood(food);
        }

    }





    public enum FoodType
    {
        None,
        Bread,
        Cheese,
        Tomato,
        Egg,
        Grits,
        Bacon,
        Meat,
        Beans,
        Potato,
        Chips,
        Ravioli,
        IceCream,
        Fudge,
        Sprinkles,
        Cookies,
        Onion,
        Gummy
    }
}
