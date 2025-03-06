using UnityEngine;

public class HandleCamera : HeroAbstract
{
    private void LateUpdate()
    {
        HeroCtrl.CameraCtrl.SetPosition(transform.parent.position);
        HeroCtrl.CameraCtrl.SetRotation(InputManager.Instance.CameraInput);
    }
}
