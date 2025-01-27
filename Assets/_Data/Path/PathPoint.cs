using UnityEngine;

public class PathPoint : GameMonobehaviour
{
    [SerializeField] protected PathPoint nextPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNextPoint();
    }
    public virtual void LoadNextPoint()
    {
        int siblingIndex = transform.GetSiblingIndex();
        if (nextPoint != null) return;
        if (siblingIndex + 1 < transform.parent.childCount)
        {
            Transform nextSibling = transform.parent.GetChild(siblingIndex + 1);
            nextPoint = nextSibling.GetComponent<PathPoint>();
            Debug.LogWarning("LoadNextPoint", gameObject);
        }
    }
}
