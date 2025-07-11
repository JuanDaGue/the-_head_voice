using System;
using System.Collections.Generic;
using UnityEngine;

public class DI_system : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Damage Indicator System")]
    [SerializeField] private DamageIndicator damageIndicatorPrefab=null;
    [SerializeField] private RectTransform holder= null;
    [SerializeField] private new Camera camera= null;
    [SerializeField] private Transform playerTransform= null;
    private Dictionary<Transform, DamageIndicator> damageIndicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static Action< Transform> CreateDamageIndicator = delegate {};
    public static Func<Transform, bool> CheckIfTheObjectInsight = null;
    #endregion

    private void OnEnable()
    {
        CreateDamageIndicator += Create;
        CheckIfTheObjectInsight += InSigth;
    }
    private void OnDisable()
    {
      CreateDamageIndicator -= Create;
        CheckIfTheObjectInsight -= InSigth;
    }

    void Create(Transform target)
    {
        if (damageIndicators.ContainsKey(target))
        {
            damageIndicators[target].RestartTimer();
            return;
        }
        DamageIndicator newDamageIndicator = Instantiate(damageIndicatorPrefab, holder);
        newDamageIndicator.Register(target, playerTransform, new Action (() =>
        {
            damageIndicators.Remove(target);
        }));
        damageIndicators.Add(target, newDamageIndicator);
    }
    bool InSigth(Transform target)
    {
        Vector3 screenPos = camera.WorldToViewportPoint(target.position);
    return screenPos.x > 0 && screenPos.x < 1 && screenPos.y > 0 && screenPos.y < 1 && screenPos.z > 0;
    }
    


    
}  
    

