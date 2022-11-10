using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] Agent agentPrefab;

    List<Sprite> sprites = new List<Sprite> ();

    [SerializeField] int agentSpawnCount;

    List<Agent> agents = new List<Agent>();

    public List<Agent> Agents
    {
        get { return agents; }
    }

    int currentIndex;

    public int ItAgentIndex
    {
        get { return currentIndex; }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
        {
            agents.Add(Instantiate(agentPrefab));

            //agents[i].Init(this);
        }

        TagPlayer(0);
    }

    public void TagPlayer(int newItIndex)
    {
        ((TagPlayer)agents[newItIndex]).ChangeStateTo(TagState.Counting);
        currentIndex = newItIndex;
    }
}