using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Aligament")]
public class AligamentBehaviour : FlockBehaviourScript
{
    public override Vector2 CalculateMove(Flock1 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return agent.transform.up;

        Vector2 AligamentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            AligamentMove += (Vector2)item.transform.up;
        }
        AligamentMove /= context.Count;

        return AligamentMove;
    }
}
