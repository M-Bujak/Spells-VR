using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    [field: SerializeField]
    private ParticleSystem ExplosionEffect { get; set; }
    [field: SerializeField]
    private float DamageAmount { get; set; }

    private void Update()
    {
        if(ExplosionEffect.isPlaying != true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            other.GetComponent<IDestroyableBehaviour>()
                .ReceiveDamage(DamageAmount, false);
        }
    }
}
