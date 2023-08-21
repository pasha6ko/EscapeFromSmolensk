using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoose : MonoBehaviour
{
    [Header("Level Components")]
    [SerializeField] private RotateCamera rotateCamera;
    [Header("Level Settings")]
    [SerializeField] private int sides;

    public void ChooseLevel()
    {
        int index = FindLevel(rotateCamera.side);

        if (index == 3) return;
        SceneManager.LoadScene($"Level{index+1}");
    }

    private int FindLevel(int side) => (side % sides + sides) % sides;
}
