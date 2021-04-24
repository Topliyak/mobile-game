using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]
public class PersonScript : MonoBehaviour
{
    public int maxLives = 100;
    public float lives;

    [Range(0, 6)]
    public int dangerDegree = 0;
    
    [Header("Profile for DangerDegree Change")]
    public bool dangerLineLog = false;
    [Range(0, 1)]
    public float dangerLine = 1; // danger line between 0 and 1. If 1 then dangerDegree increase
    public float dangerDecreaseLine = 0; // danger line between 0 and 1. If 0 then dangerDegree decrease
    public float dangerLineDecreaseCoefficient = 0.05f;
    public float dangerDecreaseLineDecreaseCoefficientWhenDangerDegree1or2 = 0.05f;
    public float dangerDecreaseLineDecreaseCoefficientWhenDangerDegree3 = 0.02f;
    public float dangerDecreaseLineDecreaseCoefficientWhenDangerDegree4 = 0.005f;

    public float priceForShootToPoliceOfficerWhenDangerDegree3 = 0.05f;
    public float priceForShootToPoliceOfficerWhenDangerDegree4 = 0.05f;
    public float priceForShootToPoliceOfficerWhenDangerDegree5 = 0.02f;
    public float priceForShootToCitizenWhenDangerDegree2 = 0.03f;
    public float priceForShootToCitizenWhenDangerDegree3 = 0.02f;
    public float priceForShootToGangsterWhenDangerDegree2 = 0.03f;
    public float priceForAttackPoliceOfficerByHandWhenDangerDegreeLessThan3;
    public float priceForAttackCitizenByHandWhenDangerDegreeLessThan3;

    [Header("Statuses")]
    public bool isAI = true;
    public bool isGrounded;
    public bool isRunning;
    public bool isJumped;
    public bool isCrouched;
    public bool isClimbing;
    public bool isAiming;
    public bool isShooting;
    public bool isPunching;
    public bool isChangedGun;
    public bool inWater;
    public bool endLessLive = false;

    [Header("Damage For Every Member Of Body In Per Cent")]
    public float damageFromPunch = 0.1f;
    public float headDamage = 100f;
    public float torsoDamage = 0.2f;
    public float handsDamage = 0.125f;
    public float legsDamage = 0.125f;

    private Animator animator;
    private CharacterController characterController;
    private Transform duplicateTransform;

    [Header("Stay CharacterController Profile")]
    public Vector3 stayCharacterControllerCenter = Vector3.up * 0.9f;
    public float stayCharacterControllerRadius = 0.25f;
    public float stayCharacterControllerHeight = 1.8f;

    [Header("Crouch CharacterController Profile")]
    public Vector3 crouchCharacterControllerCenter = new Vector3(0.1f, 0.5f, 0.14f);
    public float crouchCharacterControllerRadius = 0.5f;
    public float crouchCharacterControllerHeight = 1;

    [Header("Swim CharacterController Profile")]
    public Vector3 swimCharacterControllerCenter = Vector3.zero;
    public float swimCharacterControllerRadius = 0.5f;
    public float swimCharacterControllerHeight = 1;

    private float gravitySpeed;
    private const float gravityDefaultSpeed = -1;

    private int angleForClimb;
    private bool changePosForClimb;
    private bool climbFlag = false;
    private Vector3 positionForClimb;

    [Header("Climb Values")]
    public bool climbLog = false;
    public Vector2 climbPoint;
    public Vector3 duplicatePointDefaultPosition = new Vector3(0, 1.158f, 0.294f);
    public float minHelperLimitY = 1, maxHelperLimitY = 2.2f;
    public float minSizeForClimb = 1;
    public LayerMask climbObstaclesLayers;

    private BoxCollider InteractiveObjectsChecker;
    private Vector3 defaultBoxTriggerCenter;

    [Header("Axises")]
    public float forward = 0;
    public float horizontal = 0;
    public float vertical = 0;
    public float rotateAngle;
    public float rotateAimAngle;

    [Header("Gravity Values")]
    public bool isGravity = true;
    private const float gravityAcceleration = -9.8f;
    public float isGroundedRayDistance;
    public LayerMask isGroundedLayers;

