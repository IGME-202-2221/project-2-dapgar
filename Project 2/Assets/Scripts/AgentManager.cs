using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;

    [SerializeField] Agent agentPrefab;

    [SerializeField] int agentSpawnCount;

    [HideInInspector]
    public List<Agent> agents = new List<Agent>();

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;

    [HideInInspector]
    public Vector2 minPosition = Vector2.one;

    public float camWidth;
    public float camHeight;

    public float edgePadding = 0.3f;

    public List<TagPlayer> currentSymbiotes = new List<TagPlayer>();
    public float countForTime = 10f;
    public float timer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Camera cam = Camera.main;

        if (cam != null)
        {
            camHeight = cam.orthographicSize;
            camWidth = camHeight * cam.aspect;

            maxPosition.x = camWidth - edgePadding;
            maxPosition.y = camHeight - edgePadding;

            minPosition.x = -camWidth + edgePadding;
            minPosition.y = -camHeight + edgePadding;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
        {
            Vector3 rand = new Vector3(
                Random.Range(camWidth, -camWidth),
                Random.Range(camHeight, -camHeight));
            agents.Add(Instantiate(agentPrefab, rand, Quaternion.identity));
        }

        agents[3].GetComponent<TagPlayer>().Tag();
        agents[2].GetComponent<TagPlayer>().Tag();
    }

    private void Update()
    {
        if (currentSymbiotes.Count < 3)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                int randIndex = Random.Range(0, agents.Count);

                if (agents[randIndex].GetComponent<TagPlayer>().infectable)
                {
                    agents[randIndex].GetComponent<TagPlayer>().Tag();
                }

                timer = countForTime;
            }
        }
    }

    public TagPlayer GetClosestTagPlayer(TagPlayer sourcePlayer)
    {
        float minDistance = float.MaxValue;
        Agent closestPlayer = null;

        foreach (Agent other in agents)
        {
            float sqrDistance = Vector3.SqrMagnitude(sourcePlayer.physicsObj.position - other.physicsObj.position);

            if (sqrDistance < float.Epsilon ||
                other.GetComponent<TagPlayer>().CurrentState == TagState.Infected ||
                other.GetComponent<TagPlayer>().CurrentState == TagState.Transforming)
            {
                // this is the sourcePlayer
                continue;
            }

            if (sqrDistance < minDistance)
            {
                closestPlayer = other;
                minDistance = sqrDistance;
            }
        }

        return closestPlayer.GetComponent<TagPlayer>();
    }
}
