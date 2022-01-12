using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public EnemyBasicAttackbox jumpHitbox;
    public KnockPlayerAway jumpKnockbox;
    public EnemyBehaviorTree behavior;
    [Tooltip("Float for how high the enemy will jump")]
    [SerializeField] public float maxJumpHeight;
    [Tooltip("Float for how long it will take the enemy to jump")]
    [SerializeField] public float time;
    public Collider collider;


    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponent<EnemyBehaviorTree>();
    }

    public void BeginJumping(float time)
    {
        //TODO: Delay
        LaunchJump(FindObjectOfType<CharacterMover>().transform.position, time);
    }

    private void LaunchJump(Vector3 target, float time)
    {
        Physics.IgnoreCollision(collider, FindObjectOfType<CharacterController>().GetComponent<Collider>(), true);
        foreach (var c in FindObjectOfType<CharacterMover>().GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, c, true);
        }

        StartCoroutine(jumpTick(target, time));
    }
    private IEnumerator jumpTick(Vector3 target, float time)
    {
        bool hitOn = false;
        float cTime = 0;
        float yOffset = -4 * maxJumpHeight / Mathf.Pow(time, 2);
        Vector3 startPos = transform.position;
        while (cTime < time)
        {
            if (cTime >= time / 2 && hitOn == false)
            {

                jumpHitbox.HitOn();
                jumpKnockbox.HitOn();
                hitOn = true;
            }

            transform.position = startPos;
            transform.position = Vector3.Lerp(startPos, target, cTime / time);
            float yAmount = yOffset * cTime * (cTime - time);
            transform.position = new Vector3(transform.position.x, transform.position.y + yAmount, transform.position.z);

            yield return null;
            cTime += Time.deltaTime;
        }
        transform.position = target;
        DoneJumping();

    }

    public void DoneJumping()
    {
        FindObjectOfType<PlayerCamControl>().ShakeCam(5, 1.5f);

        Physics.IgnoreCollision(collider, FindObjectOfType<CharacterController>().GetComponent<Collider>(), false);
        foreach (var c in FindObjectOfType<CharacterMover>().GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, c, false);
        }

        jumpHitbox.HitOff();
        jumpKnockbox.HitOff();
    }
}
