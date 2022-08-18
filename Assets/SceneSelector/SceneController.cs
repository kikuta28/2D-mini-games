using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadCarGameScene()
    {
        SceneManager.LoadScene("CarGameScene");
    }
    public void LoadSceneSelectorScene()
    {
        SceneManager.LoadScene("SceneSelector");

    }
}
