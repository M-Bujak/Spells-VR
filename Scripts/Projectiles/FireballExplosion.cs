using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FireballExplosion : MonoBehaviour
{
    [field: SerializeField]
    private ParticleSystem ExplosionEffect { get; set; }
    [field: SerializeField]
    private SphereCollider Collider { get; set; }
    [field: SerializeField]
    private float BaseDamage { get; set; }

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
            float distance = Vector3.Distance(transform.position,
                other.ClosestPoint(transform.position));

            float damageFalloffPercentage = Mathf.Lerp(1, 0, distance);
            float damage = Mathf.Ceil(BaseDamage * damageFalloffPercentage);

            if(damage < 10)
            {
                damage = 10;
            }

            other.GetComponent<IDestroyableBehaviour>()
                .ReceiveDamage(damage, false);
        }
    }
}
