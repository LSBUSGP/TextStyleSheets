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

    void Update()
    {
        ApplyStyle();
    }

    void ApplyStyle()
    {
        if (styleInfo != null)
        {
            text.fontSize = styleInfo.fontSize;
            text.styleSheet = styleInfo.styleSheet;
        }
    }
}
