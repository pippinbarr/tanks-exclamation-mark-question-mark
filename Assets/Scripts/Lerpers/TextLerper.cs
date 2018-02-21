using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TextLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "alignment", 
            "TextAnchor.LowerCenter", 
            "TextAnchor.LowerLeft", 
            "TextAnchor.LowerRight", 
            "TextAnchor.MiddleCenter", 
            "TextAnchor.MiddleLeft", 
            "TextAnchor.MiddleRight", 
            "TextAnchor.UpperCenter",
            "TextAnchor.UpperLeft",
            "TextAnchor.UpperRight"}));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFont", "font" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "fontStyle",
            "FontStyle.Bold",
            "TextAnchor.BoldAndItalic",
            "TextAnchor.Italic",
            "TextAnchor.Normal"
        }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpInt", "fontSize", 1, 150 }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "lineSpacing", 0f, 10f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "alignByGeometry" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "horizonalOverflow",
            "HorizontalWrapMode.Overflow",
            "HorizontalWrapMode.Wrap"
        }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "verticalOverflow",
            "VerticalWrapMode.Overflow",
            "VerticalWrapMode.Truncate"
        }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "resizeTextForBestFit" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpInt", "resizeTextMinSize", 1, 150 }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpInt", "resizeTextMaxSize", 50, 150 }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpColor", "color" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpMaterial", "materials" }));


        m_Selection = GetComponent<Text>();
        Text t = (Text)m_Selection;
        if (t.text == "")
        {
            m_LerpComplete = true;
            return;
        }

        type = "Text";


        StartCoroutine(StartLerp());
    }
	
}
