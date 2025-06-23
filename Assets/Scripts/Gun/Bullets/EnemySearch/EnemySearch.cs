using UnityEngine;
using System.Collections.Generic;

public class EnemySearch : Bullet
{
    [Header("Enemy Search Settings")]
    public float detectionRadius = 10f;
    public float detectionAngle = 60f;
    public float searchDelay = 0.2f;

    private List<Transform> detectedEnemies = new List<Transform>();
    private int currentTargetIndex = 0;
    private Transform currentTarget;

    private bool initialized = false;

    protected override void Start()
    {
        base.Start(); // importante para que se aplique la velocidad inicial

        // Desactiva el movimiento inicial hasta encontrar enemigos
        //rb.linearVelocity = Vector3.zero;

        // Inicia búsqueda
        //nvoke(nameof(DetectEnemies), searchDelay);
        Debug.Log("EnemySearch started.");
        DetectEnemies();
    }

    void DetectEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log($"Found {allEnemies.Length} enemies in the scene.");
        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= detectionRadius)
            {
                Vector3 dirToTarget = (enemy.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(direction, dirToTarget);
                if (angle <= detectionAngle / 2f)
                {
                    detectedEnemies.Add(enemy.transform);
                }
            }
        }

        if (detectedEnemies.Count > 0)
        {
            currentTarget = detectedEnemies[0];
            initialized = true;
        }
        else
        {
            Debug.LogWarning("No enemies detected within range.");
            //Destroy(gameObject); // sin objetivos
        }
    }

    void Update()
    {
        if (!initialized || currentTarget == null) return;

        // Mover hacia el objetivo actual
        Vector3 toTarget = (currentTarget.position - transform.position).normalized;
        rb.linearVelocity = toTarget * speed;

        // Rotar visualmente hacia el enemigo
        transform.rotation = Quaternion.LookRotation(toTarget);

        // Comprobar si está muy cerca
        if (Vector3.Distance(transform.position, currentTarget.position) < 1f)
        {
            ApplyDamage(currentTarget);
            currentTargetIndex++;

            if (currentTargetIndex < detectedEnemies.Count)
            {
                currentTarget = detectedEnemies[currentTargetIndex];
            }
            else
            {
               Debug.LogWarning("No more enemies to search for.");
                //Destroy(gameObject);
            }
        }
    }

    void ApplyDamage(Transform enemy)
    {
        var life = enemy.GetComponent<LifeSystem>();
        if (life != null)
        {
            life.TakeDamage(damage);
        }
    }

    protected override void OnHit(Collider hit)
    {
        // Solo daña si choca con algo no planificado
        var enemy = hit.GetComponent<LifeSystem>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        //Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
