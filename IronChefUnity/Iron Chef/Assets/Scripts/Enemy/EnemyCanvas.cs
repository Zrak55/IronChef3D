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
    Transform player;

    public bool DeactivateOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMover>().transform;
        if(DeactivateOnStart)
            gameObject.SetActive(false);
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
        var go = Instantiate(dmgNumber, (transform.position + player.position)/2, Quaternion.identity);
        go.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString("0.0");
        Destroy(go, animTime);
    }

    public void SwapState()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

}
