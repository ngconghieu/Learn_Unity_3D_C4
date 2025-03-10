using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HeroCtrl : BaseCtrl
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected CameraManager cameraCtrl;
    public Animator Animator => animator;
    public CharacterController CharacterController => characterController;
    public CameraManager CameraCtrl => cameraCtrl;

    private void LateUpdate() => cameraCtrl.SetPosition(transform.position);

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadCharacterController();
        LoadCameraCtrl();
    }

    private void LoadCharacterController()
    {
        if (characterController != null) return;
        characterController = GetComponent<CharacterController>();
        characterController.height = 3.4f;
        characterController.center = new Vector3(0, 1.7f, 0);
        characterController.radius = 0.3f;
        Debug.LogWarning("LoadCharacterController", gameObject);
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
