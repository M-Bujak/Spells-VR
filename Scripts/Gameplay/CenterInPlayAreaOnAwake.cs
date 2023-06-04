using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CenterInPlayAreaOnAwake : MonoBehaviour
{
    [field: SerializeField]
    private PlayerBodyPositions PlayerBodyPositions { get; set; }

    private float HeadXOnAwake { get; set; }

    private void Awake()
    {
        HeadXOnAwake = PlayerBodyPositions.head.position.x;
        StartCoroutine(CenterWhenDataValid());
    }

    private IEnumerator CenterWhenDataValid()
    {
        while (PlayerBodyPositions.head.position.x == HeadXOnAwake)
        {
            yield return null;
        }

        Vector3 PlayAreaCenter = FindPlayAreaCenter();
        MoveTargetPositionToZeroHorizontally(PlayAreaCenter);
    }

    private Vector3 FindPlayAreaCenter()
    {
        Vector3[] BoundaryGeometry =
            OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

        return BoundaryGeometry[0] + (BoundaryGeometry[2] - BoundaryGeometry[0]) / 2;
    }

    public void MoveTargetPositionToZeroHorizontally(Vector3 target)
    {
        Vector3 DistanceDifference = Vector3.zero - target;
        transform.position += new Vector3(DistanceDifference.x, 0, DistanceDifference.z);
    } 
}
