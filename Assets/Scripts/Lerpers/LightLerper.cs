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
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "intensity", 1f, 2f, 0f, 14f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowStrength", 0.25f, 0.5f, 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowBias", 0.25f, 0.5f, 0f, 2f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowNormalBias", 0.25f, 0.5f, 0f, 3f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "shadowNearPlane", 1f, 3f, 0.01f, 10f }));

        // Specialised parameters depending on type of light
        Light selectionAsLight = (Light)m_Selection;

        if (selectionAsLight.type == LightType.Spot)
        {
            m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "spotAngle", 15f, 45f, 1f, 179f }));
        }
        if (selectionAsLight.type != LightType.Directional)
        {
            m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "range", 2f, 10f, 0f, 1000f }));
        }

        type = "Light";


        StartCoroutine(StartLerp());
    }
	
}
