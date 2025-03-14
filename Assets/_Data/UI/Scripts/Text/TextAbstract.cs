using UnityEngine;

public class TextAbstract : GameMonoBehaviour
{
    [SerializeField] protected TMPro.TextMeshProUGUI text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }

    private void LoadText()
    {
        if (text != null) return;
        text = GetComponent<TMPro.TextMeshProUGUI>();
        Debug.LogWarning("LoadText", gameObject);
    }

}
