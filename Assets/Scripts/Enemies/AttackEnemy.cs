
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AttackEnemy : MonoBehaviour
{
    public enum State { Chase, Charge }
    public State currentState = State.Chase;

    public Transform player;
    public float chaseSpeed = 3.5f;
    public float chargeSpeed = 8f;
    public float chaseDuration = 5f;
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;

    private NavMeshAgent agent;
    private float stateTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateTimer = chaseDuration;
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Chase:
                agent.speed = chaseSpeed;
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                if (stateTimer <= 0f)
                {
                    EnterChargeState();
                }
                break;

            case State.Charge:
                agent.speed = chargeSpeed;
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                break;
        }
    }

    void EnterChargeState()
    {
        currentState = State.Charge;
        agent.speed = chargeSpeed;
        // Optional: play a charge animation or effect
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == State.Charge && collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            GameObject explosion=    Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f); // Destroy effect after 2 seconds
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            if (hit.CompareTag("Player"))
            {
                // Call player damage logic here
                Debug.Log("Player hit by explosion!");
            }
        }

        Destroy(gameObject);
    }
}
