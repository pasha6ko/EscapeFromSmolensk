using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthBar;

    public Action OnSmallDamage;
    public Action OnDamage;
    public Action OnCriticalDamage;
    public Action OnHeal;

    private float _health;

    private void Start()
    {
        _health = maxHealth;

        healthBar.minValue = 0;
        healthBar.maxValue = _health;

        UpdateBar();

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

    public void Clamping()
    {
        _health = Math.Clamp(_health, 0, maxHealth);
    }

    private void UpdateBar()
    {
        healthBar.value = _health;
    }
}
