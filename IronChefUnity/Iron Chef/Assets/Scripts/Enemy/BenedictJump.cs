using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenedictJump : MonoBehaviour
{
    public EnemyBasicAttackbox jumpHitbox;
    public BenedictBehavior behavior;
    public float maxJumpHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponent<BenedictBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginJumping(float time)
    {
        //TODO: Delay
        LaunchJump(FindObjectOfType<CharacterMover>().transform.position, time);
    }

    private void LaunchJump(Vector3 target, float time)
    {
        StartCoroutine(jumpTick(target, time));
    }
    private IEnumerator jumpTick(Vector3 target, float time)
    {
        float cTime = 0;
        float yOffset = -4 * maxJumpHeight / Mathf.Pow(time, 2);
        Vector3 startPos = transform.position;
        jumpHitbox.HitOn();
        while(cTime < time)
        {
            transform.position = startPos;
            transform.position = Vector3.Lerp(startPos, target, cTime / time);
            float yAmount = yOffset * cTime * (cTime - time);
            transform.position = new Vector3(transform.position.x,transform.position.y + yAmount, transform.position.z);

            yield return null;
            cTime += Time.deltaTime;
        }
        transform.position = target;
        DoneJumping();
    }

    public void DoneJumping()
    {
        jumpHitbox.HitOff();
    }
}
