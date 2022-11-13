using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    [SerializeField] AgentManager manager;

    private void Start()
    {
        manager = AgentManager.Instance;
    }

    protected override void CalcSteeringForces()
    {
        Flee(GetNearestEnemy());
    }

    private Vector3 GetNearestEnemy()
    {
        float minDist = float.MaxValue;
        int nearestIndex = -1;

        for (int i = 0; i < manager.agents.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, manager.agents[i].transform.position);

            if (dist < minDist)
            {
                nearestIndex = i;
                minDist = dist;
            }
        }
        Debug.Log(nearestIndex);

        if (nearestIndex != -1)
        {
            return manager.agents[nearestIndex].transform.position;
        }
        return Vector3.zero;
    }
}
