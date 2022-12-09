using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (AgentManager.Instance.currentSymbiotes.Count == AgentManager.Instance.agents.Count)
        {
            SceneManagment.Instance.ToEndScene();
        }   
    }
}
