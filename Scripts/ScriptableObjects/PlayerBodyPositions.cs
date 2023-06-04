using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBody", menuName = "ScriptableObjects/PlayerBody", order = 1)]
public class PlayerBodyPositions : ScriptableObject
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
}
