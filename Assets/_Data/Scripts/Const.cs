using UnityEngine;

public static class Const
{
    private static readonly int _isDead = Animator.StringToHash("isDead");
    private static readonly int _isHit = Animator.StringToHash("isHit");
    private static readonly int _isWalking = Animator.StringToHash("isWalking");
    private static readonly int _isRunning = Animator.StringToHash("isRunning");

    public static int IsDead => _isDead;
    public static int IsHit => _isHit;
    public static int IsWalking => _isWalking;
    public static int IsRunning => _isRunning;

}
