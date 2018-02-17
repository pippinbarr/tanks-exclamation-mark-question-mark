using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LerpManager : MonoBehaviour
{


    public Text m_Output;
    public AudioClip[] m_AudioClips;

    private GameObject m_Selected;
    private MeshRenderer m_SelectedRenderer;
    private bool lerping = false;
    private RenderBoundingBox m_RenderBoundingBox;


    // Use this for initialization
    void Start()
    {
        m_RenderBoundingBox = GetComponent<RenderBoundingBox>();
        StartCoroutine(DoLerping());
    }

    IEnumerator DoLerping()
    {
        Debug.Log("DoLerping()");
        // Get every GameObject
        Component[] all = FindObjectsOfType<AudioSource>();
        //GameObject[] all = GameObject.FindGameObjectsWithTag("AudioSource");

        // Choose one
        Component selection = all[Mathf.FloorToInt(Random.value * all.Length)];
        m_Selected = selection.gameObject;

        // Get its renderer (may or may not actually have one)
        m_SelectedRenderer = m_Selected.GetComponent<MeshRenderer>();

        // Get all relevant components
        //Light[] lights = m_Selected.GetComponents<Light>();
        AudioSource[] audioSources = m_Selected.GetComponents<AudioSource>();

        if (audioSources.Length != 0)
        {
            Debug.Log("Adding AudioSourceLerper");
            AudioSourceLerper lerper = m_Selected.AddComponent<AudioSourceLerper>();
            lerper.m_Selection = audioSources[0];
            lerper.m_AudioClips = m_AudioClips;
        }


        while (true)
        {
            
            SetUI();

            yield return null;
        }

    }

    void SetUI ()
    {
        Vector3 extents = new Vector3(1, 1, 1);
        Vector3 center = m_Selected.transform.position;
        if (m_SelectedRenderer != null)
        {
            extents = m_SelectedRenderer.bounds.extents;
            center = m_SelectedRenderer.bounds.center;
        }
        m_RenderBoundingBox.bounds = new Bounds(Vector3.zero, extents * 2);
        transform.SetPositionAndRotation(center, transform.rotation);
        m_Output.text = m_Selected.name;

    }
}
