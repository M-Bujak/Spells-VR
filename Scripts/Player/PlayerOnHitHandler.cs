using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHitHandler : MonoBehaviour
{
    public static Action OnPlayerHit { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            OnPlayerHit?.Invoke();
        }
    }
}
