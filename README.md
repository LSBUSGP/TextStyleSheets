# Globally controlling text in Unity

This example project contains two ways to globally control text in Unity. Both methods use Text Mesh Pro to display text.

## Method 1: Using a Text Mesh Pro Style Sheet

When you first add Text Mesh Pro to your project it will create default settings and a default style sheet. The default settings are stored in a file called `TMP_Settings.asset` inside a folder called `Resources` in the `TextMesh Pro` folder. The default style sheet is stored in a file called `Default Style Sheet.asset` inside a sub folder called `Style Sheets`.

### Default Font Size

The `Default Font Size` entry in the TMP Settings sets the font size on a Text Mesh Pro object when it is first created. However, if you change the default font size in the TMP Settings after you have already created a Text Mesh Pro object, the font size on the object will not change. This is because the font size on the object is set to a specific value and is not linked to the default font size in the TMP Settings.

If you create a new script `SetDefaultFontSize.cs`:

```csharp
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
```

and attach it to a Text Mesh Pro object, the font size on the object will be linked to the default font size in the TMP Settings. This means that if you change the default font size in the TMP Settings, the font size on the object will change. You could also copy other values from the TMP Settings to the object in the same way.

Note: there is a performance cost associated with using the `Update` function for this, as each instance of this script will update each frame. This is not a problem if you have only a few Text Mesh Pro objects. But, if you have many, this cost will increase linearly with the number of instances. If you don't need to change the font size at runtime, you can put it in the `Start` function instead. This way your cost will only happen when the Text Mesh Pro objects are enabled for the first time in each scene.

### Default Style Sheet

