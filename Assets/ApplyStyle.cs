using TMPro;
using UnityEngine;

public class ApplyStyle : MonoBehaviour
{
    public TMP_Text text;
    public TextStyle style;

    private void Reset()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        style.onUpdateStyle += UpdateStyle;
    }

    void UpdateStyle()
    {
        text.fontSize = style.GetSize();
    }
}
