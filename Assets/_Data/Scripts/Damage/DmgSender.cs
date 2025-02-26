using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DmgSender : GameMonoBehaviour
{
    [SerializeField] private int dmg = 1;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody();
        LoadCollider();
    }

    private void LoadRigidbody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log("LoadRigidbody", gameObject);
    }

    private void LoadCollider()
    {
        if (col != null) return;
        col = GetComponent<Collider>();
        col.isTrigger = true;
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DmgReceiver>(out DmgReceiver dmgReceiver))
        {
            SendDmg(dmgReceiver);
        }
    }

    protected virtual void SendDmg(DmgReceiver dmgReceiver)
    {
        dmgReceiver.Deduct(dmg);
    }
}
