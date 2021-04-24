using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponScript : MonoBehaviour
{
    private bool canShoot = true;
    public float delay;
    public int bullets;
    private ParticleSystem particleControl;
    private AudioSource weaponSound;

    private void Start()
    {
        particleControl = transform.Find("WeaponParticle").GetComponent<ParticleSystem>();
        weaponSound = GetComponent<AudioSource>();
    }

    public bool Shoot()
    {
        if (!canShoot)
        {
            return false;
        }

        canShoot = false;
        particleControl.Play(true);
        weaponSound.Play();
        Invoke("CanShootTrue", delay);

        return true;
    }

    private void CanShootTrue()
    {
        canShoot = true;
    }
}
