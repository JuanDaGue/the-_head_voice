using UnityEngine;

public class FlameGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Flame Gun Settings")]
    public float damage = 30f;
    public float destroyDelay = 0.2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
        private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("flame: " + other.name + "   The tag is " + other.tag);
        
        // Check if the collider is an impact target.
        if ( other.CompareTag("Enemy") )
        {

            // If the target has a LifeSystem component, apply damage.
            if (other.TryGetComponent<LifeSystem>(out var lifeSystem))
            {
                Debug.Log("flame: Collision with " + other.name);
                lifeSystem.TakeDamage(damage);
                
            }
            else
            {
                Debug.Log("flame: Collision with " + other.name + " but no LifeSystem component found.");
            }

            Destroy(gameObject, destroyDelay);
        }
    }
}
