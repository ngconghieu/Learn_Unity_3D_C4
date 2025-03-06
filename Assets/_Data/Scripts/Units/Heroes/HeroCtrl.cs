using System;
using UnityEngine;

public class HeroCtrl : GameMonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected CameraCtrl cameraCtrl;
    public Animator Animator => animator;
    public Rigidbody Rigidbody => rb;
    public CameraCtrl CameraCtrl => cameraCtrl;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadRigibody();
        LoadCameraCtrl();
    }

    private void LoadCameraCtrl()
    {
        if(cameraCtrl != null) return;
        cameraCtrl = FindAnyObjectByType<CameraCtrl>();
        cameraCtrl.SetPosition(transform.position);
        Debug.LogWarning("LoadCameraCtrl", gameObject);
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
