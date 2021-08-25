using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaximumHP = 100;
    public float CurrentHP;

    public float MaximumStamina = 100;
    public float CurrentStamina;
    public float StaminaRegenRate = 10;

    private void Awake()
    {
        CurrentHP = MaximumHP;
        CurrentStamina = MaximumStamina;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateStamina();
    }


    private void RegenerateStamina()
    {
        if(CurrentStamina < MaximumStamina)
        {
            CurrentStamina = Mathf.Clamp(CurrentStamina + (StaminaRegenRate * Time.deltaTime), 0, MaximumStamina);
        }
    }

    public bool TrySpendStamina(float amount)
    {
        if(amount <= CurrentStamina)
        {
            CurrentStamina -= amount;
            return true;
        }
        return false;
    }
}
