// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Person"",
            ""id"": ""2561e4bc-4182-4fb4-9fca-8b652b5e8d90"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""7d05aaf8-1734-4e90-a29c-3b79856a1149"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""e26164eb-bba6-4f2e-8681-7ee09db170f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""16182d77-d870-4798-864f-427da0a28879"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeGun"",
                    ""type"": ""Button"",
                    ""id"": ""21329742-8c6c-43c0-8636-f70b8eccb377"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8897d3f3-41b4-4109-897e-6d87f92ae788"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""9f9b1ecf-3ad5-4d30-91b6-60d2757da662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""89617aca-f4f9-4d43-923c-d35ac6e5badc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d7748d74-e70c-472c-af80-d4155bc397a3"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b498f165-ed02-48f8-8771-dbff0b01d262"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""647762f2-b6fb-44d7-b177-854d74d438b2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2920767e-26ae-47c4-b3e8-415d31e348d6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f98a3da9-3e4d-4123-9b1e-4ce9fff1f06c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""922fee57-d08c-4e5d-9f9f-e2f978aa94c0"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f6945f07-ed6c-471f-8986-8f42c8a3bddb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""23b9826f-deed-47da-a96d-e4fd843983f3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""106215a9-13b7-4c6b-9efe-6474989aec77"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b70cf3b4-f1b3-4d50-a799-7008d21cb2c4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a1db9f74-254c-449e-be93-5862b3630ee0"",
                    ""path"": ""1DAxis(minValue=0)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4545446b-180c-4d24-83e2-0b94a952f95d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a4fe61b7-7401-4648-a501-d4c359544698"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b61a73d9-8320-4450-86bd-7ed180cbdff1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ChangeGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9903e9f9-20aa-40d7-aa70-03cac1aa9a21"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e2519432-1e7e-4a29-978c-77d737dba7f5"",
                    ""path"": ""1DAxis(minValue=0)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bdf7c264-4d4e-4d93-a753-e293fc3008dd"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""90e324d2-35b9-4a96-b408-f257db04c8c8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eb0b6237-98a1-42e9-9f28-3535dcedda65"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dbf3b27-e139-4ea8-b63e-cd780deca368"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e560ee81-6851-4306-a60b-99730613f097"",
                    ""path"": ""1DAxis(minValue=0)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""432a8a27-f533-4757-b1f2-4a7429d254a4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3b0dc87d-63bb-4636-b4a8-fec938c6f50d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""12a0ad82-ee92-4196-92be-dab39678c8fe"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""50d5687d-4d4e-4335-861e-6c586412ff21"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""57ce4526-59bc-406f-b8a1-1f3e30c65bd9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""CameraRotate"",
            ""id"": ""46549a5d-5278-40c2-9b8a-eb37015ef111"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""22bf9a79-5628-43a6-8765-0c73119698ac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f9df8bb-1374-4c9b-a0aa-9de22d87337f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d6a8b91-d3b0-477b-8417-2d4282350a81"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Person
        m_Person = asset.FindActionMap("Person", throwIfNotFound: true);
        m_Person_Movement = m_Person.FindAction("Movement", throwIfNotFound: true);
        m_Person_Aim = m_Person.FindAction("Aim", throwIfNotFound: true);
        m_Person_Shoot = m_Person.FindAction("Shoot", throwIfNotFound: true);
        m_Person_ChangeGun = m_Person.FindAction("ChangeGun", throwIfNotFound: true);
        m_Person_Jump = m_Person.FindAction("Jump", throwIfNotFound: true);
        m_Person_Crouch = m_Person.FindAction("Crouch", throwIfNotFound: true);
        m_Person_Run = m_Person.FindAction("Run", throwIfNotFound: true);
        // CameraRotate
        m_CameraRotate = asset.FindActionMap("CameraRotate", throwIfNotFound: true);
        m_CameraRotate_Rotate = m_CameraRotate.FindAction("Rotate", throwIfNotFound: true);
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

    // Person
    private readonly InputActionMap m_Person;
    private IPersonActions m_PersonActionsCallbackInterface;
    private readonly InputAction m_Person_Movement;
    private readonly InputAction m_Person_Aim;
    private readonly InputAction m_Person_Shoot;
    private readonly InputAction m_Person_ChangeGun;
    private readonly InputAction m_Person_Jump;
    private readonly InputAction m_Person_Crouch;
    private readonly InputAction m_Person_Run;
    public struct PersonActions
    {
        private @InputMaster m_Wrapper;
        public PersonActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Person_Movement;
        public InputAction @Aim => m_Wrapper.m_Person_Aim;
        public InputAction @Shoot => m_Wrapper.m_Person_Shoot;
        public InputAction @ChangeGun => m_Wrapper.m_Person_ChangeGun;
        public InputAction @Jump => m_Wrapper.m_Person_Jump;
        public InputAction @Crouch => m_Wrapper.m_Person_Crouch;
        public InputAction @Run => m_Wrapper.m_Person_Run;
        public InputActionMap Get() { return m_Wrapper.m_Person; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PersonActions set) { return set.Get(); }
        public void SetCallbacks(IPersonActions instance)
        {
            if (m_Wrapper.m_PersonActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnMovement;
                @Aim.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnAim;
                @Shoot.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnShoot;
                @ChangeGun.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnChangeGun;
                @ChangeGun.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnChangeGun;
                @ChangeGun.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnChangeGun;
                @Jump.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnCrouch;
                @Run.started -= m_Wrapper.m_PersonActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PersonActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PersonActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_PersonActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ChangeGun.started += instance.OnChangeGun;
                @ChangeGun.performed += instance.OnChangeGun;
                @ChangeGun.canceled += instance.OnChangeGun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public PersonActions @Person => new PersonActions(this);

    // CameraRotate
    private readonly InputActionMap m_CameraRotate;
    private ICameraRotateActions m_CameraRotateActionsCallbackInterface;
    private readonly InputAction m_CameraRotate_Rotate;
    public struct CameraRotateActions
    {
        private @InputMaster m_Wrapper;
        public CameraRotateActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_CameraRotate_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_CameraRotate; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraRotateActions set) { return set.Get(); }
        public void SetCallbacks(ICameraRotateActions instance)
        {
            if (m_Wrapper.m_CameraRotateActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_CameraRotateActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_CameraRotateActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_CameraRotateActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_CameraRotateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public CameraRotateActions @CameraRotate => new CameraRotateActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPersonActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeGun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
    }
    public interface ICameraRotateActions
    {
        void OnRotate(InputAction.CallbackContext context);
    }
}
