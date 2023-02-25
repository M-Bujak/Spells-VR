using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyableBehaviour
{
    public Transform transform { get; }
    public string tag { get; set; }
    public float MaxHealthPoints { get; set; }
    public float HealthPoints { get; set; }
    public bool IsDemolished { get; }
    public float ApproxHeightMiddle { get; }

    public void ReceiveDamage(float damageAmount, bool isCritical);
}
