using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotationBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public float RotationSpeed { get; set; }

    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
    }
}
