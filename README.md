# Globally controlling text in Unity

This example project contains two ways to globally control text in Unity. Both methods use Text Mesh Pro to display text.

## Method 1: Using a Text Mesh Pro Style Sheet

When you first add Text Mesh Pro to your project it will create default settings and a default style sheet.

The default settings are stored in a file called `TMP_Settings.asset` inside a folder called `Resources` in the `TextMesh Pro` folder.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/0240eea0-13fe-46bb-a079-3263455312c3)

The default style sheet is stored in a file called `Default Style Sheet.asset` inside a sub folder called `Style Sheets`.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/d0335cb7-62d8-4ea3-b793-c70e7f36ee52)


### Default Font Size

The `Default Font Size` entry in the TMP Settings sets the font size on a TextMesh Pro object when it is first created.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/42de92c2-6d17-4e1d-beaf-de334814deb6)

However, if you change the default font size in the TMP Settings after you have already created a TextMesh Pro object, the font size on the object will not change. This is because the font size on the object is set to a specific value and is not linked to the default font size in the TMP Settings.

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

A `TMP_StyleSheet` defines a list of named styles that you can apply to TextMesh Pro text objects. Each text object can either choose a style from the default style sheet or you can create your own sheet and pick a style from that.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/94e7b952-c497-4163-add2-23c27b59032c)

You can edit each style of a style sheet in the inspector (except for the `Normal` style, the editor allows you to make changes but those changes are ignored.) Each style is a set of opening tags and closing tags using the TextMesh Pro markup system.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/b9d6b14a-0769-4cf3-a7c1-e59100fbd9ad)

The tags you can use are documented [here](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextSupportedTags.html).
