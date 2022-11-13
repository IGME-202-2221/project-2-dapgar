using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;

    [SerializeField] Agent agentPrefab;
    [SerializeField] TagPlayer tagPlayerPrefab;

    [SerializeField] int agentSpawnCount;

    [HideInInspector]
    public List<Agent> agents = new List<Agent>();

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;

    [HideInInspector]
    public Vector2 minPosition = Vector2.one;

    float edgePadding = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Camera cam = Camera.main;

        if (cam != null)
        {
            Vector3 camPos = cam.transform.position;
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            maxPosition.x = camPos.x + halfWidth * 2 - edgePadding;
            maxPosition.y = camPos.y + halfHeight * 2 - edgePadding;

            minPosition.x = camPos.x - halfWidth * 2 + edgePadding;
            minPosition.y = camPos.y - halfHeight * 2 + edgePadding;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
        {
            Vector3 rand = new Vector3(
                Random.Range(minPosition.x + 1, maxPosition.x - 1),
                Random.Range(minPosition.y + 1, maxPosition.y - 1));
            agents.Add(Instantiate(agentPrefab));//, rand, Quaternion.identity));
        }
    }

    /*public void TagPlayer(int newItIndex)
    {
        ((TagPlayer)agents[newItIndex]).ChangeStateTo(TagState.Counting);
        currentIndex = newItIndex;
    }
    */
}
