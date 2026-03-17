using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour,ITakeDamage
{
    const string RUN_TRRIGER = "Run";
    const string CROUCH_TRIGGER = "Crouch";
    const string SHHOT_TRIGGER = "Shoot";
    [SerializeField] private float startingHealth;
    [SerializeField] private float minTimeUnderCover;

    [SerializeField] private float maxTimeUnderCover;
    [SerializeField] private int minShootToTake;
    [SerializeField] private int maxShootToTake;
    [SerializeField] private float ratationSpeed;
    [SerializeField] private float damage;
    [Range(0,100)]
    [SerializeField] private float accuracy;
    [SerializeField] private float shootingInterval = 0.25f;
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private ParticleSystem bloodSplatterFx;
    
    private bool isShooting;
    private bool shootingRoutineStarted;
    private int currentShotsTaken;
    private int currentMaxShotsToTake;
    private NavMeshAgent agent;
    private Transform player;
    private Transform occupiedCoverSpot;
    private Animator animator;
    
    
    
    private float _health;

    public float health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, startingHealth);
        }
    }

    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint)
    {
        if (weapon == null) return;

        health -= weapon.GetDamage();
        if (bloodSplatterFx != null)
        {
            ParticleSystem splatter = Instantiate(bloodSplatterFx, contactPoint, Quaternion.identity);
            splatter.Play();
            Destroy(splatter.gameObject, splatter.main.duration);
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger(RUN_TRRIGER);
        _health = startingHealth;

    }

    public void Init(Transform player, Transform coverSpot)
    {
        this.player = player;
        occupiedCoverSpot = coverSpot;
        GetToCover();
    }

    private void GetToCover()
    {   agent.isStopped = false;
        agent.SetDestination(occupiedCoverSpot.position);
    }

    private void Update()
    {
        if (agent.isStopped ==false && (transform.position - occupiedCoverSpot.position).sqrMagnitude < 0.1f)
        {
            agent.isStopped = true;
            StartCoroutine(InitializeShootingCO());
        }

        if (isShooting)
        {
            RotateTowardsPlayer();
        }
    }

    private IEnumerator InitializeShootingCO()
    {
        HideBehindCover();
        yield return new WaitForSeconds(Random.Range(minTimeUnderCover, maxTimeUnderCover));
        StartShooting();
    }

    private void HideBehindCover()
    {
        animator.SetTrigger(CROUCH_TRIGGER);
    }


    private void StartShooting()
    {
        isShooting = true;
        currentMaxShotsToTake = UnityEngine.Random.Range(minShootToTake, maxShootToTake);
        currentShotsTaken = 0;
        animator.SetTrigger(SHHOT_TRIGGER);
    }   
    public void Shoot()
    {
        bool hitPlayer = Random.Range(0f, 100f) < accuracy;
        if (hitPlayer)
        {
            RaycastHit hit;
            Vector3 shootDirection = player.position - shootingPosition.position;
            if(Physics.Raycast(shootingPosition.position, shootDirection, out hit))
            //Debug.DrawLine(shootingPosition.position, hit.point, Color.red, 1f);
            {
                if (hit.transform == player)
                {
                    ITakeDamage damageable = player.GetComponent<ITakeDamage>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(null, null, hit.point);
                    }
                }
            }
        }

    }
    
    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0f; // Keep only the horizontal direction
        if (directionToPlayer.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ratationSpeed * Time.deltaTime);
        }
    }


}
