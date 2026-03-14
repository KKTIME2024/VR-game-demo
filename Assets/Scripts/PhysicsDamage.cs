using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PhysicsDamage : Projectile, ITakeDamage
{
    [SerializeField] private float lifetime;
    private Rigidbody rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    } 

    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
        Destroy(gameObject, lifetime);
    }
    
    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        rb.AddRelativeForce(Vector3.forward * weapon.GetShootingForce(), ForceMode.Impulse);  
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        ITakeDamage[] damageTakers = other.GetComponentsInChildren<ITakeDamage>();

        foreach (ITakeDamage damageTaker in damageTakers)
        {
            damageTaker.TakeDamage(weapon, this, transform.position);
        }
    }
}
