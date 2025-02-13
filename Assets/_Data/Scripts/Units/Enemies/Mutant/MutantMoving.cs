using UnityEngine;

public class MutantMoving : EnemyMoving
{
    protected override void ResetValue()
    {
        base.ResetValue();
        pathName = "Path_0";
    }
}
