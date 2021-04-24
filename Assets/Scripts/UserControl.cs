using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PersonScript))]
public class UserControl : MonoBehaviour
{
    private InputMaster input;

    [Range(0, 5)]
    public float sensitivityDefault = 1;
    [Range(0, 5)]
    public float sensitivityAim = 1;
    private float xSensDefault, ySensDefault;
    private float xSensAim, ySensAim;

    public bool aimLog = false;
    public float animLerpT;
    public float horizontal, vertical;
    public float defaultAimPointDistanceFromCamera;
    public float minAimPointDistanceFromHero;
    public float aimRayDistance;
    public LayerMask aimLayerMask;

    public Cinemachine.CinemachineFreeLook defaultCamera, aimCamera;

    private bool isAiming;

    private PersonScript personScript;
    private Transform cameraTransform;

    public GameObject aimPoint;
    public GameObject aimPointUI;
    public Transform aimPointFollow;

    public Color aimPointColor1, aimPointColor2;

    public Vector2[] recoilOffsets;
    private int index = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + transform.up + transform.forward * minAimPointDistanceFromHero, 0.1f);
    }

    private void Awake()
    {
        input = new InputMaster();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        personScript = GetComponent<PersonScript>();
        cameraTransform = GameObject.Find("Camera").transform;
        personScript.lookTarget = cameraTransform.Find("LookTarget");
        aimPointFollow.position = cameraTransform.TransformPoint(cameraTransform.forward * defaultAimPointDistanceFromCamera);
        aimPointFollow.rotation = aimPoint.transform.rotation;
        xSensDefault = defaultCamera.m_XAxis.m_MaxSpeed;
        ySensDefault = defaultCamera.m_YAxis.m_MaxSpeed;
        xSensAim = aimCamera.m_XAxis.m_MaxSpeed;
        ySensAim = aimCamera.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        SensitivitySet();
        AxisSet();
        Keyboard();
        FlagsCorrecting();
        CameraRotate();
        CameraSwitch();
        AimPointChangePos();
        Recoil();
    }

    void SensitivitySet()
    {
        defaultCamera.m_XAxis.m_MaxSpeed = xSensDefault * sensitivityDefault;
        defaultCamera.m_YAxis.m_MaxSpeed = ySensDefault * sensitivityDefault;
        aimCamera.m_XAxis.m_MaxSpeed = xSensAim * sensitivityAim;
        aimCamera.m_YAxis.m_MaxSpeed = ySensAim * sensitivityAim;
    }

    void Recoil()
    {
        if (!personScript.isShot)
        {
            return;
        }

        aimCamera.m_XAxis.Value += recoilOffsets[index].x;
        aimCamera.m_YAxis.Value -= recoilOffsets[index].y;

        index = (index + 1) % recoilOffsets.Length;
    }

    void AimPointChangePos()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, aimRayDistance, aimLayerMask))
        {
            if (personScript.weaponIndex == 0)
            {
                aimPointFollow.position = hit.point;
            }
            else if (transform.InverseTransformPoint(hit.point).z > minAimPointDistanceFromHero && hit.transform.gameObject != gameObject)
            {
                aimPointFollow.position = hit.point;

                if (!personScript.isPunching)
                {
                    personScript.aimObject = hit.collider.gameObject;
                }
            }
            else
            {
                aimPointFollow.localPosition = Vector3.forward * defaultAimPointDistanceFromCamera;

                if (!personScript.isPunching)
                {
                    personScript.aimObject = null;
                }
            }

            aimPointUI.GetComponent<Image>().color = aimPointColor1;
        }
        else
        {
            aimPointFollow.localPosition = Vector3.forward * defaultAimPointDistanceFromCamera;

            if (!personScript.isPunching)
            {
                personScript.aimObject = null;
            }

            aimPointUI.GetComponent<Image>().color = aimPointColor2;
        }

        aimPoint.transform.position = Vector3.Lerp(aimPoint.transform.position, aimPointFollow.position, animLerpT * Time.deltaTime);
    }

    void CameraRotate()
    {
        if (aimCamera.Priority > defaultCamera.Priority)
        {
            aimCamera.m_XAxis.m_InputAxisValue = input.CameraRotate.Rotate.ReadValue<Vector2>().x;
            aimCamera.m_YAxis.m_InputAxisValue = input.CameraRotate.Rotate.ReadValue<Vector2>().y;
        }
        else
        {
            defaultCamera.m_XAxis.m_InputAxisValue = input.CameraRotate.Rotate.ReadValue<Vector2>().x;
            defaultCamera.m_YAxis.m_InputAxisValue = input.CameraRotate.Rotate.ReadValue<Vector2>().y;
        }
    }

    void CameraSwitch()
    {
        if (isAiming)
        {
            aimCamera.Priority = defaultCamera.Priority + 1;
            defaultCamera.m_XAxis.Value = aimCamera.m_XAxis.Value;
            defaultCamera.m_YAxis.Value = aimCamera.m_YAxis.Value;
            aimPoint.SetActive(true);
            aimPointUI.SetActive(true);
        }
        else
        {
            aimCamera.Priority = defaultCamera.Priority - 1;
            aimCamera.m_XAxis.Value = defaultCamera.m_XAxis.Value;
            aimCamera.m_YAxis.Value = defaultCamera.m_YAxis.Value;
            aimPoint.SetActive(false);
            aimPointUI.SetActive(false);
        }
    }

    void FlagsCorrecting()
    {
        isAiming = personScript.isAiming;

        /*if (!personScript.isGrounded)
        {
            isAiming = false;
        }*/
    }

    void AxisSet()
    {
        /*horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");*/
        horizontal = input.Person.Movement.ReadValue<Vector2>().x;
        vertical = input.Person.Movement.ReadValue<Vector2>().y;

        personScript.rotateAimAngle = cameraTransform.rotation.eulerAngles.y;

        if (horizontal != 0 || vertical != 0)
        {
            personScript.rotateAngle = (90 - (cameraTransform.rotation.eulerAngles.y + 90 - Mathf.Atan2(vertical, horizontal) * 180 / Mathf.PI)
                % 360 + transform.rotation.eulerAngles.y);
        }
        else
        {
            personScript.rotateAngle = 361;
        }
    }

    void Keyboard()
    {
        personScript.isRunning = input.Person.Run.ReadValue<float>() > 0;
        personScript.isJumped = input.Person.Jump.triggered;
        personScript.isChangedGun = input.Person.ChangeGun.triggered;
        personScript.isShooting = input.Person.Shoot.ReadValue<float>() > 0;
        personScript.isAiming = aimLog ? true : input.Person.Aim.ReadValue<float>() > 0;

        if (input.Person.Crouch.triggered)
        {
            personScript.isCrouched = !personScript.isCrouched;
        }
    }
}
