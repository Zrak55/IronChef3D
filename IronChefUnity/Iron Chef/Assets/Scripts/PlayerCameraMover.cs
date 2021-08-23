using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraMover : MonoBehaviour
{
    public GameObject CameraRotation;

    [SerializeField]
    private IronChefControls controls;

    private void Awake()
    {
        controls = new IronChefControls();
        controls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Enable on pause/unpause
        MouseOff();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = GetCamRotateInput();
        if(IronChefUtils.MouseOnly())
        {
            rotationAmount *= Settings.Sensitivity;
        }
        else
        {
            rotationAmount *= Settings.Sensitivity;
            rotationAmount *= 100;
        }

        rotationAmount *= Time.deltaTime;

        CameraRotation.transform.Rotate(new Vector3(0, rotationAmount, 0));
    }

    private float GetCamRotateInput()
    {
        

        return controls.Gameplay.RotateCamera.ReadValue<float>();
    }

    public void MouseOff()
    {
        if(IronChefUtils.MouseOnly())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void MouseOn()
    {
        if (IronChefUtils.MouseOnly())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
