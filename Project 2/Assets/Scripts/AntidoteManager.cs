using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteManager : MonoBehaviour
{
    public static AntidoteManager Instance;

    public GameObject antidotePF;
    public GameObject antidote = null;

    public float countForTime = 5f;
    public float timer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        timer = countForTime;
    }

    private void Update()
    {
        if (antidote == null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Vector3 rand = new Vector3(
                Random.Range(AgentManager.Instance.camWidth, -AgentManager.Instance.camWidth),
                Random.Range(AgentManager.Instance.camHeight, -AgentManager.Instance.camHeight));

                antidote = Instantiate(antidotePF, rand, Quaternion.identity);
                timer = countForTime;
            }
        }
    }
}
