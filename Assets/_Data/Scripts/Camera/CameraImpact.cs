using System;
using UnityEngine;
public class CameraImpact : GameMonoBehaviour
{
    [SerializeField] private float targetLength = 6;
    [SerializeField] private float maxTargetLength = 10;
    [SerializeField] private float minTargetLength = 1;
    [SerializeField] private float speedDamp = 0.3f;
    [SerializeField] private Transform collisionSocket;
    [SerializeField] private float collisionRadius = 0.25f;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask = 0;
    private Vector3 _socketVelocity;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
        LoadCollisionSocket();
    }

    private void LoadCollisionSocket()
    {
        if (collisionSocket != null) return;
        collisionSocket = cam.transform.parent.GetComponent<Transform>();
        Debug.LogWarning("LoadCollisionSocket", gameObject);
    }

    private void LoadCamera()
    {
        if (cam != null) return;
        cam = GetComponentInChildren<Camera>();
        Debug.LogWarning("LoadCamera", gameObject);
    }
    #endregion

    private void LateUpdate()
    {
        SetCamPosition();
        CheckTargetLengthChange();
        UpdateLength();
    }

    private void SetCamPosition()
    {
        collisionRadius = GetCollisionRadius();
        cam.transform.localPosition = -Vector3.forward * cam.nearClipPlane; //set camera behind player at near clip plane
    }

    private void CheckTargetLengthChange()
    {
        float scrollInput = InputManager.Instance.ScrollInput;
        if (scrollInput == 0) return;
        targetLength -= scrollInput;
        targetLength = Mathf.Clamp(targetLength, minTargetLength, maxTargetLength);
    }

    private float GetCollisionRadius()
    {
        float halfFOV = cam.fieldOfView / 2 * Mathf.Deg2Rad;
        float heightOfNCP = Mathf.Tan(halfFOV) * cam.nearClipPlane;
        float widthOfNCP = heightOfNCP * cam.aspect;
        return new Vector3(widthOfNCP, heightOfNCP).magnitude;
    }

    private float GetDesiredTargetLength()
    {
        Ray ray = new(transform.position, -transform.forward);
        if (Physics.SphereCast(ray, collisionRadius, out RaycastHit hit, targetLength, layerMask))
        {
            return hit.distance;
        }
        else
        {
            return targetLength;
        }
    }
    /*
     Shoot a ray from the player. If it hits something, return the distance to the hit point
    , otherwise return the target length (distance to player)
     */
    private void UpdateLength()
    {
        float targetLength = GetDesiredTargetLength();
        Vector3 newSocketLocalPosition = -Vector3.forward * targetLength;

        collisionSocket.localPosition = Vector3.SmoothDamp(
            collisionSocket.localPosition,
            newSocketLocalPosition,
            ref _socketVelocity,
            speedDamp);
    }

    #region drawing gizmos
    private void OnDrawGizmos()
    {
        if (collisionSocket != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, collisionSocket.transform.position);
            DrawGizmoSphere(collisionSocket.transform.position, collisionRadius);
        }
    }

    private void DrawGizmoSphere(Vector3 pos, float radius)
    {
        Quaternion rot = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

        int alphaSteps = 8;
        int betaSteps = 16;

        float deltaAlpha = Mathf.PI / alphaSteps;
        float deltaBeta = 2.0f * Mathf.PI / betaSteps;

        for (int a = 0; a < alphaSteps; a++)
        {
            for (int b = 0; b < betaSteps; b++)
            {
                float alpha = a * deltaAlpha;
                float beta = b * deltaBeta;

                Vector3 p1 = pos + rot * GetSphericalPoint(alpha, beta, radius);
                Vector3 p2 = pos + rot * GetSphericalPoint(alpha + deltaAlpha, beta, radius);
                Vector3 p3 = pos + rot * GetSphericalPoint(alpha + deltaAlpha, beta - deltaBeta, radius);

                Gizmos.DrawLine(p1, p2);
                Gizmos.DrawLine(p2, p3);
            }
        }
    }

    private Vector3 GetSphericalPoint(float alpha, float beta, float radius)
    {
        Vector3 point;
        point.x = radius * Mathf.Sin(alpha) * Mathf.Cos(beta);
        point.y = radius * Mathf.Sin(alpha) * Mathf.Sin(beta);
        point.z = radius * Mathf.Cos(alpha);

        return point;
    }
    #endregion
}