    [Header("Turn Limits")]
    public float turnLeftMin;
    public float turnLeftMax;
    public float turnRightMin;
    public float turnRightMax;

    [Header("Lerp T")]
    public float animLerpT = 4;
    public float weightLerpT = 10;

    private bool isCrouchedPrevious = false;

    [Header("Swim Values")]
    public float waterHeightTrigger;
    public float waterSurfaceYPos;

    [HideInInspector]
    public int weaponIndex = 0;
    private WeaponScript weaponMethods;
    private Transform leftHandPosOnWeapon;

    [Header("Hands As Gun")]
    public float handsDelay;

    private bool pistol;
    [Header("Pistol")]
    public GameObject pistolObject;
    public int pistolBullets;

    private bool machineGun;
    [Header("Machine-Gun")]
    public GameObject machineGunObject;
    public int machineGunBullets;

    private float fistsWeight;
    private float topRigWeight;

    [Header("IK Values")]
    public float maxLookAtWeightIK;
    private float lookAtWeightIK;
    public float maxRightHandAimIK;
    private float rightHandAimIK;

    [HideInInspector]
    public Transform lookTarget = null;

    [HideInInspector]
    public GameObject aimObject;
    [HideInInspector]
    public GameObject punchObject;
    [HideInInspector]
    public bool isShot;

    private bool canChangePunch = true;

    //For set characterControllerProfile

    //public Vector3 chccenter;
    //public float height;
    //public float radius;

    [Header("Gizmos")]
    public bool drawWaterTriggerHigh = false;
    public bool drawDuplicatePointDefaultPosition = false;
    public bool drawIsGroundedRay = false;

    private void OnDrawGizmosSelected()
    {
        if (drawWaterTriggerHigh)
        {
            Gizmos.DrawCube(transform.position + Vector3.up * waterHeightTrigger, new Vector3(0.5f, 0, 0.5f));
        }

        if (drawDuplicatePointDefaultPosition)
        {
            Gizmos.DrawSphere(transform.position + transform.forward * duplicatePointDefaultPosition.z
                            + transform.up * duplicatePointDefaultPosition.y + transform.right * duplicatePointDefaultPosition.x, 0.05f);
        }

        if (drawIsGroundedRay)
        {
            Ray ray = new Ray(transform.position
                + transform.forward * characterController.center.z + transform.right * characterController.center.x
                + Vector3.up * characterController.height / 2, Vector3.down);

            Debug.DrawRay(transform.position
                + transform.forward * characterController.center.z + transform.right * characterController.center.x
                + Vector3.up * characterController.height / 2, Vector3.down * (characterController.height / 2 + isGroundedRayDistance), Color.red);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        duplicateTransform = transform.Find("DuplicateTransform");

        InteractiveObjectsChecker = GetComponent<BoxCollider>();
        defaultBoxTriggerCenter = InteractiveObjectsChecker.center;
    }

    void FixedUpdate()
    {
        CrouchStatusCheck();
        AimRotate();
        CharacterControllerSet();
        InteractiveCheckerSet();
        IsGroundedSet();
        WaterGravity();
        Gravity();
        CheckDangerLineAndChangeDangerDegree();
        DangerLineDecrease();
    }

    private void Update()
    {
        FlagsCorrecting();
        AnimationAxisSet();
        ResetRotation();
        CrouchStatusCheck();
        AnimationFlagSet();
        Swim();
        Climb();
        Aim();
        Shoot();
        AnimationWeightSet();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        FlagsCorrecting();

        if (isAiming)
        {
            lookAtWeightIK = Mathf.Lerp(lookAtWeightIK, maxLookAtWeightIK, weightLerpT * Time.deltaTime);
            rightHandAimIK = Mathf.Lerp(rightHandAimIK, maxRightHandAimIK, weightLerpT * Time.deltaTime);
        }
        else
        {
            lookAtWeightIK = Mathf.Lerp(lookAtWeightIK, 0, weightLerpT * Time.deltaTime);
            rightHandAimIK = Mathf.Lerp(rightHandAimIK, 0, weightLerpT * Time.deltaTime);
        }

        if (layerIndex == 0 && lookTarget != null)
        {
            animator.SetLookAtWeight(lookAtWeightIK, lookAtWeightIK, lookAtWeightIK, lookAtWeightIK);
            animator.SetLookAtPosition(lookTarget.position);
        }

        if (layerIndex == 1 && leftHandPosOnWeapon != null && (lives > 0 || endLessLive) && isAiming && lookTarget != null)
        {
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandAimIK);
            animator.SetIKRotation(AvatarIKGoal.RightHand, lookTarget.rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPosOnWeapon.position);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPosOnWeapon.rotation);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == gameObject)
        {
            return;
        }

