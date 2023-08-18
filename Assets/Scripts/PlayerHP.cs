using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class PlayerHP : MonoBehaviour
{
    public OneValueOperations OnSmallDamage;
    public Action OnDamage;
    public Action OnCriticalDamage;
    public Action OnHeal;

    [Header("Player Components")]
    [SerializeField] private ScoreCounter score;
    [Header("HP Components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Death death;
    [Header("HP Settings")]
    [SerializeField, Range(50, 250)] private float maxHealth;
    [SerializeField][HideInInspector] public bool isArmored = false;

    private float _health;

    private void Start()
    {
        _health = maxHealth;

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = _health;
        }
        UpdateBar();

        death.OnDeath += HealingMax;
        death.OnDeath += Clamping;
        death.OnDeath += UpdateBar;

        death.SwitchOn += SetActions;
        death.SwitchOn?.Invoke();
    }

    public void SetActions()
    {
        /*OnSmallDamage += SmallDamage;
        OnSmallDamage += UpdateBar;
        OnSmallDamage += Clamping;*/
        OnSmallDamage += score.Damage;
        //OnSmallDamage += score.ScoreUpdate;

        OnDamage += Damage;
        OnDamage += UpdateBar;
        OnDamage += Clamping;
        OnDamage += score.MediumDamage;
        OnDamage += score.ScoreUpdate;

        OnCriticalDamage += CriticalDamage;
        OnCriticalDamage += UpdateBar;
        OnCriticalDamage += Clamping;
        OnCriticalDamage += score.LargeDamage;
        OnCriticalDamage += score.ScoreUpdate;

        OnHeal += Healing;
        OnHeal += UpdateBar;
        OnHeal += Clamping;
    }

    public void SmallDamage()
    {
        if (isArmored) return;
        _health -= Mathf.FloorToInt(maxHealth / 7);
    }

    public void Damage()
    {
        _health -= Mathf.FloorToInt(maxHealth / 4);
    }

    public void CriticalDamage()
    {
        _health -= Mathf.FloorToInt(maxHealth / 2);
    }

    public void Healing()
    {
        _health += Mathf.FloorToInt(maxHealth / 2);
    }

    public void HealingMax()
    {
        _health += maxHealth;
    }

    public void Clamping()
    {
        _health = Math.Clamp(_health, 0, maxHealth);

        if (_health > 0) return;

        OnSmallDamage = null;
        OnDamage = null;
        OnCriticalDamage = null;
        OnHeal = null;

        if (death == null) return;
        death.StartDeath();
    }

    private void UpdateBar()
    {
        if (healthBar == null) return;
        healthBar.value = _health;
    }
}
public delegate void OneValueOperations(int x);
