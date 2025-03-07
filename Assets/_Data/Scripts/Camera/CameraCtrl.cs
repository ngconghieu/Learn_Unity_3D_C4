using System;
using UnityEngine;

public class CameraCtrl : GameMonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField] protected float cameraPositionSmoothTime = 0.1f;
    [SerializeField] protected float cameraHeight = 3.0f;

    [Header("Camera Rotation")]
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float maxPitchAngle = 75.0f;
    [SerializeField] protected float minPitchAngle = -45.0f;
    private Vector3 _velocity;
    private Vector2 _controlRotation;

    public virtual void SetPosition(Vector3 HeroPosition)
    {
        Vector3 cameraPosition = new(HeroPosition.x, HeroPosition.y + cameraHeight, HeroPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocity, cameraPositionSmoothTime);
    }

    public virtual void HandleCameraRotation()
    {
        Vector2 camInput = InputManager.Instance.CameraInput;

        // Y Rotation (Yaw Rotation)
        _controlRotation.y += camInput.x; //if mouse right, then y rotation is right

        // X Rotation (Pitch Rotation)
        _controlRotation.x -= camInput.y; //if mouse up, then x rotation is down

        //SetControlRotation(_controlRotation);
        float xAxis = _controlRotation.x;
        xAxis = Mathf.Clamp(xAxis, minPitchAngle, maxPitchAngle);
        //Limit the x rotation, so the camera can't rotate up and down too much

        Quaternion targetRotation = Quaternion.Euler(xAxis, _controlRotation.y, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

}