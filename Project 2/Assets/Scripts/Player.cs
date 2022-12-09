using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    public float radius = 0.25f;

    // Update is called once per frame
    private void Update()
    {
        if (AntidoteManager.Instance.antidote != null)
        {
            if (IsTouching(AntidoteManager.Instance.antidote))
            {
                Destroy(AntidoteManager.Instance.antidote);
                controller.hasAntidote = true;
            }
        }
    }

    private bool IsTouching(GameObject antidote)
    {
        float sqrDistance = Vector3.SqrMagnitude(transform.position - antidote.transform.position);

        float sqrRadii = Mathf.Pow(radius, 2) + Mathf.Pow(antidote.GetComponent<Antidote>().radius, 2);

        // Returns boolean
        return sqrDistance < sqrRadii;
    }
}
