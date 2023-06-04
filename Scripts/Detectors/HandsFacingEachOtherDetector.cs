using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandsFacingEachOtherDetector : MonoBehaviour
{
    [field: SerializeField]
    private Transform LeftHand { get; set; }
    [field: SerializeField]
    private Transform RightHand { get; set; }
    [field: SerializeField]
    [Tooltip("The height above which hands have to be to allow detection.")]
    private float HandHeightLowerLimit { get; set; }

    public bool IsDetected { get; set; }
    public bool IsLeftHandFacingRight { get; set; }
    public bool IsRightHandFacingLeft { get; set; }

    public static Action<bool> OnChangeDetected { get; set; }

    private void Start()
    {
        HandFacesOtherHandDetector.OnDetected += ChangeConditionState;
    }

    void Update()
    {
        CheckConditions();
    }

    private void CheckConditions()
    {
        bool previousCondition = IsDetected;
        IsDetected = IsLeftHandFacingRight && IsRightHandFacingLeft;

        if (IsDetected != previousCondition)
        {
            OnChangeDetected?.Invoke(IsDetected);
        }
    }

    private void ChangeConditionState(Transform hand, bool isDetected)
    {
        /*
        This check was introduced to prevent the fireball from spawning
        and despawning when hand tracking isn't tracking.
        When tracking is lost the hands of the players are moved to their
        local 0,0,0. The lower limit makes sure that no changes are made
        when that happens.
        */

        if(hand.position.y < HandHeightLowerLimit)
        {
            return;
        }

        if(hand == LeftHand)
        {
            IsLeftHandFacingRight = isDetected;
        }

        if (hand == RightHand)
        {
            IsRightHandFacingLeft = isDetected;
        }
    }

    private void OnDestroy()
    {
        HandFacesOtherHandDetector.OnDetected -= ChangeConditionState;
    }
}
