using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToHand : MonoBehaviour
{
    [field: SerializeField]
    private bool ToLeftHand { get; set; }
    [field: SerializeField]
    public Vector3 PositionOffset { get; set; }
    [field: SerializeField]
    public Vector3 Rotation { get; set; }
    [field: SerializeField]
    private PlayerBodyPositions PlayerBodyPositions { get; set; }

    private ParticleSystem ParticleSystem { get; set; }
    private Transform Hand { get; set; }

    private void Awake()
    {
        ParticleSystem = gameObject.GetComponent<ParticleSystem>();

        Hand = ToLeftHand ? PlayerBodyPositions.leftHand : PlayerBodyPositions.rightHand;
    }

    private void OnEnable()
    {
        ParticleSystem.Play();
    }

    private void OnDisable()
    {
        ParticleSystem.Stop();
    }

    private void Update()
    {
        UpdatePosition();   
    }

    private void UpdatePosition()
    {
        transform.position = Hand.TransformPoint(PositionOffset);
        transform.rotation = Hand.transform.rotation;
        transform.Rotate(Rotation);
    }
}
