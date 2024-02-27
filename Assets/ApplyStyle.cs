using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ApplyStyle : MonoBehaviour
{
    public TMP_Text text;
    public TextStyle style;

    private void Reset()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (style != null)
        {
            style.Subscribe(UpdateStyle);
        }
        UpdateStyle();
    }

    void OnDisable()
    {
        style.Unsubscribe(UpdateStyle);
    }

    void UpdateStyle()
    {
        text.fontSize = style.GetSize();
    }
}
