using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/*[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehaviourScript
{
    public override Vector2 CalculateMove(Vector2 agentPos, UnsafeList<Vector2> context, Flock flock, Vector2 velocity)
    {
        if (context.Length == 0)
            return Vector2.zero;

        Vector2 AvoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Vector2 pos in context)
        {
            if (Vector2.SqrMagnitude(pos - agentPos) < flock.squareAvoidanceRadius)
            {
                nAvoid++;
                AvoidanceMove += (agentPos - pos);
            }
        }
        if (nAvoid > 0)
            AvoidanceMove /= nAvoid;
        return AvoidanceMove;
    }
}*/
