using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/*[CreateAssetMenu(menuName = "Flock/Behavior/Aligament")]
public class AligamentBehaviour : FlockBehaviourScript
{
    public override Vector2 CalculateMove(Vector2 agentPos, UnsafeList<Vector2> context, Flock flock, Vector2 velocity)
    {
        if (context.Length == 0)
            return velocity;

        Vector2 AligamentMove = Vector2.zero;
        foreach (Vector2 pos in context)
        {
            AligamentMove += velocity;
        }
        AligamentMove /= context.Length;

        return AligamentMove;
    }
}*/
