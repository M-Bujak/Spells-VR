using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroyableBehaviour : MonoBehaviour, IDestroyableBehaviour
{
    [field: SerializeField]
    public float MaxHealthPoints { get; set; }
    [field: SerializeField]
    private GameObject TowerIntactModel { get; set; }
    [field: SerializeField]
    private GameObject TowerRubbleModel { get; set; }
    [field: SerializeField]
    private AudioSource CrumbleAudioSource { get; set; }

    [field: SerializeField]
    public float ApproxHeightMiddle { get; private set; }


    public float HealthPoints { get; set; }
    public bool IsDemolished { get; private set; }
    public static Action OnTowerDemolished { get; set; }
    public Action OnDamageReceived = delegate { };

    void Start()
    {
        GameStateManager.Instance.OnInMainMenu += ResetTower;
        ResetTower();
    }

    public void ReceiveDamage(float damageAmount, bool isCritical)
    {
        HealthPoints -= damageAmount;
        OnDamageReceived();

        if (HealthPoints <= 0)
        {
            DemolishTower();
        }
    }

    private void DemolishTower()
    {
        IsDemolished = true;
        TowerIntactModel.SetActive(false);
        TowerRubbleModel.SetActive(true);
        CrumbleAudioSource.PlayOneShot(CrumbleAudioSource.clip);
        OnTowerDemolished?.Invoke();
    }

    private void ResetTower()
    {
        IsDemolished = false;
        TowerIntactModel.SetActive(true);
        TowerRubbleModel.SetActive(false);
        HealthPoints = MaxHealthPoints;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnInMainMenu -= ResetTower;
    }
}
