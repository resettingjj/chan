using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayinRadius")]
public class StayinRadiusBehavior : FlockBehaviourScript
{
    public Vector2 center;
    public float radius;
    public Vector2 position;
    public Vector2 player;
    public override Vector2 CalculateMove(Vector2 agentPos, UnsafeList<Vector2> context, Flock flock, Vector2 velocity)
    {
        Vector2 centerOffset = center - agentPos;
        float t = centerOffset.magnitude / radius;
        Vector2 PlayerAvoid = player - agentPos;
        float c = PlayerAvoid.magnitude / radius;
        if (c < 0.2f)
        {
            return PlayerAvoid * c * c * -1;
        }
        if (t < 0.9f)
        {
            return Vector2.zero;
        }
        return centerOffset * t * t;
    }
}
