using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PersonScript))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIControl : MonoBehaviour
{
    private PersonScript personScript;
    public NavMeshAgent navMeshAgent;
    private CharacterController characterController;

    [Range(0, 4)]
    [Tooltip("1 - Idle\n2 - Panic\n3 - Attack (Hands)\n4 - Attack (Weapon)")]
    public int status = 0;

    public float animLerpT = 4;

    public int maxWeaponIndex;

    public float maxDistanceToAgent = 1;
    public float stoppingDistanceToAgent = 0.2f;
    public float stoppingDistanceForIdle;
    public float attackDistance;
    private bool needMoveToOpponent = false;

    public GameObject attacker = null;

    [HideInInspector]
    public Transform[] wayPoints;
    public bool[] dangerousStatusOfPoints;
    public Transform target = null;
    public Transform targetForShoot = null;
    private Transform previousTarget = null;
    public int targetIndex;

    public int luckyCoefficientHitToHead = 50;
    public int luckyCoefficientHitToTorso = 250;
    public int luckyCoefficientHitToHand = 350;
    public int luckyCoefficientHitToLeg = 350;
    public int unluckyCoefficient = 2000;

    private Transform lookTarget;

    public LayerMask aimObstacleLayerMask;

    public bool log = false;

    private void Awake()
    {
        if (gameObject.tag == "PoliceOfficer")
        {
            gameObject.AddComponent<PoliceOfficerAsist>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        personScript = GetComponent<PersonScript>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        lookTarget = transform.Find("LookTarget");
    }

    void FixedUpdate()
    {
        MoveToOpponent();
    }

    // Update is called once per frame
    void Update()
    {
        Log();
        Idle();
        Panic();
        AttackHands();
        AttackWeapon();
    }

    void MoveToOpponent()
    {
        if (needMoveToOpponent && target != null)
        {
            Vector3 move = (target.position - transform.position).normalized;

            characterController.Move(move * animLerpT * Time.fixedDeltaTime);
        }
        else if (target == null)
        {
            needMoveToOpponent = false;
        }
    }

    void TurnRun(bool isRunning)
    {
        if (isRunning)
        {
            maxDistanceToAgent = 3;
            personScript.isRunning = true;
        }
        else
        {
            maxDistanceToAgent = 1;
            personScript.isRunning = false;
        }
    }

    void Move(Vector3 targetPosition, bool needMove = true)
    {
        navMeshAgent.SetDestination(targetPosition);

        float distanceToAgent = Vector3.Distance(transform.position, navMeshAgent.nextPosition);

        if (distanceToAgent > maxDistanceToAgent)
        {
            navMeshAgent.velocity = Vector3.zero;
        }

        if (!needMove || distanceToAgent <= stoppingDistanceToAgent)
        {
            personScript.rotateAngle = 361;
            return;
        }

        Vector3 move = transform.InverseTransformPoint(navMeshAgent.nextPosition);

        personScript.rotateAngle = Mathf.Atan2(move.z, move.x) * 180 / Mathf.PI;
    }

    void Log()
    {
        if (status != 0)
        {
            return;
        }

        Move(target.position);
    }

    void Idle()
    {
        if (status != 1 || wayPoints.Length == 0)
        {
            return;
        }

        TurnRun(false);

        if (target == null)
        {
            targetIndex = -1;
            float minDistance = 0;

            for (int i = 0; i < wayPoints.Length; i++)
            {
                if (!dangerousStatusOfPoints[i] 
                    && (targetIndex == -1 || Vector3.Distance(transform.position, wayPoints[i].position) <= minDistance))
                {
                    targetIndex = i;
                    minDistance = Vector3.Distance(transform.position, wayPoints[i].position);
                }
            }

            if (targetIndex != -1)
            {
                target = wayPoints[targetIndex];
            }
            else
            {
                target = null;
            }
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            List<Transform> nextPointVariants = new List<Transform>();
            List<int> nextPointVariantsIndexes = new List<int>();
            NodeData node = target.GetComponent<NodeData>();
            
            for (int i = 0; i < node.nodePoints.Length; i++)
            {
                if (node.nodePoints[i] != previousTarget)
                {
                    nextPointVariants.Add(node.nodePoints[i]);
                    nextPointVariantsIndexes.Add(node.nodePointsIndexes[i]);
                }
            }

            previousTarget = target;
            targetIndex = Random.Range(0, nextPointVariants.Count);
            target = nextPointVariants[targetIndex];
            targetIndex = nextPointVariantsIndexes[targetIndex];
        }

        if (target != null)
        {
            Move(target.position);
        }
        else
        {
            Move(Vector3.zero, false);
        }
    }

    void Panic()
    {
        if (status != 2)
        {
            return;
        }

        TurnRun(true);

        if (target == null || targetIndex == -1 || dangerousStatusOfPoints[targetIndex])
        {
            targetIndex = -1;
            float minDist = 0;

            for (int i = 0; i < wayPoints.Length; i++)
            {
                if (!dangerousStatusOfPoints[i] 
                    && (targetIndex == -1 || Vector3.Distance(transform.position, wayPoints[i].position) < minDist))
                {
                    targetIndex = i;
                    minDist = Vector3.Distance(transform.position, wayPoints[i].position);
                }
            }

            target = targetIndex == -1 ? null : wayPoints[targetIndex];
        }

        if (targetIndex == -1)
        {
            Move(Vector3.zero, false);
        }
        else
        {
            Move(target.position);
        }
    }

    void AttackHands()
    {
        if (status != 3 || target == null)
        {
            personScript.isAiming = personScript.isShooting = false;
            needMoveToOpponent = false;
            return;
        }

        personScript.weaponIndex = 0;
        TurnRun(true);

        Move(target.position);

        personScript.rotateAimAngle = Quaternion.LookRotation(target.position - transform.position).eulerAngles.y;

        float distanceToTarget = Vector3.Distance(transform.position - transform.position.y * Vector3.up, 
                                                  target.position - target.position.y * Vector3.up);
        float heightDifference = Mathf.Abs(transform.position.y - target.position.y);

        if (navMeshAgent.remainingDistance <= stoppingDistanceForIdle && heightDifference < 1)
        {
            personScript.isAiming = true;
            personScript.isShooting = true;

            if (distanceToTarget > attackDistance)
            {
                personScript.isAiming = personScript.isShooting = false;
                needMoveToOpponent = true;
            }
            else
            {
                personScript.isAiming = true;
                personScript.isShooting = true;
            }
        }
        else
        {
            personScript.isAiming = personScript.isShooting = false;
        }

        if (distanceToTarget <= attackDistance)
        {
            needMoveToOpponent = false;
        }
    }

    void AttackWeapon()
    {
        if (status != 4 || target == null)
        {
            return;
        }

        TurnRun(true);

        Transform targetChest = target.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);

        personScript.weaponIndex = maxWeaponIndex;
        personScript.isAiming = true;
        lookTarget.position = targetChest.position;
        lookTarget.rotation = transform.rotation;
        Vector3 buffVec = transform.InverseTransformPoint(targetChest.position) - Vector3.up * characterController.height * 0.8f;
        lookTarget.localRotation = Quaternion.Euler(-Mathf.Atan2(buffVec.y, buffVec.z) * 180 / Mathf.PI, lookTarget.localRotation.y, -90);
        personScript.lookTarget = lookTarget;
        personScript.rotateAimAngle = Quaternion.LookRotation(target.position - transform.position).eulerAngles.y;

        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * characterController.height * 0.8f;
        Ray ray = new Ray(origin, targetChest.position - origin);
        float dist = Vector3.Distance(transform.position + Vector3.up * characterController.height * 0.8f, targetChest.position);
        Debug.DrawRay(origin, targetChest.position - origin, Color.yellow);
        if (Physics.Raycast(ray, out hit, dist, aimObstacleLayerMask)/* && hit.transform != transform*//* && hit.transform != target*/)
        {
            Move(target.position);
            personScript.isShooting = false;

            if (!personScript.isPunching)
            {
                personScript.aimObject = null;
            }
        }
        else
        {
            Move(Vector3.zero, false);
            personScript.isShooting = true;

            if (!personScript.isPunching)
            {
                int luckyNum = Random.Range(0, luckyCoefficientHitToHand + luckyCoefficientHitToLeg  + luckyCoefficientHitToTorso 
                                                                         + luckyCoefficientHitToHead + unluckyCoefficient);

                if (luckyNum < luckyCoefficientHitToHead)
                {
                    //hit to head
                    personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).gameObject;
                }
                else if (luckyNum < luckyCoefficientHitToHead + luckyCoefficientHitToTorso)
                {
                    //hit to torso
                    personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest).gameObject;
                }
                else if (luckyNum < luckyCoefficientHitToHead + luckyCoefficientHitToTorso + luckyCoefficientHitToHand)
                {
                    //hit to hand

                    int nRandom = Random.Range(0, 2);

                    if (nRandom == 0) //left hand
                    {
                        personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
                    }
                    else //right hand
                    {
                        personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;
                    }
                }
                else if (luckyNum < luckyCoefficientHitToHead + luckyCoefficientHitToTorso + luckyCoefficientHitToHand + luckyCoefficientHitToLeg)
                {
                    //hit to leg

                    int nRandom = Random.Range(0, 2);

                    if (nRandom == 0) //left leg
                    {
                        personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperLeg).gameObject;
                    }
                    else //right leg
                    {
                        personScript.aimObject = target.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperLeg).gameObject;
                    }
                }
                else
                {
                    //don't hit
                }
            }
        }
    }

    public void Dead()
    {
        navMeshAgent.enabled = false;
        this.enabled = false;
    }
}
