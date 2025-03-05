using System;
using UnityEngine;

public class HeroCtrl : GameMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected Rigidbody rb;
    public Rigidbody Rigidbody => rb;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadRigibody();
    }

    private void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody>();
        Debug.LogWarning("LoadRigibody", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning("LoadAnimator", gameObject);
    }
    #endregion
}
