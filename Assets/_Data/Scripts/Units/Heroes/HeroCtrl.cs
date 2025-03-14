using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HeroCtrl : BaseCtrl
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected CameraManager cameraCtrl;
    public HeroMovementSettings Movement;
    public Animator Animator => animator;
    public CapsuleCollider CapsuleCollider => capsuleCollider;
    public Rigidbody Rigidbody => rb;
    public CameraManager CameraCtrl => cameraCtrl;

    private void LateUpdate() => cameraCtrl.SetPosition(transform.position);

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadCollider();
        LoadRigibody();
        LoadCameraCtrl();
    }

    private void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.LogWarning("LoadRigibody", gameObject);
    }

    private void LoadCollider()
    {
        if (capsuleCollider != null) return;
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.height = 3.4f;
        capsuleCollider.center = new Vector3(0, 1.7f, 0);
        capsuleCollider.radius = 0.3f;
        Debug.LogWarning("LoadCollider", gameObject);
    }

    private void LoadCameraCtrl()
    {
        if(cameraCtrl != null) return;
        cameraCtrl = FindAnyObjectByType<CameraManager>();
        cameraCtrl.SetPosition(transform.position);
        Debug.LogWarning("LoadCameraCtrl", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning("LoadAnimator", gameObject);
    }
    #endregion

}

[Serializable]
public class HeroMovementSettings
{
    public float walkingSpeed = 5f;
    public float runningSpeedMultiplier = 1.8f;
    public float rotateSpeed = 8;
    public bool isWalking = false;
    public bool isRunning = false;
}