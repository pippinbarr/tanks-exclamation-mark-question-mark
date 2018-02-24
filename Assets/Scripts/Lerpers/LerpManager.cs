using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LerpManager : MonoBehaviour
{


    public Text m_Output;
    public static Text output;
    public float minimumLerpTime = 0.1f;
    public float maximumLerpTime = 1f;

    public AudioClip[] m_AudioClips;
    public Mesh[] m_Meshes;
    public Material[] m_Materials;
    public Font[] m_Fonts;
    public Sprite[] m_Sprites;

    private GameObject m_SelectedGameObject;
    private Component m_SelectedComponent;

    private MeshRenderer m_SelectedRenderer;
    private RenderBoundingBox m_RenderBoundingBox;

    enum Selection
    {
        AudioSource,
        Light,
        Camera,
        Transform,
        MeshRenderer,
        MeshFilter,
        RigidBody,
        Text,
        Slider,
        Image,
        BoxCollider,
        CapsuleCollider,
        //ParticleSystem
    }

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
                //Debug.Log("Choosing new lerp...");
                if (m_SelectedGameObject != null)
                {
                    //Debug.Log("Destroying previous ParameterLerper.");
                    Destroy(m_SelectedGameObject.GetComponent<ParameterLerper>());
                }

                lerper = null;

                // Choose a random component type to lerp
                System.Array values = System.Enum.GetValues(typeof(Selection));
                Selection type = (Selection)values.GetValue(Random.Range(0, values.Length));

                //Debug.Log("Chose " + type);
                // Switch on the type to find the appropriate components and process them
                switch (type)
                {
                    case Selection.AudioSource:
                        Component[] audioSources = FindObjectsOfType<AudioSource>();
                        if (SelectComponentFrom(audioSources))
                        {
                            lerper = m_SelectedGameObject.AddComponent<AudioSourceLerper>();
                            lerper.m_AudioClips = m_AudioClips;
                        }
                        break;

                    case Selection.Light:
                        Component[] lights = FindObjectsOfType<Light>();
                        if (SelectComponentFrom(lights))
                        {
                            lerper = m_SelectedGameObject.AddComponent<LightLerper>();
                        }
                        break;

                    case Selection.Camera:
                        Component[] cameras = FindObjectsOfType<Camera>();
                        if (SelectComponentFrom(cameras))
                        {
                            lerper = m_SelectedGameObject.AddComponent<CameraLerper>();
                        }
                        break;

                    case Selection.Transform:
                        Component[] transforms = FindObjectsOfType<Transform>();
                        if (SelectComponentFrom(transforms))
                        {
                            lerper = m_SelectedGameObject.AddComponent<TransformLerper>();
                        }
                        break;

                    case Selection.MeshFilter:
                        Component[] meshFilters = FindObjectsOfType<MeshFilter>();
                        if (SelectComponentFrom(meshFilters))
                        {
                            lerper = m_SelectedGameObject.AddComponent<MeshFilterLerper>();
                            lerper.m_Meshes = m_Meshes;
                        }
                        break;

                    case Selection.MeshRenderer:
                        Component[] meshRenderers = FindObjectsOfType<MeshRenderer>();
                        if (SelectComponentFrom(meshRenderers))
                        {
                            lerper = m_SelectedGameObject.AddComponent<MeshRendererLerper>();
                            lerper.m_Materials = m_Materials;
                        }
                        break;

                    case Selection.RigidBody:
                        Component[] rigidbodies = FindObjectsOfType<Rigidbody>();
                        if (SelectComponentFrom(rigidbodies))
                        {
                            lerper = m_SelectedGameObject.AddComponent<RigidbodyLerper>();
                            lerper.m_Meshes = m_Meshes;
                        }
                        break;

                    case Selection.Text:
                        Component[] texts = FindObjectsOfType<Text>();
                        if (SelectComponentFrom(texts))
                        {
                            lerper = m_SelectedGameObject.AddComponent<TextLerper>();
                            lerper.m_Materials = m_Materials;
                            lerper.m_Fonts = m_Fonts;
                        }
                        break;

                    case Selection.Slider:
                        Component[] sliders = FindObjectsOfType<Slider>();
                        if (SelectComponentFrom(sliders))
                        {
                            //Debug.Log("Adding SliderLerper...");
                            lerper = m_SelectedGameObject.AddComponent<SliderLerper>();
                        }

                        break;

                    case Selection.Image:
                        Component[] images = FindObjectsOfType<Image>();
                        if (SelectComponentFrom(images))
                        {
                            lerper = m_SelectedGameObject.AddComponent<ImageLerper>();
                            lerper.m_Sprites = m_Sprites;
                            lerper.m_Materials = m_Materials;
                        }
                        break;

                    case Selection.BoxCollider:
                        Component[] boxColliders = FindObjectsOfType<BoxCollider>();
                        if (SelectComponentFrom(boxColliders))
                        {
                            lerper = m_SelectedGameObject.AddComponent<BoxColliderLerper>();
                        }
                        break;

                    case Selection.CapsuleCollider:
                        Component[] capsuleColliders = FindObjectsOfType<CapsuleCollider>();
                        if (SelectComponentFrom(capsuleColliders))
                        {
                            lerper = m_SelectedGameObject.AddComponent<CapsuleColliderLerper>();
                        }
                        break;
                    //case Selection.ParticleSystem:
                        //Component[] particleSystems = FindObjectsOfType<ParticleSystem>();
                        //if (SelectComponentFrom(particleSystems))
                        //{
                        //    Debug.Log("Creating lerper.");
                        //    lerper = m_SelectedGameObject.AddComponent<ParticleSystemLerper>();
                        //}
                        //break;

                }

                if (m_SelectedComponent != null)
                {
                    lerper.m_MinimumLerpTime = minimumLerpTime;
                    lerper.m_MaximumLerpTime = maximumLerpTime;
                    lerper.m_Selection = m_SelectedComponent;
                }
            }

            yield return null;
        }
    }


    bool SelectComponentFrom(Component[] components)
    {
        if (components.Length == 0)
        {
            //Debug.Log("Found no components of this type.");
            m_SelectedComponent = null;
            return false;
        }
        else
        {
            m_SelectedComponent = components[Random.Range(0, components.Length)];
            m_SelectedGameObject = m_SelectedComponent.gameObject;
            m_SelectedRenderer = m_SelectedGameObject.GetComponent<MeshRenderer>();
            return true;
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
