using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayinRadius")]
public class StayinRadiusBehavior : FlockBehaviourScript
{
    public Vector2 center;
    public float radius;
    public Vector2 position;
    public Vector2 player;
    public override Vector2 CalculateMove(Flock1 agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        Vector2 PlayerAvoid = player - (Vector2)agent.transform.position;
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
