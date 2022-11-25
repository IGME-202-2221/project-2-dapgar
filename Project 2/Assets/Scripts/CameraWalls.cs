using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalls : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject player;
    [SerializeField] Camera cameraObj;
    [SerializeField] public float height;
    [SerializeField] public float width;

    // Start is called before the first frame update
    void Start()
    {
        height = cameraObj.orthographicSize * 2f;
        width = height * cameraObj.aspect;
    }
}
