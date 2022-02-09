using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCanvas : MonoBehaviour
{
    public EnemyHitpoints hp;
    public Slider hpSlider;
    public GameObject dmgNumber;
    public float animTime;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    private void OnDestroy()
    {
        if (hpSlider != null)
            hpSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerCameraSetup.pcamSetup.cam.transform);
        IronChefUtils.moveABar(hpSlider, hp.GetPercentHP());
    }

    public void MakeDamageNumber(float value)
    {
        var go = Instantiate(dmgNumber, transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString("0.0");
        Destroy(go, animTime);
    }
}
