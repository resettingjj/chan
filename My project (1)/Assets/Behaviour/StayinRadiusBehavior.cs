using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayinRadius")]
public class StayinRadiusBehavior : FlockBehaviourScript
{
    public Vector2 center;
    public float radius;

    public override Vector2 CalculateMove(Flock1 agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }
        return centerOffset * t * t;
    }
}
