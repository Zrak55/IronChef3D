using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpatulaJumper : MonoBehaviour
{
    public float maxJumpHeight;
    public GameObject spatulaJumpEffects;
    // Start is called before the first frame update
    void Start()
    {

        spatulaJumpEffects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Jump(Vector3 target, float time, bool specialHoldInPlace, float jumpHeight = -1)
    {
        StartCoroutine(jumpTick(target, time, jumpHeight, specialHoldInPlace));
    }

    private IEnumerator jumpTick(Vector3 target, float time, float jumpHeight, bool specialHoldInPlace)
    {
        spatulaJumpEffects.SetActive(true);
        GetComponentInChildren<Animator>().SetBool("SpatulaJumping", true);
        GetComponentInChildren<Animator>().SetLayerWeight(GetComponentInChildren<Animator>().GetLayerIndex("Roll Layer"), 0);
        GetComponent<PlayerAttackController>().canAct = false;

        if (jumpHeight < 1)
            jumpHeight = maxJumpHeight;
        GetComponent<CharacterMover>().enabled = false;
        float cTime = 0;
        float yOffset = -4 * jumpHeight / Mathf.Pow(time, 2);
        Vector3 startPos = transform.position;
        cTime += Time.deltaTime;
        while (cTime < time)
        {
            transform.position = Vector3.Lerp(startPos, target, cTime / time);
            float yAmount = yOffset * cTime * (cTime - time);
            transform.position = new Vector3(transform.position.x, transform.position.y + yAmount, transform.position.z);

            yield return null;
            cTime += Time.deltaTime;
        }
        transform.position = target;
        GetComponent<CharacterMover>().enabled = true;

        if(specialHoldInPlace)
        {
            for(int i = 0; i < 20; i++)
            {
                transform.position = target;
                yield return null;
            }
        }

        GetComponentInChildren<Animator>().SetBool("SpatulaJumping", false);
        GetComponent<PlayerAttackController>().canAct = true;

        spatulaJumpEffects.SetActive(false);
    }
}
