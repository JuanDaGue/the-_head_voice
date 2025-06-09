using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : Enemy
{
    [Header("Sniper Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;
    public float zoomedFOV = 30f;    // optional: camera‐zoom effect  
    public float unzoomedFOV = 60f;

    protected override void DoAttack()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Raycast to ensure clear line-of-sight
        RaycastHit hit;
        Vector3 dir = (player.position + Vector3.up * 1.2f - firePoint.position).normalized;
        if (Physics.Raycast(firePoint.position, dir, out hit, attackRange))
        {
            // Instantiate and fire bullet
            GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
            if (b.TryGetComponent<Rigidbody>(out var rb))
                rb.linearVelocity = dir * bulletSpeed;
        }
    }

    protected override void Update()
    {
        // Optional: zoom in while aiming
        if (currentState == State.Chase || currentState == State.Attack)
        {
            Camera.main.fieldOfView = zoomedFOV;
        }
        else Camera.main.fieldOfView = unzoomedFOV;

        base.Update();
    }
}