        if (other.gameObject.layer == 8 && weaponIndex == 0)
        {
            if (transform.InverseTransformPoint(other.transform.position).z > 0)
            {
                punchObject = other.gameObject;
            }
            else if (other.gameObject == punchObject)
            {
                punchObject = null;
            }
        }

        Ray ray = new Ray(new Vector3(transform.position.x, other.transform.position.y, transform.position.z), 
                          other.transform.position - new Vector3(transform.position.x, other.transform.position.y, transform.position.z));
        RaycastHit hit;

        if (!isClimbing && !isGrounded && other.gameObject.GetComponent<HelperData>() != null
            && (!Physics.Raycast(ray, out hit, Vector3.Distance(other.transform.position, transform.position), climbObstaclesLayers)
                || hit.transform == transform || hit.transform == other.transform)
            && other.bounds.max.y - transform.position.y < maxHelperLimitY
            && other.bounds.max.y - transform.position.y > minHelperLimitY)
        {
            HelperData helperData = other.gameObject.GetComponent<HelperData>();

            StartClimb1(other.transform);

            //--------------------------------------------------------------------------------------------------------------------

            angleForClimb = 361;

            if (Mathf.Abs(duplicateTransform.localPosition.x) < Mathf.Abs(duplicateTransform.localPosition.z))
            {
                if (duplicateTransform.localPosition.z > 0 && helperData.IsFree(180))
                {
                    angleForClimb = 180;
                    positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, 0.5f);
                }
                else if (duplicateTransform.localPosition.z < 0 && helperData.IsFree(0))
                {
                    angleForClimb = 0;
                    positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, -0.5f);
                }
            }
            else
            {
                if (duplicateTransform.localPosition.x > 0 && helperData.IsFree(-90))
                {
                    angleForClimb = -90;
                    positionForClimb = new Vector3(0.5f, 0.5f, Mathf.Clamp(duplicateTransform.localPosition.z, -0.5f, 0.5f));
                }
                else if (duplicateTransform.localPosition.x < 0 && helperData.IsFree(90))
                {
                    angleForClimb = 90;
                    positionForClimb = new Vector3(-0.5f, 0.5f, Mathf.Clamp(duplicateTransform.localPosition.z, -0.5f, 0.5f));
                }
            }

            if (angleForClimb == 361)
            {
                StopClimb1();
                StopClimb2();
            }

            //--------------------------------------------------------------------------------------------------------------------

            /*if (other.transform.localScale.z >= minSizeForClimb
            && other.transform.localScale.x >= minSizeForClimb)
            {
                //climb on

                angleForClimb = 0;
                positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, -0.5f);

                if (Mathf.Abs(duplicateTransform.localPosition.x) < Mathf.Abs(duplicateTransform.localPosition.z))
                {
                    if (duplicateTransform.localPosition.z > 0)
                    {
                        angleForClimb = 180;
                        positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, 0.5f);
                    }
                }
                else
                {
                    if (duplicateTransform.localPosition.x > 0)
                    {
                        angleForClimb = -90;
                        positionForClimb = new Vector3(0.5f, 0.5f, Mathf.Clamp(duplicateTransform.localPosition.z, -0.5f, 0.5f));
                    }
                    else
                    {
                        angleForClimb = 90;
                        positionForClimb = new Vector3(-0.5f, 0.5f, Mathf.Clamp(duplicateTransform.localPosition.z, -0.5f, 0.5f));
                    }
                }
            }
            else if (Mathf.Abs(duplicateTransform.localPosition.x) < Mathf.Abs(duplicateTransform.localPosition.z))
            {
                //climb over

                StopClimb1();
                StopClimb2();

                *//*angleForClimb = 0;
                positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, -0.5f);

                if (duplicateTransform.localPosition.z > 0)
                {
                    angleForClimb = 180;
                    positionForClimb = new Vector3(Mathf.Clamp(duplicateTransform.localPosition.x, -0.5f, 0.5f), 0.5f, 0.5f);
                }*//*
            }
            else
            {
                //stop climb

                StopClimb1();
                StopClimb2();
            }*/
        }

        if (other.tag == "Water" 
            && other.bounds.max.y >= transform.position.y + waterHeightTrigger)
        {
            inWater = true;
            waterSurfaceYPos = other.bounds.max.y;
        }

        if (isPunching)
        {
            aimObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (punchObject == other.gameObject)
        {
            punchObject = null;
        }
    }

    private void ResetRotation()
    {
        if (isGrounded)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    void ChangeDangerDegreeBecauseOfShootToPoliceOfficer()
    {
        if (dangerDegree <= 2)
        {
            dangerDegree = 3;
        }
        else if (dangerDegree == 3)
        {
            dangerLine += priceForShootToPoliceOfficerWhenDangerDegree3;
        }
        else if (dangerDegree == 4)
        {
            dangerLine += priceForShootToPoliceOfficerWhenDangerDegree4;
        }
        else if (dangerDegree == 5)
        {
            dangerLine += priceForShootToPoliceOfficerWhenDangerDegree5;
        }
    }

    void ChangeDangerDegreeBecauseOfShootToCitizen()
    {
        if (dangerDegree < 2)
        {
            dangerDegree = 2;
        }
        else if (dangerDegree == 2)
        {
            dangerLine += priceForShootToCitizenWhenDangerDegree2;
        }
        else if (dangerDegree == 3)
        {
            dangerLine += priceForShootToCitizenWhenDangerDegree3;
        }
    }

    void ChangeDangerDegreeBecauseOfShootToGangster()
    {
        if (dangerDegree < 2)
        {
            dangerDegree = 2;
        }
        else if (dangerDegree == 2)
        {
            dangerLine += priceForShootToGangsterWhenDangerDegree2;
        }
    }

    void ChangeDangerDegreeBecauseOfShootToPeople(string tagOfPeopleWhoIsShot)
    {
        if (tagOfPeopleWhoIsShot == "PoliceOfficer")
        {
            ChangeDangerDegreeBecauseOfShootToPoliceOfficer();
        }
        else if (tagOfPeopleWhoIsShot == "Citizen")
        {
            ChangeDangerDegreeBecauseOfShootToCitizen();
        }
        else if (tagOfPeopleWhoIsShot == "Gangster")
        {
            ChangeDangerDegreeBecauseOfShootToGangster();
        }

        dangerDecreaseLine = 1;
    }

    void ChangeDangerDegreeBecauseOfAttackPoliceOfficerByHand()
    {
        if (dangerDegree == 0)
        {
            dangerDegree = 1;
        }
        else if (dangerDegree <= 2)
        {
            dangerLine += priceForAttackPoliceOfficerByHandWhenDangerDegreeLessThan3;
        }
    }

    void ChangeDangerDegreeBecauseOfAttackCitizenByHand()
    {
        if (dangerDegree == 0)
        {
            dangerDegree = 1;
        }
        else if (dangerDegree <= 2)
        {
            dangerLine += priceForAttackCitizenByHandWhenDangerDegreeLessThan3;
        }
    }

    void ChangeDangerDegreeBecauseOfAttackGangsterByHand()
    {
        if (dangerDegree == 0)
        {
            dangerDegree = 1;
        }
    }

    void ChangeDangerDegreeBecauseOfAttackPeopleByHand(string tagOfPeopleWhoIsAttacked)
    {
        if (tagOfPeopleWhoIsAttacked == "PoliceOfficer")
        {
            ChangeDangerDegreeBecauseOfAttackPoliceOfficerByHand();
        }
        else if (tagOfPeopleWhoIsAttacked == "Citizen")
        {
            ChangeDangerDegreeBecauseOfAttackCitizenByHand();
        }
        else if (tagOfPeopleWhoIsAttacked == "Gangster")
        {
            ChangeDangerDegreeBecauseOfAttackGangsterByHand();
        }

        dangerDecreaseLine = 1;
    }

    void Shoot()
    {
        if (!isShooting || !isAiming)
        {
            isShot = false;
            return;
        }

        bool isHit = false;

        if (weaponMethods != null)
        {
            isHit = weaponMethods.Shoot();
        }
        else if (canChangePunch)
        {
            StartPunch();
        }

        isShot = isHit;

        if (!isHit || aimObject == null)
        {
            return;
        }

        if (aimObject.layer == 10 && aimObject.GetComponent<BodyScript>() != null) // if layer == "body"
        {
            aimObject.GetComponent<BodyScript>().SayToOwnerAboutHit(1);

            ChangeDangerDegreeBecauseOfShootToPeople(aimObject.GetComponent<BodyScript>().owner.tag);

            if (aimObject.GetComponent<BodyScript>().ownerPersonScript.isAI)
            {
                aimObject.GetComponent<BodyScript>().owner.GetComponent<AIControl>().attacker = gameObject;
            }

        }
    }

    void CheckDangerLineAndChangeDangerDegree()
    {
        if (1 - dangerLine <= 0.0001)
        {
            dangerDegree += 1;
        }

        if (dangerDecreaseLine <= 0.0001)
        {
            dangerDegree -= 1;
        }

        dangerDegree = Mathf.Clamp(dangerDegree, 0, 6);
    }

    void DangerLineDecrease()
    {
        dangerLine -= dangerLineDecreaseCoefficient * Time.fixedDeltaTime;
        dangerLine = Mathf.Clamp(dangerLine, 0, 1);


        if (dangerDegree <= 2)
        {
            dangerDecreaseLine -= dangerDecreaseLineDecreaseCoefficientWhenDangerDegree1or2 * Time.fixedDeltaTime;
        }
        else if (dangerDegree == 3)
        {
            dangerDecreaseLine -= dangerDecreaseLineDecreaseCoefficientWhenDangerDegree3 * Time.fixedDeltaTime;
        }
        else if (dangerDegree == 4)
        {
            dangerDecreaseLine -= dangerDecreaseLineDecreaseCoefficientWhenDangerDegree4 * Time.fixedDeltaTime;
        }

        dangerDecreaseLine = Mathf.Clamp(dangerDecreaseLine, 0f, 1f);


        if (dangerLineLog)
        {
            GameObject.Find("LogCanvas").transform.Find("Slider").GetComponent<Slider>().value = dangerLine;
            GameObject.Find("LogCanvas").transform.Find("Text").GetComponent<Text>().text = dangerDegree.ToString();
            GameObject.Find("LogCanvas").transform.Find("Slider2").GetComponent<Slider>().value = lives;
            GameObject.Find("LogCanvas").transform.Find("Slider3").GetComponent<Slider>().value = dangerDecreaseLine;
        }
    }

    void AimRotate()
    {
        FlagsCorrecting();
        if (isAiming)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotateAimAngle, 0), animLerpT * Time.fixedDeltaTime);
        }
    }

    void FlagsCorrecting()
    {
        if (!isGrounded || isClimbing)
        {
            isAiming = false;
        }
    }

    void CrouchStatusCheck()
    {
        Ray ray = new Ray(transform.position
            + transform.forward * characterController.center.z + transform.right * characterController.center.x
            + Vector3.up * characterController.height / 2, Vector3.up);

        if ((isCrouched == false || isJumped || isAiming || isRunning) && isCrouchedPrevious == true 
            && Physics.SphereCast(ray, characterController.radius, 
            characterController.height / 2 + stayCharacterControllerHeight - crouchCharacterControllerHeight, isGroundedLayers))
        {
            isCrouched = true;
            isRunning = false;
            isAiming = false;
            isJumped = false;
        }

        isCrouchedPrevious = isCrouched;
    }

    private void WaterGravity()
    {
        if (!inWater)
        {
            return;
        }

        if (transform.position.y + waterHeightTrigger < waterSurfaceYPos)
        {
            characterController.Move(Vector3.up * Time.fixedDeltaTime);
        }
        else if (transform.position.y + waterHeightTrigger > waterSurfaceYPos + 0.3f)
        {
            characterController.Move(Vector3.down * Time.fixedDeltaTime);
        }
    }

    private void Swim()
    {
        if (isGrounded && (waterSurfaceYPos + 0.3f < transform.position.y + waterHeightTrigger))
        {
            inWater = false;
        }

        if (!inWater)
        {
            return;
        }

        StopJump();

        if (rotateAngle != 361)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90 - rotateAngle, 0),
                                                  animLerpT * Time.deltaTime);
        }
    }

    private void Aim()
    {
        if (isPunching)
        {
            topRigWeight = fistsWeight = 0;
        }
        else if (isAiming)
        {
            topRigWeight = 1;
            fistsWeight = 0;
        }
        else
        {
            topRigWeight = 0;
            fistsWeight = 1;
        }

        if (isChangedGun && !isPunching)
        {
            weaponIndex = (weaponIndex + 1) % 3;
        }

        if (weaponIndex == 0)
        {
            //hands
            pistol = false;
            machineGun = false;
            leftHandPosOnWeapon = null;
            pistolObject.SetActive(false);
            machineGunObject.SetActive(false);
            weaponMethods = null;
        }
        else if (weaponIndex == 1)
        {
            //pistol
            pistol = true;
            machineGun = false;
            leftHandPosOnWeapon = pistolObject.transform.Find("LeftHandPos");
            machineGunObject.SetActive(false);
            pistolObject.SetActive(true);
            weaponMethods = pistolObject.GetComponent<WeaponScript>();
        }
        else if (weaponIndex == 2)
        {
            //machine-gun
            machineGun = true;
            pistol = false;
            leftHandPosOnWeapon = machineGunObject.transform.Find("LeftHandPos");
            pistolObject.SetActive(false);
            machineGunObject.SetActive(true);
            weaponMethods = machineGunObject.GetComponent<WeaponScript>();
        }
    }

    private void Climb()
    {
        if (!changePosForClimb)
        {
            return;
        }

        duplicateTransform.localRotation = Quaternion.Slerp(duplicateTransform.localRotation, 
                                                            Quaternion.Euler(Vector3.up * angleForClimb), animLerpT * 5 * Time.deltaTime);
        duplicateTransform.localPosition = Vector3.Slerp(duplicateTransform.localPosition, positionForClimb, animLerpT * 5 * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, duplicateTransform.rotation, 1);
        transform.position = Vector3.Slerp(transform.position, 
            duplicateTransform.position + duplicateTransform.forward * climbPoint.x + duplicateTransform.up * climbPoint.y, 1);

        if (!climbLog && (Mathf.Abs(duplicateTransform.localRotation.eulerAngles.y % 360 - 360 - angleForClimb) % 360 <= 1f
          || Mathf.Abs(duplicateTransform.localRotation.eulerAngles.y % 360 + 360 - angleForClimb) % 360 <= 1f)
          && Mathf.Abs(duplicateTransform.localPosition.x - positionForClimb.x) <= 0.1f
          && Mathf.Abs(duplicateTransform.localPosition.y - positionForClimb.y) <= 0.1f
          && Mathf.Abs(duplicateTransform.localPosition.z - positionForClimb.z) <= 0.1f)
        {
            animator.SetTrigger("climbOn");
        }
    }

    void IsGroundedSet()
    {
        Ray ray = new Ray(transform.position
            + transform.forward * characterController.center.z + transform.right * characterController.center.x
            + Vector3.up * characterController.height / 2, Vector3.down);

        if (characterController.isGrounded)
        {
            isGrounded = true;
        }
        else if (Physics.SphereCast(ray, characterController.radius, characterController.height / 2 + isGroundedRayDistance, isGroundedLayers))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Gravity()
    {
        gravitySpeed += gravityAcceleration * Time.fixedDeltaTime;

        if (characterController.isGrounded)
        {
            gravitySpeed = gravityDefaultSpeed;
        }

        if (isGravity && !inWater && characterController.enabled)
        {
            characterController.Move(Vector3.up * gravitySpeed * Time.fixedDeltaTime);
        }
    }

    void AnimationAxisSet()
    {
        int n = 1;

        if (isRunning)
            n = 2;

        if (rotateAngle != 361)
        {
            forward = 1;
            horizontal = Mathf.Lerp(horizontal, Mathf.Cos(rotateAngle * Mathf.PI / 180) * n, animLerpT * Time.deltaTime);
            vertical = Mathf.Lerp(vertical, Mathf.Sin(rotateAngle * Mathf.PI / 180) * n, animLerpT * Time.deltaTime);
        }
        else
        {
            forward = 0;
            horizontal = Mathf.Lerp(horizontal, 0, animLerpT * Time.deltaTime);
            vertical = Mathf.Lerp(vertical, 0, animLerpT * Time.deltaTime);
        }

        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("forward", forward);
    }

    void AnimationFlagSet()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isCrouched", isCrouched);
        animator.SetBool("inWater", inWater);
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("pistol", pistol);
        animator.SetBool("machineGun", machineGun);
        animator.SetBool("isDead", endLessLive ? false : lives <= 0);

        animator.SetBool("turn", ((rotateAngle + 360) % 360 > turnLeftMax) && ((rotateAngle + 360) % 360 < turnRightMin));
        animator.SetBool("turnLeft", ((rotateAngle + 360) % 360 >= turnLeftMin) && ((rotateAngle + 360) % 360 <= turnLeftMax));
        animator.SetBool("turnRight", rotateAngle != 361 && (((rotateAngle + 360) % 360 >= turnRightMin) || ((rotateAngle + 360) % 360 <= turnRightMax)));

        if (isJumped && rotateAngle != 361)
        {
            animator.SetTrigger("jumpForward");
        }

        if (isJumped && rotateAngle == 361)
        {
            animator.SetTrigger("jumpStand");
        }
    }

    void AnimationWeightSet()
    {
        if (!endLessLive && lives <= 0)
        {
            topRigWeight = 0;
            fistsWeight = 0;
        }

        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), topRigWeight, weightLerpT * Time.deltaTime));
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), fistsWeight, weightLerpT * Time.deltaTime));
    }

    private void CharacterControllerSet()
    {
        //For comfortable set CharacterControllerProfile
        //characterController.center = chcCenter;
        //characterController.height = height;
        //characterController.radius = radius;

        Vector3 newCharacterControllerCenter = stayCharacterControllerCenter;
        float newCharacterControllerRadius = stayCharacterControllerRadius;
        float newCharacterControllerHeight = stayCharacterControllerHeight;

        if (inWater)
        {
            //Swim
            newCharacterControllerCenter = swimCharacterControllerCenter;
            newCharacterControllerRadius = swimCharacterControllerRadius;
            newCharacterControllerHeight = swimCharacterControllerHeight;
        }
        else if (isCrouched && isGrounded && !isAiming && !isRunning)
        {
            //Crouch
            newCharacterControllerCenter = crouchCharacterControllerCenter;
            newCharacterControllerRadius = crouchCharacterControllerRadius;
            newCharacterControllerHeight = crouchCharacterControllerHeight;
        }
        else if (!isGrounded)
        {
            //Air
            newCharacterControllerCenter = stayCharacterControllerCenter;
            newCharacterControllerRadius = stayCharacterControllerRadius;
            newCharacterControllerHeight = stayCharacterControllerHeight;
        }
        else if (isGrounded && !isCrouched && !inWater)
        {
            //Walk
            newCharacterControllerCenter = stayCharacterControllerCenter;
            newCharacterControllerRadius = stayCharacterControllerRadius;
            newCharacterControllerHeight = stayCharacterControllerHeight;
        }
        else
        {
            //print("else");
        }

        characterController.center = Vector3.Lerp(characterController.center, newCharacterControllerCenter, animLerpT * Time.fixedDeltaTime);
        characterController.radius = Mathf.Lerp(characterController.radius, newCharacterControllerRadius, animLerpT * Time.fixedDeltaTime);
        characterController.height = Mathf.Lerp(characterController.height, newCharacterControllerHeight, animLerpT * Time.fixedDeltaTime);
    }

    private void InteractiveCheckerSet()
    {
        InteractiveObjectsChecker.center = Vector3.Lerp(InteractiveObjectsChecker.center, 
                                                        defaultBoxTriggerCenter +
                                                        new Vector3(characterController.center.x - stayCharacterControllerCenter.x, 0, 
                                                                    characterController.center.z - stayCharacterControllerCenter.z 
                                                                    + characterController.radius - stayCharacterControllerRadius), 
                                                        animLerpT * Time.fixedDeltaTime);
    }

    public void GetDamage(int typeOfBody, float damage)
    {
        if (typeOfBody == 0)
        {
            //damage from punch
            lives -= maxLives * damageFromPunch * damage;
        }
        if (typeOfBody == 1)
        {
            //legs
            lives -= maxLives * legsDamage * damage;
        }
        else if (typeOfBody == 2)
        {
            //hands
            lives -= maxLives * handsDamage * damage;
        }
        else if (typeOfBody == 3)
        {
            //torso
            lives -= maxLives * torsoDamage * damage;
        }
        else if (typeOfBody == 4)
        {
            //head
            lives -= maxLives * headDamage * damage;
        }
    }

    public void ProcessPunch()
    {
        if (punchObject == null)
        {
            return;
        }

        PersonScript punchObjPS = punchObject.GetComponent<PersonScript>();
        punchObjPS.GetDamage(0, 1);

        AIControl punchObjAIControl = punchObject.GetComponent<AIControl>();

        if (punchObjAIControl != null)
        {
            punchObjAIControl.attacker = gameObject;
        }

        ChangeDangerDegreeBecauseOfAttackPeopleByHand(punchObject.tag);
    }

    public void StartJump()
    {
        isGravity = false;
    }

    public void StopJump()
    {
        isGravity = true;
        animator.ResetTrigger("jumpStand");
        animator.ResetTrigger("jumpForward");
    }

    private void StartPunch()
    {
        isPunching = true;
        topRigWeight = 0;
        fistsWeight = 0;
        canChangePunch = false;
        animator.SetBool("punch", true);
    }

    public void StopPunch1()
    {
        canChangePunch = true;
        animator.SetBool("punch", false);
    }

    public void StopPunch2()
    {
        if (animator.GetBool("punch"))
        {
            return;
        }

        isPunching = false;
        topRigWeight = 1;
        fistsWeight = 0;
    }

    public void StartClimb1(Transform other)
    {
        isClimbing = true;
        characterController.enabled = false;
        inWater = false;
        duplicateTransform.SetParent(other);
        animator.SetTrigger("climbPose");
    }

    public void StartClimb2()
    {
        if (climbFlag)
        {
            return;
        }

        climbFlag = true;
        changePosForClimb = true;
        inWater = false;
    }

    public void StopClimb1()
    {
        changePosForClimb = false;
        duplicateTransform.SetParent(transform);
        duplicateTransform.transform.localPosition = duplicatePointDefaultPosition;
        duplicateTransform.transform.localRotation = Quaternion.Euler(Vector3.zero);
        duplicateTransform.localScale = Vector3.one;
    }

    public void StopClimb2()
    {
        climbFlag = false;
        isClimbing = false;
        characterController.enabled = true;
        StopJump();
        animator.ResetTrigger("climbPose");
        animator.ResetTrigger("climbOn");
    }

    public void OnDead()
    {
        if (isAI)
        {
            GetComponent<AIControl>().Dead();
        }

        InteractiveObjectsChecker.enabled = false;
        characterController.enabled = false;
        animator.SetBool("isDead", false);
        this.enabled = false;
    }
}
