using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyableBehaviour : MonoBehaviour, IDestroyableBehaviour
{
    [field: SerializeField]
    public float MaxHealthPoints { get; set; }
    [field: SerializeField]
    private int PointsForDestroy { get; set; }
    [field: SerializeField]
    public float ApproxHeightMiddle { get; private set; }
    [field: SerializeField]
    private GameObject DeathVFX { get; set; }

    public float HealthPoints { get; set; }
    public bool IsDemolished { get; private set; }

    void Start()
    {
        HealthPoints = MaxHealthPoints;
    }

    public void ReceiveDamage(float damageAmount, bool isCritical)
    {
        HealthPoints -= damageAmount;
        EnemyEvents.Damaged(this.transform.position + new Vector3(0, ApproxHeightMiddle, 0),
            damageAmount, isCritical);

        if (HealthPoints <= 0)
        {
            Instantiate(DeathVFX,
                this.transform.position + new Vector3(0, ApproxHeightMiddle, 0),
                Quaternion.identity);
            Destroy(gameObject);
            PointsManager.Instance.AddPoints(PointsForDestroy);
            IsDemolished = true;
        }
    }
}
