using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballConditionsDetector : MonoBehaviour
{
    [field: SerializeField]
    private PlayerBodyPositions PlayerBodyPositions { get; set; }
    [field: SerializeField]
    private SpellCaster SpellCaster { get; set; }
    [field: SerializeField]
    private float HandHeightLowerLimit { get; set; }
    [field: Range(-1, 1)]
    [field: SerializeField]
    private float HandFacesOtherHandDotLowerLimit { get; set; }
    [field: Range(-1, 1)]
    [field: SerializeField]
    private float PerpendicularStateLowerLimit { get; set; }
    [field: Range(-1, 1)]
    [field: SerializeField]
    private float ShootDirectionInViewLowerLimit { get; set; }
    [field: SerializeField]
    private float DistanceUpperLimit { get; set; }

    public bool IsDetected { get; private set; }
    private float CurrentDotProduct { get; set; }
    private Vector3 ShootDirection { get; set; }
    public static Action<bool> OnChangeDetected { get; set; }
    public static Action<Vector3> OnShootConditionsMet { get; set; }


    private List<Vector3> SavedPositions { get; set; } = new List<Vector3>();
    private float LastSaveToQueue { get; set; }
    private Transform LeftHand { get; set; }
    private Transform RightHand { get; set; }
    private bool IsSwipeDetected { get; set; }

    private void Start()
    {
        LeftHand = PlayerBodyPositions.leftHand;
        RightHand = PlayerBodyPositions.rightHand;
    }

    void Update()
    {
        if (SpellCaster.IsBeingBlocked == true)
        {
            IsDetected = false;
            return;
        }

        if (AreHandsAboveLimit())
        {
            CheckConditions();

            if (IsDetected == true)
            {
                CheckIfPerpendicular();
            }
        }

        SavePosition();
    }

    private bool AreHandsAboveLimit()
    {
        return LeftHand.position.y > HandHeightLowerLimit &&
             RightHand.position.y > HandHeightLowerLimit;
    }

    private void CheckConditions()
    {
        bool previousIsDetectedValue = IsDetected;

        IsDetected = CheckIfHandFacesOtherHand(LeftHand, RightHand) &&
                     CheckIfHandFacesOtherHand(RightHand, LeftHand);

        if (IsDetected == true)
        {
            CheckDistance();
        }

        if (previousIsDetectedValue != IsDetected)
        {
            OnChangeDetected?.Invoke(IsDetected);
        }
    }

    private bool CheckIfHandFacesOtherHand(Transform hand, Transform otherHand)
    {
        Vector3 PalmFacingDirection = hand.up;
        if(hand == RightHand)
        {
            PalmFacingDirection *= -1;
        }

        Vector3 FromLeftToRight = (otherHand.position - hand.position).normalized;
        float DotFromLeftToRight = Vector3.Dot(FromLeftToRight, PalmFacingDirection);

        if (DotFromLeftToRight > HandFacesOtherHandDotLowerLimit)
        {
            return true;
        }

        return false;
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(LeftHand.position, RightHand.position);

        if (distance < DistanceUpperLimit)
        {
            IsDetected = true;
        }
        else
        {
            IsDetected = false;
        }
    }

    public void CheckIfPerpendicular()
    {
        CurrentDotProduct = Vector3.Dot(LeftHand.up, RightHand.up * -1);

        if (IsSwipeDetected == false)
        {
            return;
        }

        if (CurrentDotProduct > PerpendicularStateLowerLimit)
        {
            CalculateShotDirectionFromQueue();
            if (IsShotAwayFromPlayer())
            {
                OnShootConditionsMet?.Invoke(ShootDirection);
            }
        }
    }

    private bool IsShotAwayFromPlayer()
    {
        float dotProduct = Vector3.Dot(PlayerBodyPositions.head.forward, ShootDirection);

        if (dotProduct > ShootDirectionInViewLowerLimit)
        {
            return true;
        }

        return false;
    }

    private void CalculateShotDirectionFromQueue()
    {
        Vector3 sumVector = new Vector3();

        for (int i = 1; i < SavedPositions.Count; i++)
        {
            sumVector += SavedPositions[i] - SavedPositions[0];
        }

        ShootDirection = sumVector.normalized;
    }

    public void SetIsSwipeDetected(bool value)
    {
        IsSwipeDetected = value;
    }

    private void SavePosition()
    {
        if (LastSaveToQueue + Time.deltaTime >= 0.01)
        {
            SavedPositions.Add(RightHand.position);

            if (SavedPositions.Count > 5)
            {
                SavedPositions.RemoveAt(0);
            }
        }
    }
}
