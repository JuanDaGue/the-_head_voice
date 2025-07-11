using System;
using System.Collections;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private const float MaxTimer = 8.0f;
    private float timer = MaxTimer;
    private CanvasGroup canvasGroup = null;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    protected RectTransform rectTransform = null;
    protected RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
                if (rectTransform == null)
                {
                    rectTransform = gameObject.AddComponent<RectTransform>();
                }
            }
            return rectTransform;
        }
    }
    public Transform Target { get; protected set; }
    = null;
    private Transform playerTransform = null;

    private IEnumerator IE_Countdown = null;
    private Action unRegister = null;
    private Quaternion tRot = Quaternion.identity;
    private Vector3 tPos = Vector3.zero;
    public void Register(Transform target, Transform PlayerTransform, Action unRegisterAction)
    {
        Target = target;
        playerTransform = PlayerTransform;
        unRegister = unRegisterAction;

        StartCoroutine(RotateToTarget());
        StartTimer();
    }

    private void StartTimer()
    {
        if (IE_Countdown != null)
        {
            StopCoroutine(IE_Countdown);
        }
        IE_Countdown = RotateToTarget();
        StartCoroutine(IE_Countdown );
    }

    IEnumerator RotateToTarget()
    {
        while (enabled)
        {
            if (Target )
            {
                tRot = Target.rotation;
                tPos = Target.position;
            }
            tRot = Quaternion.LookRotation(playerTransform.position - tPos, Vector3.up);
            tRot.z = -tRot.y;
            tRot.y = 0f;
            tRot.x = 0f;
            Vector3 north = new Vector3(0f, 0f, playerTransform.eulerAngles.y);
            RectTransform.localRotation = Quaternion.Euler(north) * tRot;
            yield return null;
        }
    }
    private IEnumerator Countdown()
    {
        Debug.Log("Start Countdown");
        while (CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha += Time.deltaTime * 4;
            yield return null;
        }
    
        while (timer > 0f)
        {
            timer --;
            yield return new WaitForSeconds(1f);
        }
        while (CanvasGroup.alpha > 0f)
        {
            CanvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        //unRegister?.Invoke();
        unRegister();
        Destroy(gameObject);
    }
    public void RestartTimer()
    {
        timer = MaxTimer;
        StartTimer();
    }
    // void Start()
    // {
    //     StartTimer();
    // }

    // void Update()
    // {
    //     if (timer <= 0f)
    //     {
    //         StartTimer();
    //     }
    // }
    
}
