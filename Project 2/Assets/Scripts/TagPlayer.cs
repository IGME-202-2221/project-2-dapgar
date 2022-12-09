using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagState
{
    Human,
    Infected,
    Transforming
}
public class TagPlayer : Agent
{
    TagState currentState = TagState.Human;
    public TagState CurrentState => currentState;

    [SerializeField] float countForTime = 10f;
    float timer = 0f;

    public float visionDistance = 4f;

    public float stayInBoundsWeight = 6f;

    public SpriteRenderer spriteRenderer;

    public Sprite infectedSprite;
    public Sprite humanSprite;
    public Sprite transformingSprite;

    public bool infectable = false;

    protected override void CalcSteeringForces()
    {
        switch (currentState)
        {
            case TagState.Human:
                {
                    infectable = true;

                    // Wander/Run away from It
                    List<TagPlayer> currentSymbiotes = AgentManager.Instance.currentSymbiotes;

                    foreach (TagPlayer infected in currentSymbiotes)
                    {
                        float distToItPlayer = Vector3.SqrMagnitude(physicsObj.position - infected.physicsObj.position);

                        if (distToItPlayer < Mathf.Pow(visionDistance, 2))
                        {
                            Flee(infected.physicsObj.position);
                        }
                        else
                        {
                            Wander();
                        }
                    }
                    Separate(AgentManager.Instance.agents);

                    break;
                }

            case TagState.Infected:
                {
                    infectable = false;

                    // Run towards nearest agent(not self)
                    // Get index to closest agent
                    TagPlayer targetPlayer = AgentManager.Instance.GetClosestTagPlayer(this);

                    if (IsTouching(targetPlayer))
                    {
                        // Tag other target
                        targetPlayer.Tag();

                        // Become not-it
                        // StateTransition(TagState.Human);
                    }
                    else
                    {
                        Seek(targetPlayer.physicsObj.position);
                    }
                    Separate(AgentManager.Instance.currentSymbiotes);

                    break;
                }

            case TagState.Transforming:
                {
                    infectable = false;

                    // Count down to zero
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        StateTransition(TagState.Infected);
                    }
                    break;
                }
        }
        
        StayInBounds(stayInBoundsWeight);
        AvoidAllObstacles();
        SpriteFlip();
    }

    public void StateTransition(TagState newState)
    {
        switch(newState)
        {
            case TagState.Human:
                {
                    infectable = true;

                    spriteRenderer.sprite = humanSprite;
                    AgentManager.Instance.currentSymbiotes.Remove(this);
                    break;
                }

            case TagState.Infected:
                {
                    infectable = false;

                    spriteRenderer.sprite = infectedSprite;
                    break;
                }

            case TagState.Transforming:
                {
                    infectable = false;

                    spriteRenderer.sprite = transformingSprite;

                    Vector3 temp = transform.position;
                    transform.position = temp;
                    
                    timer = countForTime;
                    AgentManager.Instance.currentSymbiotes.Add(this);
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
        StateTransition(TagState.Transforming);
    }

    public void Cure()
    {
        StateTransition(TagState.Human);
    }

    public void SpriteFlip()
    {
        if (physicsObj.velocity.x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (physicsObj.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }
}
