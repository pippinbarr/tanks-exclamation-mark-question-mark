using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LerpManager : MonoBehaviour
{


    public Text m_Output;
    public AudioClip[] m_AudioClips;

    private GameObject m_SelectedGameObject;
    private Component m_SelectedComponent;

    private MeshRenderer m_SelectedRenderer;
    private RenderBoundingBox m_RenderBoundingBox;


    // Use this for initialization
    void Start()
    {
        m_RenderBoundingBox = GetComponent<RenderBoundingBox>();
        StartCoroutine(DoLerping());
        StartCoroutine(UpdateUI());
    }

    IEnumerator DoLerping()
    {
        ParameterLerper lerper = null;

        while (true)
        {
            if (lerper == null || lerper.m_LerpComplete)
            {
                if (m_SelectedGameObject != null)
                {
                    Destroy(m_SelectedGameObject.GetComponent<ParameterLerper>());
                }


                // Get every GameObject
                Component[] audioSources = FindObjectsOfType<AudioSource>();
                Component[] lights = FindObjectsOfType<Light>();
                Component[] cameras = FindObjectsOfType<Camera>();

                Component[] selectedCategory;

                float r = Random.value;
                r = 0.7f;
                if (r < 0.33f)
                {
                    Debug.Log("Lerping an AudioSource");
                    selectedCategory = audioSources;
                }
                else if (r < 0.66f)
                {
                    Debug.Log("Lerping a Light");
                    selectedCategory = lights;
                }
                else
                {
                    Debug.Log("Lerping a Camera");
                    selectedCategory = cameras;
                }

                Component selection = selectedCategory[Mathf.FloorToInt(Random.value * selectedCategory.Length)];
                m_SelectedGameObject = selection.gameObject;
                m_SelectedRenderer = m_SelectedGameObject.GetComponent<MeshRenderer>();


                if (selectedCategory == audioSources)
                {
                    lerper = m_SelectedGameObject.AddComponent<AudioSourceLerper>();
                    lerper.m_Selection = selection;
                    lerper.m_AudioClips = m_AudioClips;
                }
                else if (selectedCategory == lights)
                {
                    lerper = m_SelectedGameObject.AddComponent<LightLerper>();
                    lerper.m_Selection = selection;
                }
                else if (selectedCategory == cameras)
                {
                    lerper = m_SelectedGameObject.AddComponent<CameraLerper>();
                    lerper.m_Selection = selection;
                }
            }

            yield return null;
        }
    }

    IEnumerator UpdateUI()
    {
        while (true)
        {
            Vector3 extents = new Vector3(1, 1, 1);
            Vector3 center = m_SelectedGameObject.transform.position;
            if (m_SelectedRenderer != null)
            {
                extents = m_SelectedRenderer.bounds.extents;
                center = m_SelectedRenderer.bounds.center;
            }
            m_RenderBoundingBox.bounds = new Bounds(Vector3.zero, extents * 2);
            transform.SetPositionAndRotation(center, transform.rotation);
            m_Output.text = m_SelectedGameObject.name;

            yield return null;
        }
    }
}
