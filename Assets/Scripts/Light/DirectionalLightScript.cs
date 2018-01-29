using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class DirectionalLightScript : MonoBehaviour {

    public Text parameterText;
    public float m_Interval = 2f;
    public float m_LerpSpeed = 2f;
    public Texture[] allTextures;

    private Light selection;


    private Quaternion m_OriginalRotation;
    private Quaternion m_TargetRotation;
    private Color m_TargetColor;
    private float m_TargetIntensity;
    private float m_TargetShadowStrength;
    private float m_TargetShadowBias;
    private float m_TargetShadowNormalBias;
    private float m_TargetShadowNearPlane;

    private float elapsed = 0f;

    private ArrayList[] parameters = {
        new ArrayList(new object[]{ "LerpFloat", "intensity", 0f, 2f }),
        new ArrayList(new object[]{ "LerpFloat", "shadowStrength", 0f, 1f }),
        new ArrayList(new object[]{ "LerpFloat", "shadowBias", 0f, 2f }),
        new ArrayList(new object[]{ "LerpFloat", "shadowNormalBias", 0f, 3f }),
        new ArrayList(new object[]{ "LerpFloat", "shadowNearPlane", 0f, 10f }),
    };
    private bool lerpComplete = true;



	// Use this for initialization
	void Start () {
        
        selection = GetComponent<Light>();

        TestFloatLerps();

        //TestReflectionFunctions();

        //StartCoroutine(RandomizeInterval()); 

        //StartCoroutine(RandomizeLerp());
    }
	
	// Update is called once per frame
    void Update () {
    }

    void TestFloatLerps()
    {
        StartCoroutine(StartLerps());
    }

    IEnumerator StartLerps()
    {
        while (true)
        {
            if (lerpComplete)
            {
                lerpComplete = false;
                ArrayList parameterToLerp = parameters[Mathf.FloorToInt(Random.value * parameters.Length)];
                StartCoroutine((string)parameterToLerp[0], parameterToLerp);
            }
            yield return null;
        }
    }

    IEnumerator LerpFloat(ArrayList floatParameterInfo)
    {
        // Set elapsed time to 0
        float floatElapsed = 0f;

        // Choose a duration for this lerp to take place
        float duration = 0.5f + Random.value * 1f;
        string parameterName = (string)floatParameterInfo[1];

        // Set the target float value based on parameters
        float targetFloat = (float)floatParameterInfo[2] + Random.value * ((float)floatParameterInfo[3] - (float)floatParameterInfo[2]);

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        float startFloat = (float) getter.Invoke(selection, null);

        Debug.Log("Start: " + targetFloat);

        parameterText.text = selection.GetType() + " " + selection.name + " " + selection.GetInstanceID() + "\n" + parameterName + ": " + startFloat;

        // Loop while the time has not yet elapsed
        while (floatElapsed < duration)
        {
            // Track how much time passed in this loop
            floatElapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = floatElapsed / duration;

            // Calculate the new value of the float required through interpolation
            float newFloat = Mathf.Lerp(startFloat, targetFloat, t);

            // Set the new value of the float parameter

            setter.Invoke(selection, new object[] { newFloat });

            parameterText.text = selection.GetType() + " " + selection.name + " " + selection.GetInstanceID() + "\n" + parameterName + ": " + newFloat;

            Debug.Log("elapsed: " + floatElapsed + ", t: " + t + ", new: " + newFloat);

            // Yield until the next frame
            yield return null;
        }

        lerpComplete = true;
    }


    void TestReflectionFunctions()
    {
        // This gets the color property out as a color
        Color c = (Color)selection.GetType().GetProperty("color").GetValue(selection, null);
        Debug.Log(c);

        // This pulls the setter method information
        MethodInfo setter = selection.GetType().GetProperty("color").GetSetMethod();

        MethodInfo getter = selection.GetType().GetProperty("color").GetGetMethod();

        // This involves the set method
        setter.Invoke(selection, new object[] { new Color(1000, 0, 0) });

        Color got = (Color)getter.Invoke(selection, null);
        Debug.Log(got);

        // This works to set the color property via creating a new Color
        //selection.GetType().GetProperty("color").SetValue(selection, new Color(0.5f,0f,0f), null);

        // This will print out a pretty specific identification of the particular instance
        Debug.Log(selection.GetType() + ", name: " + selection.name + ", id: " + selection.GetInstanceID());
    }

    IEnumerator RandomizeLerp()
    {
        SetTargets();

        while (true)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / m_Interval;
            Debug.Log(t);

            transform.rotation = Quaternion.Lerp(m_OriginalRotation, m_TargetRotation, t);
            //selection.color = Color.Lerp(selection.color, m_TargetColor, t);
            //selection.intensity = Mathf.Lerp(selection.intensity, m_TargetIntensity, t);
            //selection.shadowStrength = Mathf.Lerp(selection.shadowStrength, m_TargetShadowStrength, t);
            //selection.shadowBias = Mathf.Lerp(selection.shadowBias, m_TargetShadowBias, t);
            //selection.shadowNormalBias = Mathf.Lerp(selection.shadowNormalBias, m_TargetShadowNormalBias, t);
            //selection.shadowNearPlane = Mathf.Lerp(selection.shadowNearPlane, m_TargetShadowNearPlane, t);

            if (elapsed >= m_Interval) {
                elapsed = 0f;
                SetTargets();
            }

            //if (Vector3.Distance(transform.rotation.eulerAngles,m_TargetRotation.eulerAngles) < 0.1f) {
            //    SetTargets();
            //}

            yield return null;
        }
    }

    private void SetTargets()
    {
        m_OriginalRotation = transform.rotation;
        m_TargetRotation = Random.rotation;
        m_TargetColor = Random.ColorHSV();
        m_TargetIntensity = Random.value * 50f;
        m_TargetShadowStrength = Random.value;
        m_TargetShadowBias = Random.value * 2f;
        m_TargetShadowNormalBias = Random.value * 3f;
        m_TargetShadowNearPlane = Random.value * 10f;
    }

    IEnumerator RandomizeInterval()
    {
        while (true)
        {
            SetTargets();

            transform.rotation = m_TargetRotation;

            selection.color = m_TargetColor;

            selection.intensity = m_TargetIntensity;

            float r = Random.value;
            if (r < 0.33) selection.shadows = LightShadows.Hard;
            else if (r < 0.66) selection.shadows = LightShadows.Soft;
            else selection.shadows = LightShadows.None;

            selection.shadowStrength = m_TargetShadowStrength;

            selection.shadowBias = m_TargetShadowBias;

            selection.shadowNormalBias = m_TargetShadowNormalBias;

            selection.shadowNearPlane = m_TargetShadowNearPlane;

            selection.cookie = allTextures[Mathf.FloorToInt(Random.value * allTextures.Length)];

            selection.cookieSize = Random.value * 100f;

            yield return new WaitForSeconds(m_Interval);
        }
    }
}
