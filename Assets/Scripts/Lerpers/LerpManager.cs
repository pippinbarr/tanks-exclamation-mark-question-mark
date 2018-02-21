using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LerpManager : MonoBehaviour
{


    public Text m_Output;
    public static Text output;

    public AudioClip[] m_AudioClips;
    public Mesh[] m_Meshes;
    public Material[] m_Materials;
    public Font[] m_Fonts;
    public Sprite[] m_Sprites;

    private GameObject m_SelectedGameObject;
    private Component m_SelectedComponent;

    private MeshRenderer m_SelectedRenderer;
    private RenderBoundingBox m_RenderBoundingBox;


    // Use this for initialization
    void Start()
    {
        m_RenderBoundingBox = GetComponent<RenderBoundingBox>();
        output = m_Output;

        StartCoroutine(DoLerping());
        StartCoroutine(UpdateUI());
    }

    IEnumerator DoLerping()
    {
        ParameterLerper lerper = null;

        while (true)
        {
            if (lerper == null || lerper.m_LerpComplete || lerper.gameObject == null)
            {
                if (m_SelectedGameObject != null)
                {
                    Destroy(m_SelectedGameObject.GetComponent<ParameterLerper>());
                }


                // Get every GameObject
                Component[] audioSources = FindObjectsOfType<AudioSource>();
                Component[] lights = FindObjectsOfType<Light>();
                Component[] cameras = FindObjectsOfType<Camera>();
                Component[] transforms = FindObjectsOfType<Transform>();
                Component[] rectTransforms = FindObjectsOfType<RectTransform>();
                Component[] meshFilters = FindObjectsOfType<MeshFilter>();
                Component[] meshRenderers = FindObjectsOfType<MeshRenderer>();
                Component[] rigidbodies = FindObjectsOfType<Rigidbody>();
                Component[] texts = FindObjectsOfType<Text>();
                Component[] sliders = FindObjectsOfType<Slider>();
                Component[] images = FindObjectsOfType<Image>();

                Component[][] categories = { audioSources, lights, cameras, transforms, meshFilters, meshRenderers, rigidbodies, texts, sliders, images };
                //Component[][] categories = { images };

                Component[] selectedCategory = categories[Mathf.FloorToInt(Random.value * categories.Length)];
                if (selectedCategory.Length == 0)
                {
                    //Debug.Log("Empty category.");
                    lerper = null;
                }
                else
                {
                    Component selection = selectedCategory[Mathf.FloorToInt(Random.value * selectedCategory.Length)];
                    m_SelectedGameObject = selection.gameObject;
                    m_SelectedRenderer = m_SelectedGameObject.GetComponent<MeshRenderer>();


                    if (selectedCategory == audioSources)
                    {
                        Debug.Log("Selected AudioSource...");
                        lerper = m_SelectedGameObject.AddComponent<AudioSourceLerper>();
                        lerper.m_AudioClips = m_AudioClips;
                    }
                    else if (selectedCategory == lights)
                    {
                        Debug.Log("Selected Light...");
                        lerper = m_SelectedGameObject.AddComponent<LightLerper>();
                    }
                    else if (selectedCategory == cameras)
                    {
                        Debug.Log("Selected Camera...");
                        lerper = m_SelectedGameObject.AddComponent<CameraLerper>();
                    }
                    else if (selectedCategory == transforms)
                    {
                        Debug.Log("Selected Transform...");
                        lerper = m_SelectedGameObject.AddComponent<TransformLerper>();
                    }
                    //else if (selectedCategory == rectTransforms)
                    //{
                    //    Debug.Log("Selected RectTransform...");
                    //    lerper = m_SelectedGameObject.AddComponent<RectTransformLerper>();
                    //}
                    else if (selectedCategory == meshFilters)
                    {
                        Debug.Log("Selected MeshFilter...");
                        lerper = m_SelectedGameObject.AddComponent<MeshFilterLerper>();
                        lerper.m_Meshes = m_Meshes;
                    }
                    else if (selectedCategory == meshRenderers)
                    {
                        Debug.Log("Selected MeshRenderer...");
                        lerper = m_SelectedGameObject.AddComponent<MeshRendererLerper>();
                        lerper.m_Materials = m_Materials;
                    }
                    else if (selectedCategory == rigidbodies)
                    {
                        Debug.Log("Selected Rigidbody...");
                        lerper = m_SelectedGameObject.AddComponent<RigidbodyLerper>();
                        lerper.m_Meshes = m_Meshes;
                    }
                    else if (selectedCategory == texts)
                    {
                        Debug.Log("Selected Text...");
                        lerper = m_SelectedGameObject.AddComponent<TextLerper>();
                        lerper.m_Materials = m_Materials;
                        lerper.m_Fonts = m_Fonts;
                    }
                    else if (selectedCategory == sliders)
                    {
                        Debug.Log("Selected Slider...");
                        lerper = m_SelectedGameObject.AddComponent<SliderLerper>();
                    }
                    else if (selectedCategory == images)
                    {
                        Debug.Log("Selected Image...");
                        lerper = m_SelectedGameObject.AddComponent<ImageLerper>();
                        lerper.m_Sprites = m_Sprites;
                        lerper.m_Materials = m_Materials;
                    }
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
            if (m_SelectedGameObject != null)
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
                //m_Output.text = m_SelectedGameObject.name;
            }
            yield return null;
        }
    }
}
