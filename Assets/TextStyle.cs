using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TextStyle : ScriptableObject
{
    public TextStyle basedOn;
    public enum OverrideType
    {
        None,
        Replace,
        ScaleHeight,
        ScaleArea
    }

    [SerializeField] private OverrideType overrideSize;
    [SerializeField] private float size;

    void OnValidate()
    {
        UpdateStyle();
    }

    void UpdateStyle()
    {
        if (onUpdateStyle != null)
        {
            onUpdateStyle.Invoke();
        }
    }

    public void OverrideSize(float size, OverrideType type)
    {
        this.overrideSize = type;
        this.size = size;
        UpdateStyle();
    }

    public void SetFontSize(float size)
    {
        switch (Mathf.RoundToInt(size))
        {
            case 0:
                size = 32;
                break;
            case 1:
                size = 34;
                break;
            case 2:
                size = 38;
                break;
            case 3:
                size = 44;
                break;
            case 4:
                size = 52;
                break;
        }
        OverrideSize(size, OverrideType.Replace);
    }

    public float GetSize()
    {
        float baseSize = 36; // default value
        if (basedOn != null)
        {
            baseSize = basedOn.GetSize();
        }

        float size = baseSize;
        switch (overrideSize)
        {
            case OverrideType.None:
                size = baseSize;
                break;
            case OverrideType.Replace:
                size = this.size;
                break;
            case OverrideType.ScaleHeight:
                size = baseSize * this.size;
                break;
            case OverrideType.ScaleArea:
                size = Mathf.Sqrt(baseSize * baseSize * this.size);
                break;
        }

        return size;
    }

    public void Subscribe(OnUpdateStyle update)
    {
        if (basedOn != null)
        {
            basedOn.Subscribe(update);
        }
        onUpdateStyle += update;
    }

    public void Unsubscribe(OnUpdateStyle update)
    {
        if (basedOn != null)
        {
            basedOn.Unsubscribe(update);
        }
        onUpdateStyle -= update;
    }

    public delegate void OnUpdateStyle();

    event OnUpdateStyle onUpdateStyle;
}
