using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Transform player;
    public Flock1 agentPrefab;
    List<Flock1> agents = new List<Flock1>();
    public FlockBehaviourScript behaviour;

    [Range(10, 10000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 20f;
    [Range(1f, 100f)]
    public float maxspeed = 20f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRaidiusMultipilter = 0.5f;

    float squareMaxspeed;
    float squareneighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxspeed = maxspeed * maxspeed;
        squareneighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareneighborRadius * avoidanceRaidiusMultipilter * avoidanceRaidiusMultipilter;

        for (int i = 0; i < startingCount; i++)
        {
            Flock1 newAgent = Instantiate(
                agentPrefab,
                UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 300f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }
    public StayinRadiusBehavior srb;
    // Update is called once per frame
    void Update()
    {
        srb.player = (Vector2)player.position;
        foreach (Flock1 agent in agents)
        {
            List<Transform> context = getNearbyObjects(agent);
            agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxspeed)
            {
                move = move.normalized * maxspeed;
            }
            agent.Move(move);
            Vector2 PlayerAvoid = srb.player - (Vector2)agent.transform.position;
            float c = PlayerAvoid.magnitude / 38;
            if (c < 0.03f)
            {
                agent.gameObject.SetActive(false);
                Destroy(agent);
            }
        }
    }

    private void LateUpdate(){
        agents.RemoveAll(s => s == null);
    }
    List<Transform> getNearbyObjects(Flock1 agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColloders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in  contextColloders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
