using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    [Range(1, 4)]
    [Tooltip("1 - Leg\n2 - Hand\n3 - Torso\n4 - Head")]
    public int typeOfBody;
    public GameObject owner;
    public PersonScript ownerPersonScript;

    private void Start()
    {
        ownerPersonScript = owner.GetComponent<PersonScript>();
    }

    public void SayToOwnerAboutHit(float damage)
    {
        ownerPersonScript.GetDamage(typeOfBody, damage);
    }
}
