// GENERATED AUTOMATICALLY FROM 'Assets/Menu/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerMap"",
            ""id"": ""f68f7fea-9e4c-4e57-8ca2-84e185063c6c"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""31d6a01a-d369-4aa9-aa6c-62abf804c8b0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""51d0cd68-2649-482a-91a6-fe61ed4f6afc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""9fa4e1b1-19bc-4e9f-95f3-d16b07da42b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""22a7a27b-06da-4f15-8907-25f5ce78def1"",
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
                    ""id"": ""2d423380-6aa4-4407-a360-081a27db0d61"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f3b27fe8-5e88-4809-a9d5-c8d95ec5ad73"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f66ea4d0-dab9-433a-bcf4-b68a81ffcce7"",
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
                    ""id"": ""abafe720-89ef-44fd-b0d0-73216f552e33"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5f9931ca-2b19-400e-bdeb-e2a1e66e1b9c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b79ad7bc-142b-461a-980d-7ebb80457eee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse & KeyBoard"",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse & KeyBoard"",
            ""bindingGroup"": ""Mouse & KeyBoard"",
            ""devices"": []
        }
    ]
}");
        // PlayerMap
        m_PlayerMap = asset.FindActionMap("PlayerMap", throwIfNotFound: true);
        m_PlayerMap_Horizontal = m_PlayerMap.FindAction("Horizontal", throwIfNotFound: true);
        m_PlayerMap_Vertical = m_PlayerMap.FindAction("Vertical", throwIfNotFound: true);
        m_PlayerMap_Test = m_PlayerMap.FindAction("Test", throwIfNotFound: true);
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

    // PlayerMap
    private readonly InputActionMap m_PlayerMap;
    private IPlayerMapActions m_PlayerMapActionsCallbackInterface;
    private readonly InputAction m_PlayerMap_Horizontal;
    private readonly InputAction m_PlayerMap_Vertical;
    private readonly InputAction m_PlayerMap_Test;
    public struct PlayerMapActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerMapActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_PlayerMap_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_PlayerMap_Vertical;
        public InputAction @Test => m_Wrapper.m_PlayerMap_Test;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMapActions instance)
        {
            if (m_Wrapper.m_PlayerMapActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnVertical;
                @Test.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTest;
            }
            m_Wrapper.m_PlayerMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);
    private int m_MouseKeyBoardSchemeIndex = -1;
    public InputControlScheme MouseKeyBoardScheme
    {
        get
        {
            if (m_MouseKeyBoardSchemeIndex == -1) m_MouseKeyBoardSchemeIndex = asset.FindControlSchemeIndex("Mouse & KeyBoard");
            return asset.controlSchemes[m_MouseKeyBoardSchemeIndex];
        }
    }
    public interface IPlayerMapActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
    }
}
