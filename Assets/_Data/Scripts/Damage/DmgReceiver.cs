using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DmgReceiver : GameMonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private int currentHP = 10;
    [SerializeField] private int maxHP = 10;
    [SerializeField] private bool isDead = false;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if(col != null) return;
        col = GetComponent<Collider>();
        col.isTrigger = true;
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

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
        CheckDead();
    }

    protected virtual void CheckDead()
    {
        if(currentHP < 0)
        {
            currentHP = 0;
            isDead = true;
        }
    }
}
