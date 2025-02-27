using System;
using UnityEngine;

public abstract class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] private int currentHP = 0;
    [SerializeField] private int maxHP = 10;

    protected override void ResetValue()
    {
        base.ResetValue();
        SetCurrentHP();
    }

    private void OnEnable()
    {
        SetCurrentHP();
    }

    protected virtual void SetCurrentHP()
    {
        currentHP = maxHP;
    }

    protected virtual void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
    }

    public virtual void Deduct(int dmg)
    {
        currentHP -= dmg;
        if (CheckDead())
            IsDead();
        else
            IsHit();
    }

    public virtual bool CheckDead() => currentHP <= 0;

    protected abstract void IsDead();
    protected abstract void IsHit();
}
