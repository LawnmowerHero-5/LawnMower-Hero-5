using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class transformVariable : ScriptableObject
{
    public Transform playerTransform;
    public int score;
    public float score2;

    [Range(1, 100)]public int gainPointsFromKills;
    [Range(1, 100)]public int loosePointsFromFriendlyKills;
}
