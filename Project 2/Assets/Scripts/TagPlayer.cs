using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagState
{
    NotIt,
    It,
    Counting
}
public class TagPlayer : Agent
{
    TagState currentState = TagState.NotIt;

    [SerializeField] float countForTime = 10f;
    float timer;

    [SerializeField] SpriteRenderer spriteRenderer;

    protected override void CalcSteeringForces()
    {
        /*
        switch (currentState)
        {
            case TagState.NotIt:
                // Run away from It
                // totalSteeringForce += Flee(manager.Agents[manager.ItAgentIndex].transform.position);
                break;

            case TagState.It:
                // Run towards nearest agent(not self)
                // Get index to closest agent

                // If close enough
                if (false)
                {
                    //manager.TagPlayer(index);
                    ChangeStateTo(TagState.NotIt);
                }
                break;

            case TagState.Counting:
                // Count down to zero
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    ChangeStateTo(TagState.It);
                }
                break;
        }
        */
        Wander();
        StayInBounds(3f);
        Separate(AgentManager.Instance.agents);

    }

    public void ChangeStateTo(TagState newState)
    {
        switch(newState)
        {
            case TagState.NotIt:
                break;

            case TagState.It:
                break;

            case TagState.Counting:
                timer = countForTime;
                break;
        }

        // spriteRenderer.sprite = manager.sprites[(int)newState];
        currentState = newState;
    }
}
