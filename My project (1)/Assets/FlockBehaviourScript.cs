using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviourScript : ScriptableObject
{
    public abstract Vector2 CalculateMove(Flock1 agent, List<Transform> context, Flock flock);
}
