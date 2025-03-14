using UnityEngine;
using UnityEngine.UI;

public abstract class SliderAbstract : GameMonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Slider Slider => _slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSlider();
    }

    private void LoadSlider()
    {
        if (_slider != null) return;
        _slider = GetComponentInChildren<Slider>();
        Debug.LogWarning("LoadSlider", gameObject);
    }
}
