using System;
using UnityEngine;

public class CameraManager : GameMonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField] protected float speedCamera = 0.05f;
    [SerializeField] protected float cameraHeight = 3.0f;

    [Header("Camera Rotation")]
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float maxPitchAngle = 75.0f;
    [SerializeField] protected float minPitchAngle = -45.0f;
    private Vector3 _velocity;
    private Vector2 _controlRotation;
    public Vector2 ControlRotation => _controlRotation;

    public virtual void SetPosition(Vector3 HeroPosition)
    {
        Vector3 cameraPosition = new(HeroPosition.x, HeroPosition.y + cameraHeight, HeroPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocity, speedCamera);
        HandleCameraRotation();
    }

    public virtual void HandleCameraRotation()
    {
        Vector2 camInput = InputManager.Instance.CameraInput;

        // Y Rotation (Yaw Rotation)
        _controlRotation.y += camInput.x; //if mouse right, then y rotation is right
        _controlRotation.y %= 360; //keep the value between 0 and 360

        // X Rotation (Pitch Rotation)
        _controlRotation.x = Mathf.Clamp(_controlRotation.x - camInput.y, minPitchAngle, maxPitchAngle); //Limit the x rotation

        Quaternion targetRotation = Quaternion.Euler(_controlRotation.x, _controlRotation.y, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

}