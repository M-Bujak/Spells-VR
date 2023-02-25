using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpellCaster : SpellCaster
{
    [field: SerializeField]
    public GameObject SpellEffect { get; set; }
    [field: SerializeField]
    public GameObject PlayerHand { get; set; }


    private void Start()
    {
        SpellEffect.SetActive(false);
    }

    public override void EnableCast()
    {
        base.EnableCast();
        if(IsBeingBlocked == true)
        {
            return;
        }

        SpellEffect.SetActive(true);
    }

    public override void DisableCast()
    {
        base.DisableCast();
        SpellEffect.SetActive(false);
    }

    protected override void CastSpell()
    {
        UpdateHandVFXPosition();
    }

    private void UpdateHandVFXPosition()
    {
        Vector3 positionOffset = new Vector3(0.08f, 0.05f, 0);

        SpellEffect.transform.position = PlayerHand.transform.TransformPoint(positionOffset);
        SpellEffect.transform.rotation = PlayerHand.transform.rotation;
        SpellEffect.transform.Rotate(new Vector3(90, 0, -90));
    }
}
