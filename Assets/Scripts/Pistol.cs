using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Pistol : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    protected override void StartShooting(ActivateEventArgs args)
    {
        base.StartShooting(args);
        Shoot();
    }

    protected override void Shoot()
    {
        base.Shoot();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
    }

}
