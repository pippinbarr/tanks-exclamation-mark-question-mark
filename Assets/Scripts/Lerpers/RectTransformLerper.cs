using System.Collections;
using UnityEngine;


public class RectTransformLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "left", -1000f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "right", -1000f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "top", -1000f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "bottom", -1000f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "localPosition", 5.0f, 10.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpRotation", "localRotation", -30f, 30f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "localScale", 0.5f, 1.0f }));

        m_Selection = GetComponent<RectTransform>();
        RectTransform t = (RectTransform)m_Selection;
        //t.

        type = "RectTransform";

        StartCoroutine(StartLerp());
    }
	
}
