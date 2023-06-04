using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectSetter : MonoBehaviour
{
    [field: SerializeField]
    private Transform _headTransform;

    [field: SerializeField]
    private Transform _leftHand;

    [field: SerializeField]
    private Transform _rightHand;

    [field: SerializeField]
    private PlayerBodyPositions _playerBodyPositions;

    private void Awake()
    {
        _playerBodyPositions.head = _headTransform;
        _playerBodyPositions.leftHand = _leftHand;
        _playerBodyPositions.rightHand = _rightHand;
    }
}
