using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using UnityEditor.PackageManager;
using Unity.Collections;
using UnityEngine.AI;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class Flock : MonoBehaviour
{
    public Flock1 agentPrefab;
    List<Flock1> agents = new List<Flock1>();
    public float neighborRadius;
    public Transform player;

    [Range(10, 10000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;
    public float maxSpeed = 3f;
    public float Ali;
    public float coh;
    public float rad;
    public float avoid;
    public float driveFactor;
    float squareMaxspeed;
    float squareneighborRadius;
    public float squareAvoidanceRadius;

    public float avoidanceRaidiusMultipilter;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private JobHandle jobHandle;
    private FlockJob flockJob;
    NativeArray<Vector2> agentPos;
    NativeArray<Vector2> agentDir;
    NativeArray<Vector2> agentMove;
    NativeArray<bool> agentRemove;
    NativeArray<Color> agentColor;
    // Start is called before the first frame update


    //NativeArray<Vector2> tempAgentMove; // �߰�

    void Start()
    {
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
        };

        agentPos = new NativeArray<Vector2>(agents.Count, Allocator.Persistent);
        agentDir = new NativeArray<Vector2>(agents.Count, Allocator.Persistent);
        agentMove = new NativeArray<Vector2>(agents.Count, Allocator.Persistent);
        agentRemove = new NativeArray<bool>(agents.Count, Allocator.Persistent);
        agentColor = new NativeArray<Color>(agents.Count, Allocator.Persistent);

    }
    // Update is called once per frame


    void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agentPos[i] = agents[i].transform.position;
            if (agentDir[i] == null)
            {
                agentDir[i] = Vector2.zero;
            }
        }

        //tempAgentMove = new NativeArray<Vector2>(agents.Count, Allocator.TempJob); // �߰�

        flockJob = new FlockJob()
        {
            //flock = this,
            //fb = behaviour,
            dt = Time.deltaTime,
            maxspeed = maxSpeed,
            driveFactor = driveFactor,
            SqrMS = squareMaxspeed,
            SqrAV = squareAvoidanceRadius,
            neighborRadius = neighborRadius,
            avoidanceRaidiusMultipilter = avoidanceRaidiusMultipilter,
            PlayerPosition = (Vector2)player.position,
            agentPositions = agentPos,
            agentDirections = agentDir,
            Alig = new UnsafeList<Vector2>(0, Allocator.Temp),
            context = new UnsafeList<Vector2>(0, Allocator.Temp),
            agentmove = agentMove,
            //agentmove = tempAgentMove, // ����
            agentsToRemove = agentRemove,
            agentColors = agentColor,
            a = Ali,
            b=coh,
            c=avoid,
            d=rad
        };

        jobHandle = flockJob.Schedule(agents.Count, 30);
    }
    void LateUpdate() {
        jobHandle.Complete();
        for (int i = 0; i < agents.Count; i++) {
            //agents[i].Move(tempAgentMove[i]); // ����
            agentDir[i] = flockJob.agentmove[i];
            agents[i].Move(flockJob.agentmove[i]);
            agents[i].GetComponentInChildren<SpriteRenderer>().color = flockJob.agentColors[i];
            if (flockJob.agentsToRemove[i]) {
                Destroy(agents[i].gameObject);
                agents.RemoveAt(i);
                flockJob.agentsToRemove[i] = false;
            }
        }

    }
    void OnDestroy()
    {
        agentPos.Dispose();
        agentDir.Dispose();
        agentRemove.Dispose();
        agentMove.Dispose();
        agentColor.Dispose();

    }
    
    
    public struct FlockJob : IJobParallelFor
    {
        public float dt;
        public float driveFactor;
        public float maxspeed;
        public float neighborRadius;
        public float avoidanceRaidiusMultipilter;
        public float SqrMS;
        public float SqrAV;
        public float a;
        public float b;
        public float c;
        public float d;
        public Vector2 PlayerPosition;
        //public FlockBehaviourScript fb;
        //public Flock flock;
        [ReadOnly]
        public NativeArray<Vector2> agentPositions;
        [ReadOnly]
        public NativeArray<Vector2> agentDirections;   
        public NativeArray<bool> agentsToRemove;
        public UnsafeList<Vector2> context;
        public UnsafeList<Vector2> Alig;
        public NativeArray<Vector2> agentmove;
        public NativeArray<Color> agentColors;

        public void Execute(int index)
        {
            if (agentsToRemove[index]) { return; }

            float distanceToPlayer = (PlayerPosition - agentPositions[index]).magnitude;
            float avoidanceThreshold = avoidanceRaidiusMultipilter * avoidanceRaidiusMultipilter;
            bool remove = distanceToPlayer < avoidanceThreshold*4;
            agentsToRemove[index] = remove;


            //�ֺ� ��ü���� ��ġ�� context�� ����
            context.Clear();
            Alig.Clear();
            for (int i = 0; i < agentPositions.Length; i++)
            {
                if (Vector2.Distance(agentPositions[index], agentPositions[i]) < neighborRadius)
                {
                    context.Add(agentPositions[i]);
                    Alig.Add(agentDirections[i]);
                }
            }
            Vector2 move = Vector2.zero;
            move += Aligament(agentPositions[index], context, agentDirections[index], Alig) * a;
            move += SteeredCohesion(agentPositions[index], context, agentDirections[index], maxspeed, dt) * b;
            move += Avoidance(agentPositions[index], context, SqrAV) * c;
            move += StayinRadius(agentPositions[index],PlayerPosition) * d;
            //context ������� ������ ���
            move *= driveFactor;//���ӵ�? ������?
            if (move.sqrMagnitude > SqrMS)
            {
                move = move.normalized * maxspeed;//�ӵ� ����
            }
            agentmove[index] = move;
            agentColors[index] = Color.Lerp(Color.yellow, Color.red, context.Length / 6f);
        }

    }
    static Vector2 Aligament(Vector2 agentPos, UnsafeList<Vector2> context, Vector2 velocity, UnsafeList<Vector2> Al) {

        if (context.Length == 0)
            return velocity;

        Vector2 AligamentMove = Vector2.zero;
        for (int i = 0; i < context.Length; i++)
        {
            AligamentMove += Al[i];
        }
        AligamentMove /= context.Length;

        return AligamentMove;


    }

    static Vector2 Avoidance(Vector2 agentPos, UnsafeList<Vector2> context,float squareAvoidanceRadius) {

        if (context.Length == 0)
            return Vector2.zero;

        Vector2 AvoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Vector2 pos in context)
        {
            if (Vector2.SqrMagnitude(pos - agentPos) < squareAvoidanceRadius)
            {
                nAvoid++;
                AvoidanceMove += (agentPos - pos);
            }
        }
        if (nAvoid > 0)
            AvoidanceMove /= nAvoid;
        return AvoidanceMove;
    }
    static Vector2 currentVelocity;
    static float agentSmoothTime = 1.15f;
    static Vector2 SteeredCohesion(Vector2 agentPos, UnsafeList<Vector2> context, Vector2 velocity, float maxspeed, float dt)
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
        cohesionMove = Vector2.SmoothDamp(velocity, cohesionMove, ref currentVelocity, agentSmoothTime, maxspeed, dt);
        return cohesionMove;
    }
    static Vector2 center = Vector2.zero;
    static float radius = 37.9f;
    static Vector2 StayinRadius(Vector2 agentPos,Vector2 playerPos)
    {

        Vector2 centerOffset = center - agentPos;
        float t = centerOffset.magnitude / radius;
        Vector2 PlayerAvoid = playerPos - agentPos;
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
