using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerStats stats;

    public Slider hpBar;
    public Slider staminaBar;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();
    }


    private void UpdateBars()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    private void UpdateHPBar()
    {
        hpBar.value = stats.CurrentHP / stats.MaximumHP;
    }
    private void UpdateStaminaBar()
    {
        staminaBar.value = stats.CurrentStamina / stats.MaximumStamina;
    }

}
