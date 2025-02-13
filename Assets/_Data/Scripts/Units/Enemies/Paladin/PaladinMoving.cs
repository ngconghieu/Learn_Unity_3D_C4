using UnityEngine;

public class PaladinMoving : EnemyMoving
{
    protected override void ResetValue()
    {
        base.ResetValue();
        pathName = "Path_0";
    }
}
