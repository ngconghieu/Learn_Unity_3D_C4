using System;
using UnityEngine;

public class ItemCtrl : GameMonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float force = 7f;
    [SerializeField] protected float dropPos = 2f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody();
    }

    private void LoadRigidbody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody>();
        Debug.LogWarning("LoadRigidbody", gameObject);
    }

    private void OnEnable()
    {
        OnDropItem();
    }

    private void OnDropItem()
    {
        float randomPos = UnityEngine.Random.Range(dropPos, -dropPos);
        Vector3 newPos = new(randomPos, force, randomPos);
        rb.AddForce(newPos, ForceMode.Impulse);
    }
}
