// GENERATED AUTOMATICALLY FROM 'Assets/UI/Menu/InputSystem/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9e5a90f9-92c2-4e6a-bee3-d788ae941b95"",
            ""actions"": [
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""53605593-b961-46ab-90b1-7c9c36b43d14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""d9f6a315-5fe0-46c8-8c1d-77f1f947feca"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""70883828-c678-4895-888b-bf4530834f47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""3eeaaf0e-b054-4e74-ac20-76ed4be55569"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""6daee4f1-f046-4a59-bdd5-68895bd2336e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseObject"",
                    ""type"": ""Button"",
                    ""id"": ""472c6375-56fa-4010-aad0-42ceeaf20a3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchAttackMode"",
                    ""type"": ""Button"",
                    ""id"": ""a9ed9a00-a03d-459d-ad15-4d21b5494e2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenCoffre"",
                    ""type"": ""Value"",
                    ""id"": ""32abab0f-79dd-4354-bff0-eea9459f1644"",
                    ""expectedControlType"": ""Digital"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenShop"",
                    ""type"": ""Value"",
                    ""id"": ""6673a60c-eae3-4dbb-9a2f-e4b3ce1325f6"",
                    ""expectedControlType"": ""Digital"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialShoot"",
                    ""type"": ""Button"",
                    ""id"": ""c57510d8-b6a4-4f26-b7bb-33e71d0706f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ff6a153-09b9-4bae-9a95-8654402b9b01"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2164f94-f370-419d-b00b-150b92377702"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""384c7824-4f5c-4ec7-9947-d340836ce395"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ff6e318e-9e45-4499-91de-edd630ea645a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""580dc430-16ab-4956-9bfd-c538d457c1f0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6a8fc72a-a4f8-40df-9ddc-ac38872afbb5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6fe7c5b8-5eb9-4efc-aae9-db349f49db46"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3b068532-4c4f-4b44-9e96-4ac7f59c474c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""29546bac-df0e-40f5-a37c-048195b5b506"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""UseObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81731a2f-20a5-46cd-b5f9-eb08069d022e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SwitchAttackMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e2294b5-66a4-49d6-9884-079c09673197"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""OpenCoffre"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c6e3939-53f6-4632-ace2-b07858e03e7d"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""OpenShop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e5065df-28de-4b25-8221-93fc05440aa4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SpecialShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""16f374cf-1a31-43a1-bbd5-2d33e37d6f40"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Horizontal = m_Player.FindAction("Horizontal", throwIfNotFound: true);
        m_Player_Vertical = m_Player.FindAction("Vertical", throwIfNotFound: true);
        m_Player_UseObject = m_Player.FindAction("UseObject", throwIfNotFound: true);
        m_Player_SwitchAttackMode = m_Player.FindAction("SwitchAttackMode", throwIfNotFound: true);
        m_Player_OpenCoffre = m_Player.FindAction("OpenCoffre", throwIfNotFound: true);
        m_Player_OpenShop = m_Player.FindAction("OpenShop", throwIfNotFound: true);
        m_Player_SpecialShoot = m_Player.FindAction("SpecialShoot", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Horizontal;
    private readonly InputAction m_Player_Vertical;
    private readonly InputAction m_Player_UseObject;
    private readonly InputAction m_Player_SwitchAttackMode;
    private readonly InputAction m_Player_OpenCoffre;
    private readonly InputAction m_Player_OpenShop;
    private readonly InputAction m_Player_SpecialShoot;
    public struct PlayerActions
    {
        private @PlayerControl m_Wrapper;
        public PlayerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Horizontal => m_Wrapper.m_Player_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_Player_Vertical;
        public InputAction @UseObject => m_Wrapper.m_Player_UseObject;
        public InputAction @SwitchAttackMode => m_Wrapper.m_Player_SwitchAttackMode;
        public InputAction @OpenCoffre => m_Wrapper.m_Player_OpenCoffre;
        public InputAction @OpenShop => m_Wrapper.m_Player_OpenShop;
        public InputAction @SpecialShoot => m_Wrapper.m_Player_SpecialShoot;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Horizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVertical;
                @UseObject.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseObject;
                @UseObject.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseObject;
                @UseObject.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseObject;
                @SwitchAttackMode.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchAttackMode;
                @SwitchAttackMode.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchAttackMode;
                @SwitchAttackMode.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchAttackMode;
                @OpenCoffre.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCoffre;
                @OpenCoffre.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCoffre;
                @OpenCoffre.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCoffre;
                @OpenShop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
                @OpenShop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
                @OpenShop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
                @SpecialShoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialShoot;
                @SpecialShoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialShoot;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @UseObject.started += instance.OnUseObject;
                @UseObject.performed += instance.OnUseObject;
                @UseObject.canceled += instance.OnUseObject;
                @SwitchAttackMode.started += instance.OnSwitchAttackMode;
                @SwitchAttackMode.performed += instance.OnSwitchAttackMode;
                @SwitchAttackMode.canceled += instance.OnSwitchAttackMode;
                @OpenCoffre.started += instance.OnOpenCoffre;
                @OpenCoffre.performed += instance.OnOpenCoffre;
                @OpenCoffre.canceled += instance.OnOpenCoffre;
                @OpenShop.started += instance.OnOpenShop;
                @OpenShop.performed += instance.OnOpenShop;
                @OpenShop.canceled += instance.OnOpenShop;
                @SpecialShoot.started += instance.OnSpecialShoot;
                @SpecialShoot.performed += instance.OnSpecialShoot;
                @SpecialShoot.canceled += instance.OnSpecialShoot;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    public struct UIActions
    {
        private @PlayerControl m_Wrapper;
        public UIActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnDash(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnUseObject(InputAction.CallbackContext context);
        void OnSwitchAttackMode(InputAction.CallbackContext context);
        void OnOpenCoffre(InputAction.CallbackContext context);
        void OnOpenShop(InputAction.CallbackContext context);
        void OnSpecialShoot(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
    }
}
