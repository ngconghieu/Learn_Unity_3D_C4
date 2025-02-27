using System;
using UnityEngine;

public abstract class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] private int currentHP = 10;
    [SerializeField] private int maxHP = 10;

    protected override void ResetValue()
    {
        base.ResetValue();
        currentHP = maxHP;
    }

    protected virtual void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
    }

    public virtual void Deduct(int dmg)
    {
        currentHP -= dmg;
        IsHit();
        CheckDead();
    }

    protected virtual void CheckDead()
    {
        if(currentHP <= 0)
        {
            currentHP = 0;
            IsDead();
        }
    }

    protected abstract void IsDead();
    protected abstract void IsHit();
}
