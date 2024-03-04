using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class StyleInfo : ScriptableObject
{
    [SerializeField] private float fontSize;
    [SerializeField] private TMP_StyleSheet styleSheet;

    public delegate void StyleChanged(float fontSize, TMP_StyleSheet styleSheet);
    event StyleChanged onStyleChange;

    public void Subscribe(StyleChanged styleChangedFunction)
    {
        styleChangedFunction(fontSize, styleSheet);
        onStyleChange += styleChangedFunction;
    }

    public void Unsubscribe(StyleChanged styleChangedFunction)
    {
        onStyleChange -= styleChangedFunction;
    }

    void OnValidate()
    {
        UpdateSubscribers();
    }

    public void SetFontSize(float fontSize)
    {
        this.fontSize = fontSize;
        UpdateSubscribers();
    }

    void UpdateSubscribers()
    {
        if (onStyleChange != null)
        {
            onStyleChange.Invoke(fontSize, styleSheet);
        }
    }
}
