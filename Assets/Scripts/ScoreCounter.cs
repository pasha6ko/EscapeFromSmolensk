using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector] public int scoreNow;

    [Header("Player Components")]
    [SerializeField] private Death death;
    [Header("Score Settings")]
    [SerializeField] private int startScore;
    [SerializeField] private TextMeshProUGUI textScore;

    private void Start()
    {
        scoreNow = startScore;
        ScoreUpdate();
    }

    public void Revival(int score)
    {
        startScore = score;
        scoreNow = score;
        ScoreUpdate();
    }

    public void SmallDamage()
    {
        if (death.isDead) return;
        scoreNow -= 10;
    }

    public void MediumDamage()
    {
        if (death.isDead) return;
        scoreNow -= 40;
    }

    public void LargeDamage()
    {
        if (death.isDead) return;
        scoreNow -= 100;
    }

    public void DeathDamage()
    {
        if (death.isDead) return;
        scoreNow -= 60;
    }

    public void ScoreUpdate()
    {
        scoreNow = Math.Clamp(scoreNow, 0, startScore);
        textScore.text = $"{scoreNow}";
    }
}
