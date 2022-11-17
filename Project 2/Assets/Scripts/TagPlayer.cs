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
    public TagState CurrentState => currentState;

    [SerializeField] float countForTime = 10f;
    float timer = 0f;

    public float visionDistance = 4f;

    public SpriteRenderer spriteRenderer;

    public Sprite itSprite;
    public Sprite notitSprite;
    public Sprite countingSprite;

    protected override void CalcSteeringForces()
    {
        switch (currentState)
        {
            case TagState.NotIt:
                {
                    // Wander/Run away from It
                    TagPlayer currentIt = AgentManager.Instance.currentItPlayer;

                    float distToItPlayer = Vector3.SqrMagnitude(physicsObj.position - currentIt.physicsObj.position);

                    if (distToItPlayer < Mathf.Pow(visionDistance, 2))
                    {
                        Flee(currentIt.physicsObj.position);
                    }
                    else
                    {
                        Wander();
                    }

                    Separate(AgentManager.Instance.agents);

                    break;
                }

            case TagState.It:
                {
                    // Run towards nearest agent(not self)
                    // Get index to closest agent
                    TagPlayer targetPlayer = AgentManager.Instance.GetClosestTagPlayer(this);

                    if (IsTouching(targetPlayer))
                    {
                        // Tag other target
                        targetPlayer.Tag();

                        // Become not-it
                        StateTransition(TagState.NotIt);
                    }
                    else
                    {
                        Seek(targetPlayer.physicsObj.position);
                    }

                    break;
                }

            case TagState.Counting:
                {
                    // Count down to zero
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        StateTransition(TagState.It);
                    }
                    break;
                }
        }
        
        StayInBounds(4f);
    }

    public void StateTransition(TagState newState)
    {
        switch(newState)
        {
            case TagState.NotIt:
                {
                    spriteRenderer.sprite = notitSprite;
                    break;
                }

            case TagState.It:
                {
                    spriteRenderer.sprite = itSprite;
                    break;
                }

            case TagState.Counting:
                {
                    spriteRenderer.sprite = countingSprite;

                    Vector3 temp = transform.position;
                    transform.position = temp;
                    
                    timer = countForTime;
                    AgentManager.Instance.currentItPlayer = this;
                    break;
                }
        }

        // spriteRenderer.sprite = manager.sprites[(int)newState];
        currentState = newState;
    }

    private bool IsTouching(TagPlayer otherPlayer)
    {
        float sqrDistance = Vector3.SqrMagnitude(physicsObj.position - otherPlayer.physicsObj.position);

        float sqrRadii = Mathf.Pow(physicsObj.radius, 2) + Mathf.Pow(otherPlayer.physicsObj.radius, 2);

        // Returns boolean
        return sqrDistance < sqrRadii;
    }

    public void Tag()
    {
        StateTransition(TagState.Counting);
    }
}
