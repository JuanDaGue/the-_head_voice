using UnityEngine;

public class TestDamageIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Range(0.1f, 30f)]
    [SerializeField] private float destroyTimer = 20f;
    void Start()
    {
        InvokeRepeating(nameof(RegisterDamageIndicator), 0f, 0.1f);
        //Invoke("RegisterDamageIndicator", Random.Range(0.1f, 10f));
    }

    // Update is called once per frame
    void RegisterDamageIndicator()
    {
        if (!DI_system.CheckIfTheObjectInsight(this.transform))
        {
            DI_system.CreateDamageIndicator(this.transform);
        }
        Destroy(gameObject, destroyTimer);
    }
}
