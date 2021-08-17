using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IronChefUtils
{
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
            inputDirection = new Vector3((inputDirection.x * Mathf.Cos(Mathf.Deg2Rad * angle)) - (inputDirection.z * Mathf.Sin(Mathf.Deg2Rad * angle)), 0, (inputDirection.x * Mathf.Sin(Mathf.Deg2Rad * angle)) + (inputDirection.z * Mathf.Cos(Mathf.Deg2Rad * angle)));
        }
        return inputDirection;
    }
}
