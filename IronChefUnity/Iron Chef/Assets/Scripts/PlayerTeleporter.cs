using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    static bool currentlyTeleporting = false;
    [SerializeField] private Transform output;
    [SerializeField] private Transform inputPot;
    [SerializeField] private Transform outputFinal;
    [SerializeField] private Image blackFadeImage;
    Animator playerAnimator;
    CharacterController character;

    void Start()
    {
        character = FindObjectOfType<CharacterMover>().GetComponent<CharacterController>();
        playerAnimator = character.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(currentlyTeleporting == false)
            {
                StartCoroutine(movePlayer());
            }
        }
    }

    void PlayCharacterEnterAnim()
    {
        playerAnimator.SetTrigger("EnterTeleport");
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("EnterPotLayer"), 1);

    }

    void PlayCharacterExitAnim()
    {
        playerAnimator.SetTrigger("ExitTeleport");
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("EnterPotLayer"), 1);
    }

    void SetAnimatorBack()
    {
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("EnterPotLayer"), 0);
    }

    IEnumerator movePlayer()
    {
        currentlyTeleporting = true;
        float fadeSpeed = 3;
        float cTime = 0;
        float m_height = 5f;
        float height = -4 * m_height / Mathf.Pow(0.75f, 2);
        bool finalPos = false;

        Vector3 start = character.transform.position;
        var cm = character.GetComponent<CharacterMover>();

        //Character Off, Play enter animatior
        IronChefUtils.TurnOffCharacter();
        character.enabled = false;
        PlayCharacterEnterAnim();
        blackFadeImage.color = new Color(0, 0, 0, 0);

        while (cTime < 1.5f)
        {
            if(cTime <= 0.75f)
            {
                float yAmount = height * cTime * (cTime - 0.75f);
                character.transform.position = Vector3.Lerp(start, inputPot.position, cTime / 0.75f) + (Vector3.up * yAmount);
            }
            else if(finalPos == false)
            {
                character.transform.position = inputPot.position;
                finalPos = true;
            }

            if(cTime >= 0.5f)
                blackFadeImage.color = new Color(0, 0, 0, Mathf.Clamp(blackFadeImage.color.a + fadeSpeed * Time.deltaTime, 0, 1));

            yield return null;
            cTime += Time.deltaTime;
        }

        character.transform.LookAt(transform.position + (outputFinal.transform.position - output.transform.position));
        character.transform.rotation = Quaternion.Euler(0, character.transform.rotation.eulerAngles.y, 0);
        cm.model.transform.localRotation = Quaternion.Euler(0,0,0);
        cm.targetRotation = character.transform.rotation;


        //teleport
        character.GetComponent<PlayerSpatulaJumper>().Jump(output.position, 0.1f, false);


        //Play exit animation
        PlayCharacterExitAnim();
        yield return new WaitForSeconds(0.25f);

        finalPos = false;
        cTime = 0;
        while (cTime < 1.25f)
        {
            if (cTime <= 0.75f)
            {
                float yAmount = height * cTime * (cTime - 0.75f);

                character.transform.position = Vector3.Lerp(output.position, outputFinal.position, cTime / 0.75f) + (Vector3.up * yAmount);

            }
            else
            {
                if(finalPos == false)
                {
                    finalPos = true;
                    character.transform.position = outputFinal.position;
                }
            }



            if (cTime >= 0.25f)
                blackFadeImage.color = new Color(0, 0, 0, Mathf.Clamp(blackFadeImage.color.a - fadeSpeed * Time.deltaTime, 0, 1));

            yield return null;
            cTime += Time.deltaTime;
        }


        //Character On
        character.enabled = true;
        IronChefUtils.TurnOnCharacter();
        SetAnimatorBack();

        currentlyTeleporting = false;
    }
}
