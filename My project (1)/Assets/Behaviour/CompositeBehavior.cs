using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
/*[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehaviourScript
{
    public FlockBehaviourScript[] behaviors;
    public float[] weights;




    public override Vector2 CalculateMove(Vector2 agentPos, UnsafeList<Vector2> context, Flock flock, Vector2 velocity)
    {
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismtch in " + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agentPos, context, flock, velocity) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }
        return move;
    }*/



    //public override Vector2 CalculateMove(Flock1 agent, List<Transform> context, Flock flock)
    //{
    //    if (weights.Length != behaviors.Length) 
    //    {
    //        Debug.LogError("Data mismtch in " + name, this);
    //        return Vector2.zero;
    //    }

    //    Vector2 move = Vector2.zero;
    //    for (int i = 0; i < behaviors.Length; i++)
    //    {
    //        Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

    //        if (partialMove != Vector2.zero)
    //        {
    //            if (partialMove.sqrMagnitude > weights[i] * weights[i]) 
    //            {
    //                partialMove.Normalize();
    //                partialMove *= weights[i];
    //            }

    //            move += partialMove;
    //        }
    //    }
    //    return move;
    //}
//}
