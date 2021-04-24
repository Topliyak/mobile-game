using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIControl))]
public class PoliceOfficerAsist : MonoBehaviour
{
    public GameObject criminal = null;

    private AIControl aiControl;

    // Start is called before the first frame update
    void Start()
    {
        aiControl = GetComponent<AIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (criminal == null)
        {
            aiControl.targetForShoot = null;
            aiControl.status = 1;
            return;
        }

        PersonScript criminalPS = criminal.GetComponent<PersonScript>();

        if (!criminalPS.enabled)
        {
            criminal = null;
            aiControl.targetForShoot = null;
            aiControl.target = null;
            aiControl.status = 1;
            return;
        }

        if (criminalPS.dangerDegree == 0)
        {
            criminal = null;
            aiControl.targetForShoot = null;
            aiControl.target = null;
            aiControl.status = 1;
            return;
        }

        if (criminalPS.dangerDegree <= 2)
        {
            aiControl.target = criminal.transform;
            aiControl.targetForShoot = criminal.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
            aiControl.status = 3;
            return;
        }

        if (criminalPS.dangerDegree == 3)
        {
            aiControl.target = criminal.transform;
            aiControl.targetForShoot = criminal.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
            aiControl.status = 4;
            aiControl.maxWeaponIndex = 1;
            return;
        }

        if (criminalPS.dangerDegree > 3)
        {
            aiControl.target = criminal.transform;
            aiControl.targetForShoot = criminal.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
            aiControl.status = 4;
            aiControl.maxWeaponIndex = 2;
            return;
        }
    }
}
