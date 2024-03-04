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

### Default Style Sheet

A `TMP_StyleSheet` defines a list of named styles that you can apply to TextMesh Pro text objects. Each text object can either choose a style from the default style sheet or you can create your own sheet and pick a style from that.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/94e7b952-c497-4163-add2-23c27b59032c)

You can edit each style of a style sheet in the inspector (except for the `Normal` style, the editor allows you to make changes but those changes are ignored.) Each style is a set of opening tags and closing tags using the TextMesh Pro markup system.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/b9d6b14a-0769-4cf3-a7c1-e59100fbd9ad)

The tags you can use are documented [here](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextSupportedTags.html).

When you specify the font size you can do it as a percentage, using em units, in pixels, or relative pixels. In all cases, the original font size is used as the base for the adjustment. In this way you can get the styles to adapt to size changes by using the `SetDefaultFontSize` script above.

Also, by default, each `TextMesh Pro` object will use the `Default Style Sheet` asset assigned in the `TMP_Settings` asset.
![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/9678e4c8-3c9a-4292-a0b0-fac0fc13f4de)

By making your own style sheet and assigning it here, you can control the style of every `TextMesh Pro` text object in your project.

But, each `TextMesh Pro` text object also has its own setting for the style sheet to use. By default, the entry will be blank when the `TextMesh Pro` text objects are created. You can see the setting by click on the `Extra Settings` button in the `Inspector` view for the `TextMesh Pro` text object:

![image](https://github.com/LSBUSGP/TextStyleSheets/assets/3679392/09afdd82-5fa5-4936-bb74-7b3c94cf130f)

If you assign a style sheet here then this will override the one used in `TMP_Settings`.

### Making changes at runtime

Unfortunately, neither the `TMP_Settings` asset, nor the `Style Sheet` assets can be modified at runtime. Only the settings on each `TextMesh Pro` text object have write access from the code. So if you want to allow your players to, for example, change the size of your text from a configuration menu in game, you will have to wrtie a script to do that.

A script similar to the `SetDefaultFontSize` script (above) can be used to apply such changes at runtime. However, we will first need our own `ScriptableObject` to store the current font size and style data. The following `StyleInfo` script can act as such a store:

```csharp
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class StyleInfo : ScriptableObject
{
    public float fontSize;
    public TMP_StyleSheet styleSheet;
}
```

After creating such a script, create an asset of the type `StyleInfo` and set the font size and style sheet to your own settings.

Then you can apply the font size and style sheet to each text object with this `SetStyle` script:

```csharp
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
```

Add this script to each `TextMesh Pro` text object and assign the `Style Info` property to point to your newly created asset. Now your text objects should reflect whatever you set in those variables in your style info, and these you **can** change at runtime.

Note: using `Update` to constantly re-apply text style and font changes every frame might have a performance impact if the number text objects gets large. Should that become a problem, it is possible to replace this with callbacks so that these variables are only updated when changes are made.
