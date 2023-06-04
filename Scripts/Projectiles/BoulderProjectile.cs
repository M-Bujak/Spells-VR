using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BoulderProjectile : MonoBehaviour
{
    [field: SerializeField]
    private float Speed { get; set; }
    [field: SerializeField]
    private float RotationSpeed { get; set; }
    [field: SerializeField]
    private float PathPeakHeightOffset { get; set; }
    [field: SerializeField]
    private AnimationCurve ProjectilePathCurve;
    [field: SerializeField]
    private PlayerBodyPositions targetBodyPositions;
    [field: SerializeField]
    private GameObject AudioOnDestroy { get; set; }

    private Vector3 Target { get; set; }
    private float PathPeakHeight { get; set; }
    private Vector3 TargetHorizontalPosition { get; set; }
    private Vector3 LandingTarget { get; set; }
    private float TargetDistance { get; set; }

    void Start()
    {
        Target = targetBodyPositions.head.position;
        transform.LookAt(Target);

        TargetDistance = HorizontalVectorDistance(Target, transform.position);

        CalculateLandingPosition();
        PathPeakHeight = Target.y + PathPeakHeightOffset;
    }

    void Update()
    {
        ChangeCurrentHeight();
        MoveTowardsLanding();
        CheckIfLanded();
        RotateProjectile();
    }

    private void CalculateLandingPosition()
    {
        Vector3 TargetDirection = (Target - transform.position).normalized;
        LandingTarget = transform.position + TargetDirection * TargetDistance * 2;
        LandingTarget = new Vector3(LandingTarget.x, 0, LandingTarget.z);

        TargetHorizontalPosition = new Vector3(LandingTarget.x, 0, LandingTarget.z);
    }

    private void ChangeCurrentHeight()
    {
        float distanceToLanding = HorizontalVectorDistance(LandingTarget, transform.position);
        float y = ProjectilePathCurve.Evaluate(distanceToLanding / (TargetDistance * 2.0f)) * PathPeakHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void MoveTowardsLanding()
    {
        TargetHorizontalPosition = new Vector3(TargetHorizontalPosition.x, transform.position.y, TargetHorizontalPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, TargetHorizontalPosition, Speed * Time.deltaTime);
    }

    private void CheckIfLanded()
    {
        if (Vector3.Distance(transform.position, LandingTarget) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void RotateProjectile()
    {
        transform.Rotate(RotationSpeed, 0, 0);
    }

    private float HorizontalVectorDistance(Vector3 vector1, Vector3 vector2)
    {
        float xDiff = vector1.x - vector2.x;
        float zDiff = vector1.z - vector2.z;
        return Mathf.Sqrt((xDiff * xDiff) + (zDiff * zDiff));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(AudioOnDestroy, transform.position, Quaternion.identity);
    }
}
