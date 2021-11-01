// GENERATED AUTOMATICALLY FROM 'Assets/InputMaps/IronChefControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @IronChefControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @IronChefControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""IronChefControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""c095c614-9c38-4027-b6d7-af62a2bc211f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ab73c5e3-55cb-426a-b501-a7e902b81fe1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4062c8f3-0767-41cf-aa0a-a12a3cf94b87"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraControllerCheck"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ecadbbec-1154-434f-a5e6-b6504448e8f6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""80fb8638-2802-44ad-9a68-f306011e6748"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""b68cd4ca-2ec5-44df-a9d0-3d3fc308bfcf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""2fc9b839-8874-4e9a-94e9-85bb7538adae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BasicAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d459cac4-92f9-46ad-a6f5-365cd2eedeea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapBasicWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""746fba80-55a3-4e5b-b584-ffe76a27ae63"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FryingPan"",
                    ""type"": ""Button"",
                    ""id"": ""7d0c6a7f-31e5-4666-b688-79a65a18f971"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePower"",
                    ""type"": ""Button"",
                    ""id"": ""feb2ab15-5077-4252-89b9-1e022b621357"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""1237cfa8-6081-4e94-a864-374170af295a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Eat"",
                    ""type"": ""Button"",
                    ""id"": ""013c189d-304a-4888-b6f3-09b7651ed83d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeEat"",
                    ""type"": ""Value"",
                    ""id"": ""7eb80eb3-ad80-4337-8fde-ccd0a529644f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeEatTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""3dd945e5-ecc9-4222-bf8e-ab2dccec8731"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""d8765792-4e90-4799-b646-1eab40ecc6f5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bd72ee82-fe1b-429a-8df5-c21a8a126245"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""81c92b9e-d8e0-4e66-93fe-608a7996843b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d55d84c9-b5ac-429a-8be8-210f40acf659"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""34bab4a6-898d-46b0-8835-c543fa7e1c30"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99741e05-ba14-4ed0-a25f-645a054cb4e0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44995aa1-6950-4c16-b13b-9deea6336271"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ac78e45f-e409-484f-a6e6-f13fe279020f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""71323b7c-080f-4de0-be2f-00b95b992d18"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e071836a-1a59-43d2-af68-2439e95127a6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""898b6342-48c5-413d-b78d-b46b2c7d6b4b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""36fe26fa-df3f-48b4-90c6-4a9736a0c20a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""350f0d86-098d-4112-8312-8be40836e0fe"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ae4391be-054d-40cf-867e-82d8bf3c0ad7"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a469766a-8b92-4e61-99b0-dbd3225acfb5"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a68d15d-a303-41b2-b5c0-d26ced50a302"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""877060c5-9b3d-4883-81b9-c7eb3fd2dffc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8241d0d-ead1-41e1-9763-a0ca9544a510"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c8c8f20-98b7-41c6-a361-0d39ad603a57"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e44a5d30-1bed-488b-808c-a78774b244bc"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""408e93c2-7461-48f6-b493-45576f170fe6"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28ec9e4b-8ceb-4cb2-8bcb-a26cb52052c3"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapBasicWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""186d0d1f-c08b-441e-9102-19b5e3881969"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapBasicWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4de02d3c-9aa3-429d-b3bf-24a36cdadd64"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a634618-2008-4c1b-ba2c-09b8b66c1bab"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4084d310-c794-445f-a2ec-181523469c58"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FryingPan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a695b87a-a38d-4d1e-a1ea-7729cd053ae8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FryingPan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1d6537b-2cf9-45d2-a863-b663d50bcb25"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5896cde4-96d7-4ccb-ab42-39a62571ac54"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7b509bc-a61c-4725-85e3-84feb74356d4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46ec5741-fedf-4cbf-893b-3acc7c038be4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c52d01b-34fd-4ae8-811f-975381745c37"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Eat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a61ad684-b3cd-4184-91d2-9eb097cccdbb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Eat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""c1697ad1-4754-43d2-b724-4d7134adf9ce"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bd458132-9798-4bcf-b0cd-823363dd44f1"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8f907f08-c2cf-4335-a4c3-27962bed86b0"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""c6f2d759-576c-418f-8835-bf4c0b58ce0a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bfefa315-d59d-4119-bba8-f583acdafc5c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a593c098-d9ec-416c-bd88-bd060d4dec69"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9aa87043-169c-49ed-bf58-1296bb9411e4"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEatTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0d2de17-a736-49fb-9d33-5211985f9816"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEatTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""301f7684-1332-4fcb-b646-88e600074945"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEatTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b6ac1db-71ed-4aa0-9b0a-541504e4c99a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeEatTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1903b5f8-4439-40a5-9d3c-d2640962f9f6"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraControllerCheck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_RotateCamera = m_Gameplay.FindAction("RotateCamera", throwIfNotFound: true);
        m_Gameplay_RotateCameraControllerCheck = m_Gameplay.FindAction("RotateCameraControllerCheck", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Roll = m_Gameplay.FindAction("Roll", throwIfNotFound: true);
        m_Gameplay_Sprint = m_Gameplay.FindAction("Sprint", throwIfNotFound: true);
        m_Gameplay_BasicAttack = m_Gameplay.FindAction("BasicAttack", throwIfNotFound: true);
        m_Gameplay_SwapBasicWeapon = m_Gameplay.FindAction("SwapBasicWeapon", throwIfNotFound: true);
        m_Gameplay_FryingPan = m_Gameplay.FindAction("FryingPan", throwIfNotFound: true);
        m_Gameplay_UsePower = m_Gameplay.FindAction("UsePower", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Eat = m_Gameplay.FindAction("Eat", throwIfNotFound: true);
        m_Gameplay_ChangeEat = m_Gameplay.FindAction("ChangeEat", throwIfNotFound: true);
        m_Gameplay_ChangeEatTrigger = m_Gameplay.FindAction("ChangeEatTrigger", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_RotateCamera;
    private readonly InputAction m_Gameplay_RotateCameraControllerCheck;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Roll;
    private readonly InputAction m_Gameplay_Sprint;
    private readonly InputAction m_Gameplay_BasicAttack;
    private readonly InputAction m_Gameplay_SwapBasicWeapon;
    private readonly InputAction m_Gameplay_FryingPan;
    private readonly InputAction m_Gameplay_UsePower;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Eat;
    private readonly InputAction m_Gameplay_ChangeEat;
    private readonly InputAction m_Gameplay_ChangeEatTrigger;
    public struct GameplayActions
    {
        private @IronChefControls m_Wrapper;
        public GameplayActions(@IronChefControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @RotateCamera => m_Wrapper.m_Gameplay_RotateCamera;
        public InputAction @RotateCameraControllerCheck => m_Wrapper.m_Gameplay_RotateCameraControllerCheck;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Roll => m_Wrapper.m_Gameplay_Roll;
        public InputAction @Sprint => m_Wrapper.m_Gameplay_Sprint;
        public InputAction @BasicAttack => m_Wrapper.m_Gameplay_BasicAttack;
        public InputAction @SwapBasicWeapon => m_Wrapper.m_Gameplay_SwapBasicWeapon;
        public InputAction @FryingPan => m_Wrapper.m_Gameplay_FryingPan;
        public InputAction @UsePower => m_Wrapper.m_Gameplay_UsePower;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Eat => m_Wrapper.m_Gameplay_Eat;
        public InputAction @ChangeEat => m_Wrapper.m_Gameplay_ChangeEat;
        public InputAction @ChangeEatTrigger => m_Wrapper.m_Gameplay_ChangeEatTrigger;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @RotateCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @RotateCameraControllerCheck.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraControllerCheck;
                @RotateCameraControllerCheck.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraControllerCheck;
                @RotateCameraControllerCheck.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraControllerCheck;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Roll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Sprint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @BasicAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @SwapBasicWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapBasicWeapon;
                @SwapBasicWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapBasicWeapon;
                @SwapBasicWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwapBasicWeapon;
                @FryingPan.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPan;
                @FryingPan.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPan;
                @FryingPan.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPan;
                @UsePower.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @UsePower.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @UsePower.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Eat.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEat;
                @Eat.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEat;
                @Eat.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEat;
                @ChangeEat.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEat;
                @ChangeEat.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEat;
                @ChangeEat.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEat;
                @ChangeEatTrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEatTrigger;
                @ChangeEatTrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEatTrigger;
                @ChangeEatTrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeEatTrigger;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @RotateCameraControllerCheck.started += instance.OnRotateCameraControllerCheck;
                @RotateCameraControllerCheck.performed += instance.OnRotateCameraControllerCheck;
                @RotateCameraControllerCheck.canceled += instance.OnRotateCameraControllerCheck;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @BasicAttack.started += instance.OnBasicAttack;
                @BasicAttack.performed += instance.OnBasicAttack;
                @BasicAttack.canceled += instance.OnBasicAttack;
                @SwapBasicWeapon.started += instance.OnSwapBasicWeapon;
                @SwapBasicWeapon.performed += instance.OnSwapBasicWeapon;
                @SwapBasicWeapon.canceled += instance.OnSwapBasicWeapon;
                @FryingPan.started += instance.OnFryingPan;
                @FryingPan.performed += instance.OnFryingPan;
                @FryingPan.canceled += instance.OnFryingPan;
                @UsePower.started += instance.OnUsePower;
                @UsePower.performed += instance.OnUsePower;
                @UsePower.canceled += instance.OnUsePower;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Eat.started += instance.OnEat;
                @Eat.performed += instance.OnEat;
                @Eat.canceled += instance.OnEat;
                @ChangeEat.started += instance.OnChangeEat;
                @ChangeEat.performed += instance.OnChangeEat;
                @ChangeEat.canceled += instance.OnChangeEat;
                @ChangeEatTrigger.started += instance.OnChangeEatTrigger;
                @ChangeEatTrigger.performed += instance.OnChangeEatTrigger;
                @ChangeEatTrigger.canceled += instance.OnChangeEatTrigger;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnRotateCameraControllerCheck(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnBasicAttack(InputAction.CallbackContext context);
        void OnSwapBasicWeapon(InputAction.CallbackContext context);
        void OnFryingPan(InputAction.CallbackContext context);
        void OnUsePower(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnEat(InputAction.CallbackContext context);
        void OnChangeEat(InputAction.CallbackContext context);
        void OnChangeEatTrigger(InputAction.CallbackContext context);
    }
}
