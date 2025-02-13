using UnityEngine;

public class VampireMoving : EnemyMoving
{
    protected override void ResetValue()
    {
        base.ResetValue();
        pathName = "Path_1";
    }
}
