using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockSpellCaster : SpellCaster
{
    [SerializeField]
    public OVRSkeleton skeleton;

    [field: SerializeField]
    public float DamageAmount { get; set; }
    [field: SerializeField]
    private float DamageDealInterval { get; set; }
    [field: SerializeField]
    private float DamageBoostMultiplier { get; set; }
    [field: SerializeField]
    private float DamageDealIntervalBoostMultiplier { get; set; }
    [field: SerializeField]
    private LayerMask EnemyLayer { get; set; }
    [field: SerializeField]
    private LineRenderer LineRenderer { get; set; }
    [field: SerializeField]
    private GameObject LightningVFX { get; set; }
    [field: SerializeField]
    private Vector3 VFXOffsetY { get; set; }
    [field: SerializeField]
    private LayerMask RaycastMask { get; set; }
    [field: SerializeField]
    private float SphereCastRadius { get; set; }


    private OVRBone IndexFingerTip { get; set; }
    private OVRBone IndexFingerKnuckle { get; set; }
    private bool IsBeingBoosted { get; set; }
    private float TimeOfLastDamage { get; set; }



    void Start()
    {
        LineRenderer.positionCount = 2;

        StartCoroutine(LoadSkeletonBones());
    }

    IEnumerator LoadSkeletonBones()
    {
        yield return WaitUntilSkeletonLoaded();
        SetIndexFingertip();
    }

    IEnumerator WaitUntilSkeletonLoaded()
    {
        yield return new WaitUntil(() => skeleton.Bones.Count > 0);
    }

    private void SetIndexFingertip()
    {
        for (int i = 0; i < skeleton.Bones.Count; i++)
        {
            if (skeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_IndexTip)
            {
                IndexFingerTip = skeleton.Bones[i];
                break;
            }

            if (skeleton.Bones[i].Id == OVRSkeleton.BoneId.Hand_Index1)
            {
                IndexFingerKnuckle = skeleton.Bones[i];
            }
        }
    }

    public override void DisableCast()
    {
        base.DisableCast();
        LightningVFX.SetActive(false);
    }

    protected override void CastSpell()
    {
        RaycastHit hit = new RaycastHit();
        bool isHit = Physics.Raycast(IndexFingerKnuckle.Transform.position,
                     IndexFingerKnuckle.Transform.right,
                     out hit, 10, RaycastMask);

        if (isHit == true && hit.collider.gameObject.layer == 6)
        {
            ShootLightning(IndexFingerTip.Transform.position, hit.transform.position);
            DealDamage(hit.collider);
            return;
        }

        isHit = RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(IndexFingerKnuckle.Transform.position,
                SphereCastRadius, IndexFingerKnuckle.Transform.right, out hit, 10,
                RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor);

        if (isHit == true && hit.collider.gameObject.layer == 6)
        {
            DealDamage(hit.collider);
            return;
        }

        LightningVFX.SetActive(false);
    }

    private void DealDamage(Collider collider)
    {
        IDestroyableBehaviour destroyableBehaviour = collider.GetComponent<IDestroyableBehaviour>();
        VFXOffsetY = new Vector3(0, destroyableBehaviour.ApproxHeightMiddle, 0);
        ShootLightning(IndexFingerTip.Transform.position, collider.transform.position);

        if (TimeOfLastDamage + DamageDealInterval < Time.time)
        {
            destroyableBehaviour.ReceiveDamage(DamageAmount, IsBeingBoosted);
            TimeOfLastDamage = Time.time;
        }
    }

    private void ShootLightning(Vector3 start, Vector3 end)
    {
        if (LightningVFX.activeSelf == false)
        {
            LightningVFX.SetActive(true);
        }
        LightningVFX.transform.position = start;
        LightningVFX.transform.LookAt(end + VFXOffsetY);
    }

    public void EnableBoost()
    {
        if (IsBeingBoosted == true) return;

        DamageAmount *= DamageBoostMultiplier;
        DamageDealInterval *= DamageDealIntervalBoostMultiplier;
        IsBeingBoosted = true;
    }

    public void DisableBoost()
    {
        if (IsBeingBoosted == false) return;

        DamageAmount /= DamageBoostMultiplier;
        DamageDealInterval /= DamageDealIntervalBoostMultiplier;
        IsBeingBoosted = false;
    }
}
