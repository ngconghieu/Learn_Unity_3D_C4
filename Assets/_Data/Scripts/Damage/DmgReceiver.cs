using System;
using UnityEngine;

public abstract class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] private int _currentHP = 0;
    [SerializeField] private int _maxHP = 10;
    [SerializeField] private bool _immortal = false;

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
        _currentHP = _maxHP;
    }

    protected virtual void SetMaxHP(int maxHP)
    {
        this._maxHP = maxHP;
    }

    public virtual void Deduct(int dmg)
    {
        if (_immortal) return;
        _currentHP -= dmg;
        if (CheckDead())
            IsDead();
        else
            IsHit();
    }

    public virtual bool CheckDead() => _currentHP <= 0;

    protected abstract void IsDead();
    protected abstract void IsHit();
}
