using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public static SceneManagment Instance;


    // Loads Menu Scene
    public void ToMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    // Loads Game
    public void ToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    // Loads End Scene
    public void ToEndScene()
    {
        SceneManager.LoadScene(2);
    }
}
