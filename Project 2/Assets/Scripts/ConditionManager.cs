using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionManager : MonoBehaviour
{
    [SerializeField] SceneManagment sceneManager;

    [SerializeField] PlayerController playerController;
    [SerializeField] SpriteRenderer antidoteSR;

    // Update is called once per frame
    void Update()
    {
        if (AgentManager.Instance.currentSymbiotes.Count == AgentManager.Instance.agents.Count)
        {
            sceneManager.ToLoseScene();
        }

        if (!playerController.hasAntidote)
        {
            antidoteSR.enabled = false;
        }
        else
        {
            antidoteSR.enabled = true;
        }
    }
}
