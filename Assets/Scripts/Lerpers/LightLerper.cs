using System.Collections;
using UnityEngine;


public class LightLerper : ParameterLerper {


	// Use this for initialization
	protected override void Start () {
        base.Start();

        // Add standard parameters
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "shadows", "LightShadows.None", "LightShadows.Soft", "LightShadows.Hard" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "renderMode", "LightRenderMode.Auto", "LightRenderMode.ForcePixel", "LightRenderMode.ForceVertex" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpColor", "color" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "intensity", 0f, 2f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowStrength", 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowBias", 0f, 2f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowNormalBias", 0f, 3f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowNearPlane", 0f, 10f }));

        // Specialised parameters depending on type of light
        Light selectionAsLight = (Light)m_Selection;
        if (selectionAsLight.type != LightType.Point)
        {
            m_Parameters.Add(new ArrayList(new object[] { "LerpRotation", "rotation" }));
        }
        if (selectionAsLight.type == LightType.Spot)
        {
            m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "range", 0f, 1000f }));
            m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "spotAngle", 0f, 179f }));
        }
        if (selectionAsLight.type != LightType.Directional)
        {
            m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "range", 0f, 1000f }));
        }

        type = "LightLerper";


        StartCoroutine(StartLerp());
    }
	
}
