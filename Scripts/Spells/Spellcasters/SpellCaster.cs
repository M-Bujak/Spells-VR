using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellCaster : MonoBehaviour
{
    [field: SerializeField]
    public SpellCaster[] BlocksThese { get; private set; }
    [field: SerializeField]
    private SpellBlocker SpellBlocker { get; set; }

    protected bool IsBeingCast { get; set; }
    public bool IsBeingBlocked { get; protected set; }

    protected virtual void Update()
    {
        TryCastSpell();
    }

    protected virtual void TryCastSpell()
    {
        if (IsBeingCast == true)
        {
            CastSpell();
        }
    }
    protected abstract void CastSpell();

    public virtual void EnableCast()
    {
        if (IsBeingBlocked == true)
        {
            return;
        }

        IsBeingCast = true;
        SpellBlocker.BlockOthers(this);
    }

    public virtual void DisableCast()
    {
        IsBeingCast = false;
        SpellBlocker.UnblockOthers(this);
    }

    public virtual void Block()
    {
        IsBeingBlocked = true;
        IsBeingCast = false;
    }

    public virtual void Unblock()
    {
        IsBeingBlocked = false;
    }
}
