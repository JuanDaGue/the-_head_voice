using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class Bullet : MonoBehaviour
{
    [Header("Core Settings")]
    public float speed = 20f;
    public float damage = 10f;
    public float lifetime = 5f;
    public Vector3 direction;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true; // Mantenemos Trigger para usar OnTriggerEnter
    }

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);

        // CORREGIDO: usamos .velocity en lugar de .linearVelocity
        rb.linearVelocity = direction.normalized * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // ❌ ELIMINADO: este filtro impide que disparemos a objetos trigger
        // if (other.isTrigger) return;

        Debug.Log($"[Bullet] Colisión con: {other.name}");

        OnHit(other);
    }

    /// <summary>
    /// Called when this bullet hits something.
    /// </summary>
    protected abstract void OnHit(Collider hit);
}
