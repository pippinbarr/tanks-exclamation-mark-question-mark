using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightScript : MonoBehaviour {

    public float m_Interval = 2f;
    public float m_LerpSpeed = 2f;
    public Texture[] allTextures;

    private Light dl;

    private Quaternion m_OriginalRotation;
    private Quaternion m_TargetRotation;
    private Color m_TargetColor;
    private float m_TargetIntensity;
    private float m_TargetShadowStrength;
    private float m_TargetShadowBias;
    private float m_TargetShadowNormalBias;
    private float m_TargetShadowNearPlane;

    private float elapsed = 0f;


	// Use this for initialization
	void Start () {
        dl = GetComponent<Light>();

        //StartCoroutine(RandomizeInterval()); 

        StartCoroutine(RandomizeLerp());    
	}
	
	// Update is called once per frame
    void Update () {
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
            //dl.color = Color.Lerp(dl.color, m_TargetColor, t);
            //dl.intensity = Mathf.Lerp(dl.intensity, m_TargetIntensity, t);
            //dl.shadowStrength = Mathf.Lerp(dl.shadowStrength, m_TargetShadowStrength, t);
            //dl.shadowBias = Mathf.Lerp(dl.shadowBias, m_TargetShadowBias, t);
            //dl.shadowNormalBias = Mathf.Lerp(dl.shadowNormalBias, m_TargetShadowNormalBias, t);
            //dl.shadowNearPlane = Mathf.Lerp(dl.shadowNearPlane, m_TargetShadowNearPlane, t);

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

            dl.color = m_TargetColor;

            dl.intensity = m_TargetIntensity;

            float r = Random.value;
            if (r < 0.33) dl.shadows = LightShadows.Hard;
            else if (r < 0.66) dl.shadows = LightShadows.Soft;
            else dl.shadows = LightShadows.None;

            dl.shadowStrength = m_TargetShadowStrength;

            dl.shadowBias = m_TargetShadowBias;

            dl.shadowNormalBias = m_TargetShadowNormalBias;

            dl.shadowNearPlane = m_TargetShadowNearPlane;

            dl.cookie = allTextures[Mathf.FloorToInt(Random.value * allTextures.Length)];

            dl.cookieSize = Random.value * 100f;

            yield return new WaitForSeconds(m_Interval);
        }
    }
}
