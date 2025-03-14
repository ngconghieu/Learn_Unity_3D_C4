using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnAbstract : GameMonoBehaviour
{
    private Button _btn;

    private void Start()
    {
        AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    private void LoadButton()
    {
        if (_btn != null) return;
        _btn = GetComponent<Button>();
    }

    private void AddOnClickEvent()
    {
        _btn.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
