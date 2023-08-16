using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Action SwitchOn;
    public Action OnSmallDamage;
    public Action OnDamage;
    public Action OnCriticalDamage;
    public Action OnHeal;
    public Action OnDeath;

    [Header("HP Components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Dying death;
    [Header("HP Settings")]
    [SerializeField, Range(50, 250)] private float maxHealth;

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

        OnDeath += HealingMax;
        OnDeath += Clamping;
        OnDeath += UpdateBar;

        SwitchOn += SetActions;
        SwitchOn?.Invoke();
    }

    public void SetActions()
    {
        OnSmallDamage += SmallDamage;
        OnSmallDamage += UpdateBar;
        OnSmallDamage += Clamping;

        OnDamage += Damage;
        OnDamage += UpdateBar;
        OnDamage += Clamping;

        OnCriticalDamage += CriticalDamage;
        OnCriticalDamage += UpdateBar;
        OnCriticalDamage += Clamping;

        OnHeal += Healing;
        OnHeal += UpdateBar;
        OnHeal += Clamping;
    }

    public void SmallDamage()
    {
        _health -= Mathf.FloorToInt(maxHealth / 8);
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
        death.Death();
    }

    private void UpdateBar()
    {
        if (healthBar == null) return;
        healthBar.value = _health;
    }
}
