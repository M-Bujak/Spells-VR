using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpellEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
        }
    }
}
