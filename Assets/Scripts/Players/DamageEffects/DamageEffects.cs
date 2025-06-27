// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;

// public class DamageEffects : MonoBehaviour
// {
//     [Header("Damage Effects")]
//     public Image damageOverlay;
//     public float flashDuration = 0.3f;
//     public ParticleSystem bloodEffect;
//     public Camera playerCamera;
    
//     [Header("Direction Indicator")]
//     public GameObject directionIndicatorPrefab;
//     public float indicatorDuration = 2f;
    
//     private LifeSystem lifeSystem;

//     void Start()
//     {
//         lifeSystem = GetComponent<LifeSystem>();
//         lifeSystem.OnTakeDamageWithSource.AddListener(HandleDamage);
//         damageOverlay.color = Color.clear;
//     }

//     private void HandleDamage(float damage, Vector3 damageSource)
//     {
//         ShowDamageEffects();
//         ShowDamageDirection(damageSource);
//     }

//     void ShowDamageEffects()
//     {
//         StartCoroutine(FlashDamage());
//         if(bloodEffect != null) bloodEffect.Play();
//     }
    
//     IEnumerator FlashDamage()
//     {
//         damageOverlay.color = new Color(1, 0, 0, 0.4f);
//         yield return new WaitForSeconds(flashDuration);
//         damageOverlay.color = Color.clear;
//     }
    
//     void ShowDamageDirection(Vector3 damageSourcePosition)
//     {
//         if(directionIndicatorPrefab == null) return;
        
//         Canvas canvas = FindObjectOfType<Canvas>();
//         if(canvas == null) 
//         {
//             Debug.LogError("No Canvas found in scene!");
//             return;
//         }
        
//         GameObject indicator = Instantiate(directionIndicatorPrefab, canvas.transform);
//         DamageIndicator indicatorScript = indicator.GetComponent<DamageIndicator>();
//         if(indicatorScript != null)
//         {
//             indicatorScript.Initialize(playerCamera, damageSourcePosition, indicatorDuration);
//         }
//     }
// }