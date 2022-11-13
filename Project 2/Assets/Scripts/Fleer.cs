using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    [SerializeField] AgentManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("AgentManager").GetComponent<AgentManager>();
    }

    protected override void CalcSteeringForces()
    {
        Flee(GetNearestEnemy());
    }

    private Vector3 GetNearestEnemy()
    {
        float minDist = float.MaxValue;
        int nearestIndex = -1;

        for (int i = 0; i < manager.Agents.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, manager.Agents[i].transform.position);

            if (dist < minDist)
            {
                nearestIndex = i;
                minDist = dist;
            }
        }
        Debug.Log(nearestIndex);

        if (nearestIndex != -1)
        {
            return manager.Agents[nearestIndex].transform.position;
        }
        return Vector3.zero;
    }
}
