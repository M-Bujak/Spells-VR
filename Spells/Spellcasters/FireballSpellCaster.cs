using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpellCaster : SpellCaster
{
    [field: SerializeField]
    private Fireball FireballPrefab { get; set; }

    private Fireball FireballInstance { get; set; }

    private void Start()
    {
        FireballConditionsDetector.OnChangeDetected += ChangeCastState;
    }

    protected override void Update() { }

    protected override void CastSpell()
    {
        if(FireballInstance == null || FireballInstance.IsShot == true)
        {
            FireballInstance = Instantiate(FireballPrefab);
        }
    }

    private void ChangeCastState(bool currentDetectorState)
    {
        if (IsBeingBlocked == true) return;

        if(currentDetectorState == true)
        {
            EnableCast();
            TryCastSpell();
        }

        if(currentDetectorState == false)
        {
            DisableCast();
            if (FireballInstance != null)
            {
                FireballInstance.CancelCast();
                FireballInstance = null;
            }
        }
    }

    public override void Block()
    {
        base.Block();
        if (FireballInstance != null)
        {
            FireballInstance.CancelCast();
        }
    }

    private void OnDestroy()
    {
        FireballConditionsDetector.OnChangeDetected -= ChangeCastState;
    }
}
