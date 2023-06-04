using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEvents
{
    public static event Action<Vector3, float, bool> onDamaged = delegate { };

    public static void Damaged(Vector3 enemyPosition, float damageAmount, bool isCritical)
    {
        onDamaged(enemyPosition, damageAmount, isCritical);
    }
}
