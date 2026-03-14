using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour,ITakeDamage
{
    const string RUN_TRIGGER = "Run";
    const string CROUCH_TRIGGER = "Crouch";
    const string SHOOT_TRIGGER = "Shoot";

    [ SerializeField] private float startingHealth;
    [SerializeField] private float minTimeUnderCover;
    [SerializeField] private float maxTimeUnderCover;
    [SerializeField] private float minShootsToTake;
    [SerializeField] private float maxShootsToTake;
    [SerializeField] private float ratationSpeed;
    [SerializeField] private float damage;
    
    private float health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0,startingHealth);
        }
    }
    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        // Implement damage logic here, such as reducing health or playing a hit animation.
        Debug.Log("Enemy took damage from " + weapon.name);
    }
}
