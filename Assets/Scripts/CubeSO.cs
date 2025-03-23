using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeSegmentSO", menuName = "Snake/SegmentData")]
public class SnakeSegmentSO : ScriptableObject
{
    public Color headColor = Color.green;
    public Color bodyColor = Color.yellow;
    public Color foodColor = Color.red;
    public float gridSize = 1f;
}
