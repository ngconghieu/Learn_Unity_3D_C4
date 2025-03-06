using UnityEngine;

public class HandleCamera : HeroAbstract
{
    private void FixedUpdate()
    {
        HeroCtrl.CameraCtrl.SetPosition(transform.parent.position);
    }
}
