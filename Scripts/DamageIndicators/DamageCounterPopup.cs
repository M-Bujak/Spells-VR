using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageCounterPopup : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshPro Text { get; set; }

    public PlayerBodyPositions playerBodyPositions;

    private float AnimationTime = 1f;
    private float TimeOfAnimationEnd;

    private void Awake()
    {
        TimeOfAnimationEnd = Time.time + AnimationTime;
    }

    private void Start()
    {
        LookAtHeadTransform();
    }

    public void Update()
    {
        if(Time.time > TimeOfAnimationEnd)
        {
            Destroy(gameObject);
        }

        LookAtHeadTransform();
    }

    private void LookAtHeadTransform()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - playerBodyPositions.head.position);
    }

    public void Setup(float damageAmount, bool isCritical)
    {
        Text.SetText(damageAmount.ToString());

        if(isCritical == true)
        {
            Text.color = Color.red;
        }

        transform.DOMoveY(transform.position.y + 1, 1).SetEase(Ease.OutBack);
        Text.DOFade(0, 0.5f).SetDelay(0.5f);
    }
}
