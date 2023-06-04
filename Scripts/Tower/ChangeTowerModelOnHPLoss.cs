using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTowerModelOnHPLoss : MonoBehaviour
{
    [field: SerializeField]
    private TowerDestroyableBehaviour Tower { get; set; }

    [field: SerializeField]
    private GameObject[] TowerModels { get; set; }

    [field: SerializeField]
    [Range(0, 1)]
    private float[] HPPercentagesToChangeOn;

    [Header("Effects")]
    [field: SerializeField]
    private AudioSource AudioOnChange;
    [field: SerializeField]
    private float ShakeDuration;
    [field: SerializeField]
    private float ShakeStrength;
    [field: SerializeField]
    private int ShakeVibrato;

    private int CurrentActiveModelIndex { get; set; } = 0;

    private void Start()
    {
        Tower.OnDamageReceived += CheckModelChangeConditions;
    }

    private void OnEnable()
    {
        CurrentActiveModelIndex = 0;
        ChangeModel(CurrentActiveModelIndex);
    }

    private void CheckModelChangeConditions()
    {
        for (int i = HPPercentagesToChangeOn.Length - 1; i >= 0; i--)
        {
            if (Tower.HealthPoints < HPPercentagesToChangeOn[i] * Tower.MaxHealthPoints)
            {
                if (i > CurrentActiveModelIndex)
                {
                    ChangeModel(i);
                    PlayEffects();
                    return;
                }
            }
        }
    }

    private void ChangeModel(int modelIndex)
    {
        for(int i = 0; i < TowerModels.Length; i++)
        {
            TowerModels[i].SetActive(false);
        }

        TowerModels[modelIndex].SetActive(true);
        CurrentActiveModelIndex = modelIndex;
    }

    private void PlayEffects()
    {
        AudioOnChange.Play();
        transform.DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibrato);
    }
}
