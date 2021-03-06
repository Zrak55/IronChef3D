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
                    ""expectedControlType"": ""Vector2"",
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
                    ""name"": ""FryingPanAim"",
                    ""type"": ""Button"",
                    ""id"": ""8c8f80dd-40a0-43ba-b44d-ce40ad387c67"",
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
                    ""name"": ""UsePowerPressed"",
                    ""type"": ""Button"",
                    ""id"": ""983690bb-811c-4ede-affd-c1fad4c9b0a4"",
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
                },
                {
                    ""name"": ""SnapCameraToBack"",
                    ""type"": ""Button"",
                    ""id"": ""af6a7a8d-1e60-447c-9fe9-9e3e922b676a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dialogue"",
                    ""type"": ""Button"",
                    ""id"": ""e5b97ac9-5d92-466d-a14f-3df4ec725417"",
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
                    ""id"": ""d0d55963-cf5d-4c2b-9e58-f97f5306ec97"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""c7e81bcc-257b-4693-a3e7-c1152a2b697b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a3bfa3d5-cab8-49cb-8208-23e396cbb086"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.1,max=1)"",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0b857524-1319-4149-aa5b-313a6b716183"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.1,max=1)"",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa4df51c-ff67-4438-a80d-73410623afe0"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""50f5592e-3ab1-4130-ade3-29ed8ba5fc2d"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2aa2aa59-b11d-4612-82a8-ac96d93100f5"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.2)"",
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
                    ""path"": ""<Keyboard>/space"",
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
                    ""path"": ""<Gamepad>/buttonEast"",
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
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d0fb016-3400-4789-9b9c-95bad22a980c"",
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
                    ""interactions"": ""Press(behavior=1)"",
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
                    ""interactions"": ""Press(behavior=1)"",
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
                    ""interactions"": ""Press(behavior=1)"",
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
                    ""interactions"": ""Press(behavior=1)"",
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
                    ""path"": ""<Gamepad>/buttonWest"",
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
                    ""path"": ""<Gamepad>/dpad/left"",
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
                    ""path"": ""<Gamepad>/dpad/right"",
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
                    ""path"": ""<Gamepad>/dpad/left"",
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
                    ""path"": ""<Gamepad>/dpad/right"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""e7bebbbb-e6a2-4a1a-8048-8a98c4d2a849"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapCameraToBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fd85dd8-0379-4429-a10c-897a58b4a6cd"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapCameraToBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88aec0c6-f91d-48bc-a124-8fec3712be1d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41c2f6db-1bae-47bc-b02e-5b8ef7362494"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea727639-2a9b-45bf-9934-c5fb52656fdc"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FryingPanAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1b98cb6-6fe1-4288-bbc2-c051ac22be05"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FryingPanAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92a6f0c5-4bee-4f45-8d68-97931d916e93"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePowerPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58a49e88-e57c-4fc4-a6bf-5bb7a63a3d44"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePowerPressed"",
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
        m_Gameplay_FryingPanAim = m_Gameplay.FindAction("FryingPanAim", throwIfNotFound: true);
        m_Gameplay_UsePower = m_Gameplay.FindAction("UsePower", throwIfNotFound: true);
        m_Gameplay_UsePowerPressed = m_Gameplay.FindAction("UsePowerPressed", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Eat = m_Gameplay.FindAction("Eat", throwIfNotFound: true);
        m_Gameplay_ChangeEat = m_Gameplay.FindAction("ChangeEat", throwIfNotFound: true);
        m_Gameplay_ChangeEatTrigger = m_Gameplay.FindAction("ChangeEatTrigger", throwIfNotFound: true);
        m_Gameplay_SnapCameraToBack = m_Gameplay.FindAction("SnapCameraToBack", throwIfNotFound: true);
        m_Gameplay_Dialogue = m_Gameplay.FindAction("Dialogue", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_FryingPanAim;
    private readonly InputAction m_Gameplay_UsePower;
    private readonly InputAction m_Gameplay_UsePowerPressed;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Eat;
    private readonly InputAction m_Gameplay_ChangeEat;
    private readonly InputAction m_Gameplay_ChangeEatTrigger;
    private readonly InputAction m_Gameplay_SnapCameraToBack;
    private readonly InputAction m_Gameplay_Dialogue;
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
        public InputAction @FryingPanAim => m_Wrapper.m_Gameplay_FryingPanAim;
        public InputAction @UsePower => m_Wrapper.m_Gameplay_UsePower;
        public InputAction @UsePowerPressed => m_Wrapper.m_Gameplay_UsePowerPressed;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Eat => m_Wrapper.m_Gameplay_Eat;
        public InputAction @ChangeEat => m_Wrapper.m_Gameplay_ChangeEat;
        public InputAction @ChangeEatTrigger => m_Wrapper.m_Gameplay_ChangeEatTrigger;
        public InputAction @SnapCameraToBack => m_Wrapper.m_Gameplay_SnapCameraToBack;
        public InputAction @Dialogue => m_Wrapper.m_Gameplay_Dialogue;
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
                @FryingPanAim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPanAim;
                @FryingPanAim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPanAim;
                @FryingPanAim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFryingPanAim;
                @UsePower.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @UsePower.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @UsePower.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePower;
                @UsePowerPressed.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerPressed;
                @UsePowerPressed.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerPressed;
                @UsePowerPressed.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUsePowerPressed;
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
                @SnapCameraToBack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSnapCameraToBack;
                @SnapCameraToBack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSnapCameraToBack;
                @SnapCameraToBack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSnapCameraToBack;
                @Dialogue.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDialogue;
                @Dialogue.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDialogue;
                @Dialogue.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDialogue;
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
                @FryingPanAim.started += instance.OnFryingPanAim;
                @FryingPanAim.performed += instance.OnFryingPanAim;
                @FryingPanAim.canceled += instance.OnFryingPanAim;
                @UsePower.started += instance.OnUsePower;
                @UsePower.performed += instance.OnUsePower;
                @UsePower.canceled += instance.OnUsePower;
                @UsePowerPressed.started += instance.OnUsePowerPressed;
                @UsePowerPressed.performed += instance.OnUsePowerPressed;
                @UsePowerPressed.canceled += instance.OnUsePowerPressed;
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
                @SnapCameraToBack.started += instance.OnSnapCameraToBack;
                @SnapCameraToBack.performed += instance.OnSnapCameraToBack;
                @SnapCameraToBack.canceled += instance.OnSnapCameraToBack;
                @Dialogue.started += instance.OnDialogue;
                @Dialogue.performed += instance.OnDialogue;
                @Dialogue.canceled += instance.OnDialogue;
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
        void OnFryingPanAim(InputAction.CallbackContext context);
        void OnUsePower(InputAction.CallbackContext context);
        void OnUsePowerPressed(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnEat(InputAction.CallbackContext context);
        void OnChangeEat(InputAction.CallbackContext context);
        void OnChangeEatTrigger(InputAction.CallbackContext context);
        void OnSnapCameraToBack(InputAction.CallbackContext context);
        void OnDialogue(InputAction.CallbackContext context);
    }
}
