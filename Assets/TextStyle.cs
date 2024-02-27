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
    public float size { get { return _size; } set { _size = value; onUpdateStyle.Invoke(); } }
    [SerializeField] float _size;

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

    public delegate void OnUpdateStyle();

    public event OnUpdateStyle onUpdateStyle;
}
