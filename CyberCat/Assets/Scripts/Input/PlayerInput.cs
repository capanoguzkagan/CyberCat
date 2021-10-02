// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""DialogInputController"",
            ""id"": ""6b1a32c2-3be9-4b02-a637-47d20e85bc0f"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""Value"",
                    ""id"": ""fde0df9c-777a-4443-83dd-bbfc62a6a7db"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard"",
                    ""type"": ""Button"",
                    ""id"": ""b7780d35-f7dc-440f-b8e7-3f6661398269"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0ceae0e0-fb55-48fa-b9d3-46fe089a2a06"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acf72d84-3380-4bd0-ac5a-9649925d2a3b"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerMovementController"",
            ""id"": ""d87d888e-ff86-4ed0-816d-6c6bccb8428d"",
            ""actions"": [
                {
                    ""name"": ""PlayerMovementControl"",
                    ""type"": ""Value"",
                    ""id"": ""0a592ac2-e92e-47af-ab47-7a840825eb01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae00aaaa-2804-4675-82d4-ed3ca6f9673e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovementControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DialogInputController
        m_DialogInputController = asset.FindActionMap("DialogInputController", throwIfNotFound: true);
        m_DialogInputController_Touch = m_DialogInputController.FindAction("Touch", throwIfNotFound: true);
        m_DialogInputController_Keyboard = m_DialogInputController.FindAction("Keyboard", throwIfNotFound: true);
        // PlayerMovementController
        m_PlayerMovementController = asset.FindActionMap("PlayerMovementController", throwIfNotFound: true);
        m_PlayerMovementController_PlayerMovementControl = m_PlayerMovementController.FindAction("PlayerMovementControl", throwIfNotFound: true);
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

    // DialogInputController
    private readonly InputActionMap m_DialogInputController;
    private IDialogInputControllerActions m_DialogInputControllerActionsCallbackInterface;
    private readonly InputAction m_DialogInputController_Touch;
    private readonly InputAction m_DialogInputController_Keyboard;
    public struct DialogInputControllerActions
    {
        private @PlayerInput m_Wrapper;
        public DialogInputControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_DialogInputController_Touch;
        public InputAction @Keyboard => m_Wrapper.m_DialogInputController_Keyboard;
        public InputActionMap Get() { return m_Wrapper.m_DialogInputController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogInputControllerActions set) { return set.Get(); }
        public void SetCallbacks(IDialogInputControllerActions instance)
        {
            if (m_Wrapper.m_DialogInputControllerActionsCallbackInterface != null)
            {
                @Touch.started -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnTouch;
                @Keyboard.started -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnKeyboard;
                @Keyboard.performed -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnKeyboard;
                @Keyboard.canceled -= m_Wrapper.m_DialogInputControllerActionsCallbackInterface.OnKeyboard;
            }
            m_Wrapper.m_DialogInputControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @Keyboard.started += instance.OnKeyboard;
                @Keyboard.performed += instance.OnKeyboard;
                @Keyboard.canceled += instance.OnKeyboard;
            }
        }
    }
    public DialogInputControllerActions @DialogInputController => new DialogInputControllerActions(this);

    // PlayerMovementController
    private readonly InputActionMap m_PlayerMovementController;
    private IPlayerMovementControllerActions m_PlayerMovementControllerActionsCallbackInterface;
    private readonly InputAction m_PlayerMovementController_PlayerMovementControl;
    public struct PlayerMovementControllerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMovementControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMovementControl => m_Wrapper.m_PlayerMovementController_PlayerMovementControl;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovementController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementControllerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementControllerActions instance)
        {
            if (m_Wrapper.m_PlayerMovementControllerActionsCallbackInterface != null)
            {
                @PlayerMovementControl.started -= m_Wrapper.m_PlayerMovementControllerActionsCallbackInterface.OnPlayerMovementControl;
                @PlayerMovementControl.performed -= m_Wrapper.m_PlayerMovementControllerActionsCallbackInterface.OnPlayerMovementControl;
                @PlayerMovementControl.canceled -= m_Wrapper.m_PlayerMovementControllerActionsCallbackInterface.OnPlayerMovementControl;
            }
            m_Wrapper.m_PlayerMovementControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMovementControl.started += instance.OnPlayerMovementControl;
                @PlayerMovementControl.performed += instance.OnPlayerMovementControl;
                @PlayerMovementControl.canceled += instance.OnPlayerMovementControl;
            }
        }
    }
    public PlayerMovementControllerActions @PlayerMovementController => new PlayerMovementControllerActions(this);
    public interface IDialogInputControllerActions
    {
        void OnTouch(InputAction.CallbackContext context);
        void OnKeyboard(InputAction.CallbackContext context);
    }
    public interface IPlayerMovementControllerActions
    {
        void OnPlayerMovementControl(InputAction.CallbackContext context);
    }
}
