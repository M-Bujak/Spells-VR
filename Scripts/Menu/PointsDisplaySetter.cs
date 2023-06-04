using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplaySetter : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshPro Text { get; set; }

    private void OnEnable()
    {
        Text.SetText("Points: " + PointsManager.Instance.PointCount.ToString());
    }
}
