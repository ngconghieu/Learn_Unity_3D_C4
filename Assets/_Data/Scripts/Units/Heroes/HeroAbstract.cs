using System;
using UnityEngine;

public class HeroAbstract : GameMonoBehaviour
{
    [SerializeField] private HeroCtrl heroCtrl;
    public HeroCtrl HeroCtrl => heroCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHeroCtrl();
    }

    private void LoadHeroCtrl()
    {
        if(heroCtrl != null) return;
        heroCtrl = GetComponentInParent<HeroCtrl>();
        Debug.LogWarning("LoadHeroCtrl", gameObject);
    }
}
