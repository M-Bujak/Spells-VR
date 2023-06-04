using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounterPopupManager : MonoBehaviour
{
    [field: SerializeField]
    private DamageCounterPopup DamageCounterPopupPrefab { get; set; }

    private void Start()
    {
        EnemyEvents.onDamaged += Create;
    }

    public void Create(Vector3 position, float damageAmount, bool isCritical)
    {
        DamageCounterPopup damageCounterPopup = 
            Instantiate(DamageCounterPopupPrefab, position, Quaternion.identity, transform);
        damageCounterPopup.Setup(damageAmount, isCritical);
    }

    private void OnDestroy()
    {
        EnemyEvents.onDamaged -= Create;
    }
}
