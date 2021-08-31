using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputControls
{
    private static IronChefControls _controls = new IronChefControls();
    

    public static IronChefControls controls
    {
        get
        {
            if(_controls.asset.enabled == false)
            {
                _controls.Enable();
            }
            return _controls;
        }
        set
        {
            _controls = value;
        }
    }

}
