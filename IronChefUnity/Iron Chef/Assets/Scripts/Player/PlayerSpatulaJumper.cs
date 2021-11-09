using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpatulaJumper : MonoBehaviour
{
    public float maxJumpHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Jump(Vector3 target, float time, float jumpHeight = -1)
    {
        StartCoroutine(jumpTick(target, time, jumpHeight));
    }

    private IEnumerator jumpTick(Vector3 target, float time, float jumpHeight)
    {
        if (jumpHeight < 1)
            jumpHeight = maxJumpHeight;
        GetComponent<CharacterMover>().enabled = false;
        float cTime = 0;
        float yOffset = -4 * jumpHeight / Mathf.Pow(time, 2);
        Vector3 startPos = transform.position;
        while (cTime < time)
        {

            transform.position = startPos;
            transform.position = Vector3.Lerp(startPos, target, cTime / time);
            float yAmount = yOffset * cTime * (cTime - time);
            transform.position = new Vector3(transform.position.x, transform.position.y + yAmount, transform.position.z);

            yield return null;
            cTime += Time.deltaTime;
        }
        transform.position = target;
        GetComponent<CharacterMover>().enabled = true;

    }
}
