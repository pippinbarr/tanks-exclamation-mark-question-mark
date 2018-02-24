using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ImageLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpSprite", "sprite" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpColor", "color" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpMaterial", "material", m_Materials }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "type", "Image.Type.Filled", "Image.Type.Sliced", "Image.Type.Simple", "Image.Type.Tiled" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "fillMethod", "Image.FillMethod.Horizontal", "Image.FillMethod.Vertical", "Image.FillMethod.Radial90", "Image.FillMethod.Radial180", "Image.FillMethod.Radial360" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "fillAmount", 0.25f, 0.5f, 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpInt", "fillOrigin", 0, 3, false }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "fillClockwise" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "fillCenter" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "preserveAspect" }));


        m_Selection = GetComponent<Image>();
        //Image i = (Image)m_Selection;
        //i.fillC

        type = "Image";

        StartCoroutine(StartLerp());
    }
	
}
