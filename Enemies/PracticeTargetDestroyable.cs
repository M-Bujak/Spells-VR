using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTargetDestroyable : MonoBehaviour, IDestroyableBehaviour
{
    [field: SerializeField]
    public float ApproxHeightMiddle { get; private set; }
    public float MaxHealthPoints { get; set; }
    public float HealthPoints { get; set; }
    public bool IsDemolished { get; private set; }


    public void ReceiveDamage(float damageAmount, bool isCritical)
    {
        EnemyEvents.Damaged(this.transform.position + new Vector3(0, 0.1f, 0),
            damageAmount, isCritical);
    }
}
