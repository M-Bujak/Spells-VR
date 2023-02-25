using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [field: SerializeField]
    private PlayerBodyPositions PlayerBodyPositions;
    [field: SerializeField]
    private Vector3 FireballPositionOffset { get; set; }
    [field: SerializeField]
    private float FireballSpeed { get; set; }
    [field: SerializeField]
    private float LifeTimeAfterShot { get; set; }
    [field: SerializeField]
    private FireballExplosion FireballExplosion { get; set; }
    [field: SerializeField]
    private SphereCollider SphereCollider { get; set; }
    [field: SerializeField]
    private AudioSource WhooshAudioSource { get; set; }

    public FireballSpellCaster FireballSpellCaster { get; set; }
    public bool IsShot { get; private set; }
    private Vector3 ShootDirection { get; set; }
    private float TimePassedAfterShot { get; set; }

    private void Awake()
    {
        UpdatePositionWithOffset();
    }

    private void Start()
    {
        FireballConditionsDetector.OnShootConditionsMet += ShootFireball;
    }

    private void Update()
    {
        if (IsShot == false)
        {
            UpdatePositionWithOffset();
        }
        else
        {
            MoveInShotDirection();
        }
    }

    private void UpdatePositionWithOffset()
    {
        transform.position = PlayerBodyPositions.leftHand.TransformPoint(FireballPositionOffset) +
        (PlayerBodyPositions.rightHand.TransformPoint(FireballPositionOffset * -1) -
        PlayerBodyPositions.leftHand.TransformPoint(FireballPositionOffset)) / 2;
    }

    public void CancelCast()
    {
        if (IsShot == false)
        {
            Destroy(gameObject);
        }
    }

    private void ShootFireball(Vector3 direction)
    {
        if (IsShot == false)
        {
            WhooshAudioSource.Play();
            ShootDirection = direction;
            IsShot = true;
            transform.LookAt(transform.position + ShootDirection);
            transform.Rotate(-90.0f, 0.0f, 0.0f);
            StartCoroutine(LifeTimeTimer());
            FireballConditionsDetector.OnShootConditionsMet -= ShootFireball;
        }
    }

    IEnumerator LifeTimeTimer()
    {
        while (TimePassedAfterShot < LifeTimeAfterShot)
        {
            TimePassedAfterShot += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void MoveInShotDirection()
    {
        transform.position += ShootDirection * Time.deltaTime * FireballSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            CancelCast();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 ||
            other.gameObject.layer == 6 ||
            other.gameObject.layer == 12)
        {
            Instantiate(FireballExplosion, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 12)
        {
            ScaleUpCollider();
        }
    }

    private void ScaleUpCollider()
    {
        SphereCollider.radius += 0.3f;
    }

    private void ScaleDownCollider()
    {
        SphereCollider.radius -= 0.3f;
    }

    private void OnDestroy()
    {
        FireballConditionsDetector.OnShootConditionsMet -= ShootFireball;
    }
}
