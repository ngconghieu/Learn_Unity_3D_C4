using UnityEngine;

public class BulletCtrl : GameMonoBehaviour
{
    [SerializeField] protected float speed = 20f;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}