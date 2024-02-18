using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehaviourScript
{
    public override Vector2 CalculateMove(Vector2 agentPos, UnsafeList<Vector2> context, Flock flock, Vector2 velocity)
    {
        if (context.Length == 0)
            return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;
        foreach (Vector2 pos in context)
        {
            cohesionMove += pos;
        }
        cohesionMove /= context.Length;

        cohesionMove -= agentPos;
        return cohesionMove;
    }

    // Start is called before the first frame update

}
