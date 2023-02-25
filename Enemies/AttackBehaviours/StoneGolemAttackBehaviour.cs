using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StoneGolemAttackBehaviour : AttackBehaviour
{
    [field: SerializeField]
    private Transform BoulderPrefab { get; set; }

    private float DistanceAtBoulderThrow { get; set; }
    private int BouldersLeft { get; set; }

    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        BouldersLeft = 1;
        IsAttacking = false;
        Controller = GetComponent<EnemyController>();
        DistanceAtBoulderThrow = Random.Range(8, 6);
    }

    protected override void TryAttack()
    {
        if (AttackTarget == null || AttackTarget.IsDemolished == true)
        {
            StopAttacking();
            return;
        }

        if (BouldersLeft > 0)
        {
            float DistanceToTarget =
                Vector3.Distance(transform.position, AttackTarget.transform.position);

            if (DistanceToTarget <= DistanceAtBoulderThrow)
            {
                ThrowBoulder();
                BouldersLeft--;
            }
        }

        if (IsAttacking == true)
        {
            if (Time.time > TimeOfLastAttack + AttackInterval)
            {
                Attack();
                TimeOfLastAttack = Time.time;
            }
        }
    }

    private void ThrowBoulder()
    {
        Instantiate(BoulderPrefab, transform.position, Quaternion.identity, this.transform);
    }
}
