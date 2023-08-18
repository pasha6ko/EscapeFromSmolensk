using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;
    [HideInInspector] private int scoreNow;

    [Header("Player Components")]
    [SerializeField] public PlayerHP playerHP;
    [SerializeField] public Death death;
    [Header("Score Settings")]
    [SerializeField] private int startScore;
    [SerializeField] private TextMeshProUGUI textScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        scoreNow = startScore;
        ScoreUpdate();
    }

    public void Damage(int value)
    {
        if (playerHP.isArmored) return;
        if (death.isDead) return;
        scoreNow -= Mathf.FloorToInt(100 / value);

        ScoreUpdate();
    }

    public void ScoreUpdate()
    {
        scoreNow = Math.Clamp(scoreNow, 0, startScore);
        textScore.text = $"{scoreNow}";

        if (scoreNow > 0) return;
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
