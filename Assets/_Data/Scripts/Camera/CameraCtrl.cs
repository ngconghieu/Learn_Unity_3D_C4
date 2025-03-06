using System;
using UnityEngine;

public class CameraCtrl : GameMonoBehaviour
{
    [SerializeField] protected CameraImpact cameraImpact;
    [SerializeField] protected float cameraPositionSmoothTime = 0.1f;
    [SerializeField] protected float rotationSpeed = 10.0f;
    private Vector3 _velocity;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCameraImpact();
    }

    private void LoadCameraImpact()
    {
        if (cameraImpact != null) return;
        cameraImpact = GetComponentInChildren<CameraImpact>();
        Debug.LogWarning("LoadCameraImpact", gameObject);
    }
    #endregion

    public virtual void SetPosition(Vector3 HeroPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, HeroPosition, ref _velocity, cameraPositionSmoothTime);
    }

    public virtual void SetRotation(Vector2 controlRotation)
    {
        Quaternion rigTargetLocalRotation = Quaternion.Euler(controlRotation.y, 0.0f, 0.0f);

        // X Rotation (Pitch Rotation)
        Quaternion pivotTargetLocalRotation = Quaternion.Euler(0.0f, -controlRotation.x, 0.0f);

        if (rotationSpeed > 0.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rigTargetLocalRotation, rotationSpeed * Time.deltaTime);
            cameraImpact.transform.localRotation = Quaternion.Slerp(cameraImpact.transform.localRotation, pivotTargetLocalRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.localRotation = rigTargetLocalRotation;
            cameraImpact.transform.localRotation = pivotTargetLocalRotation;
        }
    }

}
