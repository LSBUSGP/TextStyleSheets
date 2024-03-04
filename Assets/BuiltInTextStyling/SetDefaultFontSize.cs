using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class SetDefaultFontSize : MonoBehaviour
{
    public TMP_Text text;

    void Reset()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.fontSize = TMP_Settings.defaultFontSize;
    }
}
