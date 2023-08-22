using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    [Header("Player Components")]
    [SerializeField] public PlayerHP playerHP;
    [SerializeField] public Death death;

    [Header("Score Settings")]
    [SerializeField] private int startScore;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI endScore;

    private int _scoreNow;

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
        _scoreNow = startScore;
        ScoreUpdate();
    }

    public void Damage(int value)
    {
        if (playerHP.isArmored) return;
        if (death.isDead) return;
        _scoreNow -= Mathf.FloorToInt(100 / value);

        ScoreUpdate();
    }

    public void ScoreUpdate()
    {
        _scoreNow = Math.Clamp(_scoreNow, 0, startScore);
        textScore.text = $"{_scoreNow}";
        endScore.text = $"{_scoreNow}";

        if (_scoreNow > 0) return;
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
