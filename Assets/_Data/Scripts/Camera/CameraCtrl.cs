using System;
using UnityEngine;

public class CameraCtrl : GameMonoBehaviour
{
    [SerializeField] protected CameraImpact cameraImpact;
    [SerializeField] protected float cameraPositionSmoothTime = 0.1f;
    private Vector3 _velocity;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCameraImpact();
    }

    private void LoadCameraImpact()
    {
        if (cameraImpact != null) return;
        cameraImpact = GetComponentInChildren<CameraImpact>();
        cameraImpact.transform.localPosition = new Vector3(0, 1, -cameraImpact.TargetLength);
        Debug.LogWarning("LoadCameraImpact",gameObject);
    }

    public virtual void SetPosition(Vector3 HeroPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, HeroPosition, ref _velocity, cameraPositionSmoothTime);
    }

    public virtual void SetRotation(Vector2 controlRotation)
    {
        transform.rotation = Quaternion.Euler(controlRotation.x, controlRotation.y, 0);
    }

}
