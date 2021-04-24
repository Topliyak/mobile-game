using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIControl))]
public class CitizenAsist : MonoBehaviour
{
    private AIControl aiControl;
    private PersonScript personScript;

    // Start is called before the first frame update
    void Start()
    {
        aiControl = GetComponent<AIControl>();
        personScript = GetComponent<PersonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiControl.enabled)
        {
            return;
        }

        if (aiControl.status == 1 && aiControl.targetIndex != -1 && aiControl.dangerousStatusOfPoints[aiControl.targetIndex])
        {
            aiControl.status = 2;
            return;
        }

        if (aiControl.status == 2 && aiControl.navMeshAgent.remainingDistance <= aiControl.stoppingDistanceForIdle)
        {
            aiControl.status = 1;
        }

        if (aiControl.attacker == null || !aiControl.attacker.GetComponent<PersonScript>().enabled)
        {
            if (aiControl.attacker != null)
            {
                aiControl.attacker = null;
                aiControl.target = null;
            }

            aiControl.status = 1;
            return;
        }
        else if (aiControl.status == 1 && personScript.dangerDegree >= aiControl.attacker.GetComponent<PersonScript>().dangerDegree - 1)
        {
            aiControl.target = aiControl.attacker.transform;
            aiControl.targetForShoot = aiControl.attacker.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
            aiControl.status = 3;
            return;
        }
    }
}
