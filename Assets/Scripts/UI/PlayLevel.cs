using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
