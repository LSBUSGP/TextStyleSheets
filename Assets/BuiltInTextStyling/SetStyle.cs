using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class SetStyle : MonoBehaviour
{
    public StyleInfo styleInfo;
    public TMP_Text text;

    void Reset()
    {
        text = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        if (styleInfo != null)
        {
            styleInfo.Subscribe(ApplyStyle);
        }
    }

    void OnDisable()
    {
        if (styleInfo != null)
        {
            styleInfo.Unsubscribe(ApplyStyle);
        }
    }

    void ApplyStyle(float fontSize, TMP_StyleSheet styleSheet)
    {
        if (styleInfo != null)
        {
            text.fontSize = fontSize;
            text.styleSheet = styleSheet;
        }
    }
}
