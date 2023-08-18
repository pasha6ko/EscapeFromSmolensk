using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class PlayerHP : MonoBehaviour
{
    public OneValueOperations OnDamage;
    public OneValueOperations OnHeal;

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

        death.SwitchOn += SetActions;
        death.SwitchOn?.Invoke();
    }

    public void SetActions()
    {
        OnDamage += Damage;
        OnDamage += ScoreCounter.Instance.Damage;

        OnHeal += Healing;
    }

    public void Damage(int value)
    {
        if (isArmored) return;
        _health -= Mathf.FloorToInt(maxHealth / value);

        UpdateBar();
    }

    public void Healing(int value)
    {
        _health += Mathf.FloorToInt(maxHealth / value);

        UpdateBar();
    }

    public void Clamping()
    {
        _health = Math.Clamp(_health, 0, maxHealth);

        if (_health > 0) return;

        OnDamage = null;
        OnHeal = null;

        if (death == null) return;
        death.StartDeath();
    }

    private void UpdateBar()
    {
        Clamping();

        if (healthBar == null) return;
        healthBar.value = _health;
    }
}
public delegate void OneValueOperations(int value);
