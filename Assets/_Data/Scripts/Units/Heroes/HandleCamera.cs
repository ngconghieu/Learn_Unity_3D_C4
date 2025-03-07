using System;
using UnityEngine;

public class HandleCamera : HeroAbstract
{
    private void LateUpdate()
    {
        SetupCamera();
    }

    private void SetupCamera()
    {
        HeroCtrl.CameraCtrl.SetPosition(transform.parent.position);
        HeroCtrl.CameraCtrl.HandleCameraRotation();
    }
}
