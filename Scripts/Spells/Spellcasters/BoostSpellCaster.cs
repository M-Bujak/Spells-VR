using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpellCaster : SpellCaster
{
    [field: SerializeField]
    public GameObject HandVFX { get; set; }
    [field: SerializeField]
    public GameObject PlayerHand { get; set; }
    [field: SerializeField]
    private ShockSpellCaster BoostTarget { get; set; }


    private void Start()
    {
        HandVFX.SetActive(false);
    }

    public override void EnableCast()
    {
        base.EnableCast();
        if (IsBeingBlocked == false)
        {
            HandVFX.SetActive(true);
            BoostTarget.EnableBoost();
        }
    }

    public override void DisableCast()
    {
        base.DisableCast();
        HandVFX.SetActive(false);
        BoostTarget.DisableBoost();
    }

    protected override void CastSpell()
    {
        UpdateHandVFXPosition();
    }

    private void UpdateHandVFXPosition()
    {
        Vector3 positionOffset = new Vector3(0.08f, 0.05f, 0);

        HandVFX.transform.position = PlayerHand.transform.TransformPoint(positionOffset);
        HandVFX.transform.rotation = PlayerHand.transform.rotation;
        HandVFX.transform.Rotate(new Vector3(90, 0, 0));
    }
}
