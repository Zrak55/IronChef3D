using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public static class IronChefUtils
{
    public static readonly float InfiniteDuration = -999;






    //Converts the x/z properties of a Vector3 to a Vector2
    public static Vector2 convertV3toV2(Vector3 inVector)
    {
        return new Vector2(inVector.x, inVector.z);
    }
    
    
    //Converts a Vector2 into the x/z properties of a Vector3, leaving the y component as 0
    public static Vector3 convertV2toV3(Vector2 inVector)
    {
        return new Vector3(inVector.x, 0, inVector.y);
    }

    //rotates an input vector so that an input of (0,0,1) will become the target forward.
    //Use case: rotate the input direction of the player's movement to match the camera's facing
    public static Vector3 RotateFlatVector3(Vector3 inputDirection, Vector3 targetForward)
    {
        var V2tar = convertV3toV2(targetForward);
        var angle = Vector2.Angle(new Vector2(0, 1), V2tar);
        if(angle > 0)
        {
            if(V2tar.x > 0)
            {
                angle = 360f - angle;
            }
            inputDirection = new Vector3((inputDirection.x * Mathf.Cos(Mathf.Deg2Rad * angle)) - 
                (inputDirection.z * Mathf.Sin(Mathf.Deg2Rad * angle)), 0, (inputDirection.x * Mathf.Sin(Mathf.Deg2Rad * angle)) + (inputDirection.z * Mathf.Cos(Mathf.Deg2Rad * angle)));
        }
        return inputDirection;
    }

    public static bool MouseOnly()
    {
        return Gamepad.all.Count == 0;
        
    }

    public static bool IsCharacterOn()
    {
        return GameObject.FindObjectOfType<CharacterMover>().enabled;
    }

    public static Vector3 GetSoftLockDirection(Vector3 forward, Vector3 origin, LayerMask desiredLayer, float searchDegree, bool matchForwardY = true)
    {
        Vector3 newFacing = forward;

        float closestEnemyDist = Mathf.Infinity;

        Vector3 newDirection;
        RaycastHit hit;


        for (int i = (int)(-searchDegree/2); i <= searchDegree/2; i++)
        {
            newDirection = new Vector3((forward.x * Mathf.Cos(Mathf.Deg2Rad * i) - forward.z * Mathf.Sin(Mathf.Deg2Rad * i)), forward.y, (forward.x * Mathf.Sin(Mathf.Deg2Rad * i)) + (forward.z * Mathf.Cos(Mathf.Deg2Rad * i)));
            if (Physics.Raycast(origin, newDirection, out hit, 50, desiredLayer))
            {
                if(Vector3.Distance(origin, hit.point) < closestEnemyDist)
                {
                    newFacing = newDirection;
                    closestEnemyDist = Vector3.Distance(origin, hit.point);
                }
            }

        }

        if(matchForwardY)
            newFacing.y = 0;

        newFacing = newFacing.normalized * forward.magnitude;
        return newFacing;
    }

    public static Vector3 RotateUpByDegree(Vector3 forward, float angle)
    {
        forward = forward.normalized;

        float Distance = Vector2.Distance(Vector2.right, new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)));

        forward = Vector3.MoveTowards(forward, Vector3.up, Distance);

        return forward;

    }


    public static void HideMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public static bool MouseIsHidden()
    {
        return Cursor.visible == false && Cursor.lockState == CursorLockMode.Locked;
    }

    public static void AddSlow(EnemySpeedController enemy, float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        bool alreadyThere = false;
        foreach (var speedMod in enemy.Modifiers)
        {
            if (speedMod.effectName == effectName)
            {
                if (duration > speedMod.duration)
                {
                    alreadyThere = true;
                    speedMod.duration = duration;
                    break;
                }
            }
        }
        if (!alreadyThere)
        {
            var slow = MakeSlowEffector(amount, duration, effectName);
            enemy.Modifiers.Add(slow);
        }
    }
    public static void AddSlow(PlayerSpeedController player, float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        bool alreadyThere = false;
        foreach (var speedMod in player.Modifiers)
        {
            if (speedMod.effectName == effectName)
            {
                if (duration > speedMod.duration)
                {
                    alreadyThere = true;
                    speedMod.duration = duration;
                    break;
                }
            }
        }
        if (!alreadyThere)
        {
            var slow = MakeSlowEffector(amount, duration, effectName);
            player.Modifiers.Add(slow);
        }
    }
    public static void AddSpeedUp(PlayerSpeedController player, float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        bool alreadyThere = false;
        foreach (var speedMod in player.Modifiers)
        {
            if (speedMod.effectName == effectName)
            {
                alreadyThere = true;
                if (duration > speedMod.duration)
                {
                    speedMod.duration = duration;
                    break;
                }
            }
        }
        if (!alreadyThere)
        {
            var speed = MakeSpeedEffector(amount, duration, effectName);
            player.Modifiers.Add(speed);
        }
    }
    public static void AddSpeedUp(EnemySpeedController player, float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        bool alreadyThere = false;
        foreach (var speedMod in player.Modifiers)
        {
            if (speedMod.effectName == effectName)
            {
                alreadyThere = true;
                if (duration > speedMod.duration)
                {
                    speedMod.duration = duration;
                    break;
                }
            }
        }
        if (!alreadyThere)
        {
            var speed = MakeSpeedEffector(amount, duration, effectName);
            player.Modifiers.Add(speed);
        }
    }

    public static SpeedEffector MakeSlowEffector(float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        SpeedEffector slow = new SpeedEffector();
        slow.duration = duration;
        slow.percentAmount = -amount;
        slow.effectName = effectName;
        return slow;
    }
    public static SpeedEffector MakeSpeedEffector(float amount, float duration, SpeedEffector.EffectorName effectName)
    {
        SpeedEffector slow = new SpeedEffector();
        slow.duration = duration;
        slow.percentAmount = amount;
        slow.effectName = effectName;
        return slow;
    }
    public static List<GameObject> GetCastHits(float width, float height, float depth, Vector3 location, Quaternion rotation, string Layer = "Hitbox")
    {
        var newGO = GameObject.Instantiate(new GameObject(), location, rotation);
        var col = newGO.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = new Vector3(width, depth, height);
        var list = GetCastHits(col, Layer);
        GameObject.Destroy(newGO);
        return list;
    }

    public static List<GameObject> GetCastHits(float radius, Vector3 position, string Layer = "Hitbox")
    {
        var h = Physics.OverlapSphere(position, radius, 1 << LayerMask.NameToLayer(Layer));
        List<GameObject> hits = new List<GameObject>();
        foreach(var cols in h)
        {
            hits.Add(cols.gameObject);
        }
        return hits;
    }

    public static List<GameObject> GetCastHits(Collider col, string Layer = "Hitbox")
    {


        Collider[] hits = { };
        if (col is BoxCollider)
            hits = Physics.OverlapBox(col.transform.position, (col as BoxCollider).size / 2, col.transform.rotation, 1 << LayerMask.NameToLayer(Layer));
        else if (col is CapsuleCollider)
        {
            var capcol = col as CapsuleCollider;
            var direction = new Vector3 { [capcol.direction] = 1 };
            var offset = capcol.height / 2 - capcol.radius;
            var localPoint0 = capcol.center - direction * offset;
            var localPoint1 = capcol.center + direction * offset;
            var point0 = capcol.transform.TransformPoint(localPoint0);
            var point1 = capcol.transform.TransformPoint(localPoint1);
            hits = Physics.OverlapCapsule(point0, point1, capcol.radius, 1 << LayerMask.NameToLayer(Layer));
        }
        else if (col is SphereCollider)
            hits = Physics.OverlapSphere(col.transform.position, (col as SphereCollider).radius, 1 << LayerMask.NameToLayer(Layer));

        List<GameObject> HitGameObjects = new List<GameObject>();
        foreach (var c in hits)
        {
            HitGameObjects.Add(c.gameObject);
        }

        return HitGameObjects;



    }

    public static void TurnOffCharacter()
    {
        GameObject.FindObjectOfType<CharacterMover>().MouseOff = false;
        GameObject.FindObjectOfType<CharacterMover>().enabled = false;
        GameObject.FindObjectOfType<PlayerCamControl>().CanMoveCam(false);
        GameObject.FindObjectOfType<PlayerAttackController>().canAct = false;
        ShowMouse();

    }
    public static void TurnOnCharacter()
    {
        GameObject.FindObjectOfType<CharacterMover>().MouseOff = true;
        GameObject.FindObjectOfType<CharacterMover>().enabled = true;
        GameObject.FindObjectOfType<PlayerCamControl>().CanMoveCam(true);
        GameObject.FindObjectOfType<PlayerAttackController>().canAct = true;
        HideMouse();
    }

    public static void moveABar(Slider s, float target)
    {
        if (s.value != target)
        {
            float tickRate = Mathf.Clamp(Mathf.Abs(s.value - target) * Time.deltaTime * 0.5f * (s.maxValue - s.minValue), Time.deltaTime * 2, Mathf.Infinity);
            if (Mathf.Abs(target - s.value) < tickRate)
                s.value = target;
            else
            {
                if (target < s.value)
                    tickRate *= -1;
                s.value = s.value + tickRate;
            }
        }
    }
}
