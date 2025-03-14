using System;

public abstract class SliderHP : SliderAbstract
{
    private void FixedUpdate()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        Slider.value = GetValue();
    }

    public abstract float GetValue();
}