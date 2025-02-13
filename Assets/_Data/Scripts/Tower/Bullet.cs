using UnityEngine;

public class Bullet : GameMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 8f;

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }
}
