using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance { get; private set; }

    public int PointCount { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStateManager.Instance.OnInMainMenu += ResetPointCount;
    }
    
    private void ResetPointCount()
    {
        PointCount = 0;
    }

    public void AddPoints(int points)
    {
        PointCount += points;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnInMainMenu -= ResetPointCount;
    }
}
