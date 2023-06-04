using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFacesOtherHandDetector : MonoBehaviour
{
    public static Action<Transform, bool> OnDetected { get; set; }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(BoxCollider))
        {
            OnDetected?.Invoke(transform, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetType() == typeof(BoxCollider))
        {
            OnDetected?.Invoke(transform, false);

        }
    }
}
