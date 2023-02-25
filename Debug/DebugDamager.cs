using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDamager : MonoBehaviour
{
    [field: SerializeField]
    private EnemyDestroyableBehaviour DamagedEnemy { get; set; }

    [field: SerializeField]
    private float DamageAmount { get; set; }

    [field: SerializeField]
    private bool IsCritical { get; set; }

    [field: SerializeField]
    private float DamageInterval { get; set; }

    private float TimeOfLastDamage { get; set; }

    void Update()
    {
        if (Time.time > TimeOfLastDamage + DamageInterval)
        {
            DamagedEnemy.ReceiveDamage(DamageAmount, IsCritical);
        }
    }
}
