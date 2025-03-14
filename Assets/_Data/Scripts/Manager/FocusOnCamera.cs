using System;
using UnityEngine;

public class FocusOnCamera : GameMonoBehaviour
{
    [SerializeField] private Camera _camera;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
    }

    private void LoadCamera()
    {
        if(_camera != null) return;
        _camera = FindAnyObjectByType<Camera>();
        Debug.LogWarning("LoadCamera", gameObject);
    }

    private void FixedUpdate()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        Vector3 dir = _camera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
