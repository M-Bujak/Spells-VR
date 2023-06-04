using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [field: SerializeField]
    protected float Damage { get; set; }
    
    [field: SerializeField]
    protected EnemyController Controller { get; set; }

    [field: SerializeField]
    protected float AttackInterval { get; set; }

    [field: SerializeField]
    protected float AttackDistance { get; set; }
    [field: SerializeField]
    protected Animator AnimationController { get; set; }
    [field: SerializeField]
    protected AudioSource HitAudioSource { get; set; }

    protected bool IsAttacking { get; set; }
    protected float TimeOfLastAttack { get; set; }
    protected IDestroyableBehaviour AttackTarget { get; set; }

    public void Update()
    {
        TryAttack();
    }

    protected virtual void TryAttack()
    {
        if (AttackTarget == null || AttackTarget.IsDemolished == true)
        {
            StopAttacking();
            return;
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

    protected virtual void Attack()
    {
        if (AttackTarget.IsDemolished == false)
        {
            AnimationController.SetTrigger("SwingAttack");
        }
        else
        {
            StopAttacking();
        }
    }

    private void DealDamageOnAnimationEvent()
    {
        if (AttackTarget != null && AttackTarget.IsDemolished == false)
        {
            AttackTarget.ReceiveDamage(Damage, false);
            HitAudioSource.Play();
        }
    }

    protected virtual void StartAttacking()
    {
        if (AttackTarget.IsDemolished == false)
        {
            IsAttacking = true;
            Controller.StopWalking();
            AnimationController.SetBool("IsAttacking", true);
        }
    }

    public virtual void StopAttacking()
    {
        IsAttacking = false;
        AnimationController.SetBool("IsAttacking", false);
        AttackTarget = null;
    }

    public void SetTarget(IDestroyableBehaviour target)
    {
        AttackTarget = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (AttackTarget != null)
        {
            if (other.tag == AttackTarget.tag)
            {
                StartAttacking();
            }
        }
    }
}
