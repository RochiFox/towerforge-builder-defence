using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamage;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamage?.Invoke(this, EventArgs.Empty);

        if (IsDead())
            OnDied?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead() => healthAmount == 0;

    public bool IsFullHealth() => healthAmount == healthAmountMax;

    public int GetHealthAmount() => healthAmount;

    public int GetHealthAmountMax() => healthAmountMax;

    public float GetHealthAmountNormalized() => (float)healthAmount / healthAmountMax;

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount)
            healthAmount = healthAmountMax;
    }
}
