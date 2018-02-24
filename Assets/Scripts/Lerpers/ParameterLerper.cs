using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.Rendering;


public class ParameterLerper : MonoBehaviour
{

    //public Text m_ParameterText;
    public float m_MinimumLerpTime = 1f;
    public float m_MaximumLerpTime = 2f;
    public bool m_LerpComplete = true;
    public string type = "";

    //protected Light m_Selection;
    public Component m_Selection;

    protected string m_CurrentParameterName;
    protected float m_CurrentLerpTime;

    protected ArrayList m_Parameters;

    private bool m_Bounce = false;

    public AudioClip[] m_AudioClips;
    public Mesh[] m_Meshes;
    public Material[] m_Materials;
    public Font[] m_Fonts;
    public Sprite[] m_Sprites;


    protected virtual void Start()
    {
        m_Parameters = new ArrayList();
        m_LerpComplete = false;
    }

    private void OnDisable()
    {
        m_LerpComplete = true;
    }


    public virtual IEnumerator StartLerp()
    {

        ArrayList parameterToLerp = (ArrayList)m_Parameters[Mathf.FloorToInt(Random.value * m_Parameters.Count)];
        string parameterName = (string)parameterToLerp[1];
        //Debug.Log("Parameter: " + parameterToLerp[1]);

        PropertyInfo pInfo = m_Selection.GetType().GetProperty(parameterName);
        if (pInfo != null)
        {
            LerpManager.output.text = m_Selection.name + ": " + type + "." + parameterToLerp[1];

            m_CurrentLerpTime = m_MinimumLerpTime + Random.value * (m_MaximumLerpTime - m_MinimumLerpTime);
            StartCoroutine((string)parameterToLerp[0], parameterToLerp);
        }
        else
        {
            //Debug.Log("Parameter doesn't exist.");
            m_LerpComplete = true;
        }

        yield return null;
    }




    // ADDITIVE

    IEnumerator LerpFloat(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        float min = (float)parameterInfo[2];
        float max = (float)parameterInfo[3];
        float amount = min + Random.value * (max - min);
        if (Random.value < 0.5f) amount = -amount;


        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        float start = (float)getter.Invoke(m_Selection, null);

        float target = start + amount;

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startFloat;

        // Loop while the time has not yet elapsed
        bool lerpingBack = false;

        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            float next = Mathf.Lerp(start, target, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });


            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newFloat;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // HAS ADDITIVE AND NON-ADDITIVE VERSION BASED ON PARAMETER

    IEnumerator LerpInt(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        bool additive = (bool)parameterInfo[4];

        // Obtain the getter and setter methods for the float property to lerp

        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        int start = (int)getter.Invoke(m_Selection, null);

        int target;
        int min = (int)parameterInfo[2];
        int max = (int)parameterInfo[3];

        // Set the target float value based on m_Parameters
        if (additive)
        {
            int amount = Mathf.FloorToInt(min + Random.value * (max - min));
            if (Random.value < 0.5f) amount = -amount;
            target = start + amount;
        }
        else
        {
            target = Mathf.FloorToInt(min + Random.value * ((max - min));
        }
        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startFloat;

        // Loop while the time has not yet elapsed
        bool lerpingBack = false;

        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            int next = Mathf.FloorToInt(Mathf.Lerp(start, target, t));

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpColor(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        Color target = Random.ColorHSV();

        // Obtain the getter and setter methods for the float property to lerp

        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Color start = (Color)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startColor;

        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Color next = Color.Lerp(start, target, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }
            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newColor;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpEnum(ArrayList parameterInfo)
    {
        //Debug.Log("LerpEnum");
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Obtain the getter and setter methods for the float property to lerp

        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        string value = getter.Invoke(m_Selection, null).ToString();
        string enumName = (string)getter.Invoke(m_Selection, null).GetType().Name;
        string start = enumName + "." + value;

        object target = parameterInfo[2 + Mathf.FloorToInt(Random.value * (parameterInfo.Count - 2))];

        //Debug.Log(target.ToString());

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2)
            {
                setter.Invoke(m_Selection, new object[] { parameterInfo.IndexOf(target) - 2 });
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpBool(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        bool start = (bool)getter.Invoke(m_Selection, null);

        bool target = !start;

        //Debug.Log("Target: " + target);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2)
            {
                setter.Invoke(m_Selection, new object[] { target });
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpAudioClip(ArrayList parameterInfo)
    {
        AudioSource selectionAsAudioSource = (AudioSource)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        AudioClip[] audioClips = (AudioClip[])parameterInfo[2];
        AudioClip target = audioClips[Mathf.FloorToInt(Random.value * audioClips.Length)];



        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = selectionAsAudioSource.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        AudioClip start = (AudioClip)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {
                selectionAsAudioSource.Stop();
                setter.Invoke(m_Selection, new object[] { target });
                selectionAsAudioSource.Play();
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // ADDITIVE, ONE PROPERTY AT A TIME
    IEnumerator LerpRotation(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];


        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Quaternion start = (Quaternion)getter.Invoke(m_Selection, null);

        float min = (float)parameterInfo[2];
        float max = (float)parameterInfo[3];
        float amount = min + Random.value * (max - min);
        if (Random.value < 0.5f) amount = -amount;

        float r = Random.value;
        float newX = start.eulerAngles.x;
        float newY = start.eulerAngles.y;
        float newZ = start.eulerAngles.z;
        if (r < 0.33f)
        {
            newX += amount;
        }
        else if (r < 0.66)
        {
            newY += amount;
        }
        else
        {
            newZ += amount;
        }


        //Debug.Log(m_Selection.name + ": " + start.eulerAngles.x + "," + start.eulerAngles.y + "," + start.eulerAngles.z + " > " + newX + "," + newY + "," + newZ);

        // Set the target float value based on m_Parameters
        Quaternion target = new Quaternion();
        target.eulerAngles = new Vector3(newX, newY, newZ);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Quaternion next = Quaternion.Lerp(start, target, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // ADDITIVE, ONLY ONE AXIS PER LERP
    IEnumerator LerpVector3(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];
        float min = (float)parameterInfo[2];
        float max = (float)parameterInfo[3];
        float amount = min + Random.value * (max - min);
        if (Random.value < 0.5f) amount = -amount;

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Vector3 start = (Vector3)getter.Invoke(m_Selection, null);

        // Set the target float value based on m_Parameters
        Vector3 target = new Vector3(start.x, start.y, start.z);
        float r = Random.value;
        if (r < 0.33) target.x += amount;
        else if (r < 0.66) target.y += amount;
        else target.z += amount;

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Vector3 next = Vector3.Lerp(start, target, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // ADDITIVE, ONE PROPERTY PER LERP
    IEnumerator LerpRect(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];
        float min = (float)parameterInfo[2];
        float max = (float)parameterInfo[3];
        float amount = min + Random.value * (max - min);
        if (Random.value < 0.5f) amount = -amount;


        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Rect start = (Rect)getter.Invoke(m_Selection, null);


        Rect target = new Rect(start.x, start.y, start.width, start.height);
        float r = Random.value;
        if (r < 0.25) target.x = Mathf.Max(Mathf.Min(target.x + amount, 1f), 0f);
        else if (r < 0.5) target.y = Mathf.Max(Mathf.Min(target.y + amount, 1f), 0f);
        else if (r < 0.75) target.width = Mathf.Max(Mathf.Min(target.width + amount, 1f), 0f);
        else target.height = Mathf.Max(Mathf.Min(target.height + amount, 1f), 0f);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;


        bool lerpingBack = false;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Rect next = new Rect(
                Mathf.Lerp(start.x, target.x, t),
                Mathf.Lerp(start.y, target.y, t),
                Mathf.Lerp(start.width, target.width, t),
                Mathf.Lerp(start.height, target.height, t));


            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { next });

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                start = next;
                lerpingBack = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpMesh(ArrayList parameterInfo)
    {
        MeshFilter selectionAsMeshFilter = (MeshFilter)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        Mesh[] meshes = (Mesh[])parameterInfo[2];
        Mesh target = meshes[Mathf.FloorToInt(Random.value * meshes.Length)];


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = selectionAsMeshFilter.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Mesh start = (Mesh)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {
                setter.Invoke(m_Selection, new object[] { target });
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }

    // NON-ADDITIVE
    IEnumerator LerpMaterials(ArrayList parameterInfo)
    {
        //MeshFilter selectionAsMeshFilter = (MeshFilter)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        //Material[] materials = (Mesh[])parameterInfo[2];
        //Mesh target = meshes[Mathf.FloorToInt(Random.value * meshes.Length)];


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Material[] start = (Material[])getter.Invoke(m_Selection, null);
        Material[] target = new Material[start.Length];
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = m_Materials[Mathf.FloorToInt(Random.value * m_Materials.Length)];
        }

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {

                setter.Invoke(m_Selection, new object[] { target });
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }

    // NON-ADDITIVE
    IEnumerator LerpMaterial(ArrayList parameterInfo)
    {
        //MeshFilter selectionAsMeshFilter = (MeshFilter)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        //Material[] materials = (Mesh[])parameterInfo[2];
        //Mesh target = meshes[Mathf.FloorToInt(Random.value * meshes.Length)];


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Material start = (Material)getter.Invoke(m_Selection, null);
        Material target = m_Materials[Mathf.FloorToInt(Random.value * m_Materials.Length)];


        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {

                setter.Invoke(m_Selection, new object[] { target });
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }

    // NON-ADDITIVE
    IEnumerator LerpFont(ArrayList parameterInfo)
    {
        //AudioSource selectionAsAudioSource = (AudioSource)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        //Fon[] audioClips = (AudioClip[])parameterInfo[2];
        Font target = m_Fonts[Mathf.FloorToInt(Random.value * m_Fonts.Length)];



        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Font start = (Font)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {
                //selectionAsAudioSource.Stop();
                setter.Invoke(m_Selection, new object[] { target });
                //selectionAsAudioSource.Play();
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // NON-ADDITIVE
    IEnumerator LerpSprite(ArrayList parameterInfo)
    {
        //AudioSource selectionAsAudioSource = (AudioSource)m_Selection;
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        //Fon[] audioClips = (AudioClip[])parameterInfo[2];
        Sprite target = m_Sprites[Mathf.FloorToInt(Random.value * m_Sprites.Length)];



        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Sprite start = (Sprite)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        bool lerpingBack = false;
        bool lerpingForward = true;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
            {
                //selectionAsAudioSource.Stop();
                setter.Invoke(m_Selection, new object[] { target });
                //selectionAsAudioSource.Play();
                lerpingForward = false;
            }

            if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
            {
                elapsed = 0;
                target = start;
                lerpingBack = true;
                lerpingForward = true;
            }

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    // THE WASTELAND OF PARTICLE SYSTEM PROPERTIES LURKS BELOW
    // BEWARE.

    //IEnumerator LerpMainParticleSystemModule(ArrayList parameterInfo)
    //{
    //    Debug.Log("Lerping Main Particle System Module");
    //    // Set elapsed time to 0
    //    float elapsed = 0f;

    //    string parameterName = (string)parameterInfo[1];

    //    MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
    //    MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

    //    // Get the current value of the parameter
    //    ParticleSystem.MainModule start = (ParticleSystem.MainModule)getter.Invoke(m_Selection, null);


    //    bool lerpingBack = false;
    //    bool lerpingForward = true;

    //    // Loop while the time has not yet elapsed
    //    while (elapsed < m_CurrentLerpTime)
    //    {
    //        // Track how much time passed in this loop
    //        elapsed += Time.deltaTime;

    //        if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
    //        {
    //            ParticleSystem.MinMaxCurve curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.startDelay = curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.startLifetime = curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.startSpeed = curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constantMin = Random.value * 20f;
    //            curve.constantMax = Random.value * 20f;

    //            start.startSize = curve;
    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.gravityModifier = curve;

    //            start.randomizeRotationDirection = Random.value;

    //            start.startColor = Random.ColorHSV();

    //            start.maxParticles = Mathf.FloorToInt(Random.value * 100f);
    //            lerpingForward = false;
    //        }

    //        if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
    //        {
    //            elapsed = 0;
    //            //target = start;
    //            lerpingBack = true;
    //            lerpingForward = true;
    //        }

    //        //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

    //        // Yield until the next frame
    //        yield return null;
    //    }

    //    m_LerpComplete = true;
    //}


    //IEnumerator LerpEmissionParticleSystemModule(ArrayList parameterInfo)
    //{
    //    Debug.Log("Lerping Main Particle System Module");
    //    // Set elapsed time to 0
    //    float elapsed = 0f;

    //    string parameterName = (string)parameterInfo[1];

    //    MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
    //    MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

    //    // Get the current value of the parameter
    //    ParticleSystem.EmissionModule start = (ParticleSystem.EmissionModule)getter.Invoke(m_Selection, null);


    //    bool lerpingBack = false;
    //    bool lerpingForward = true;

    //    // Loop while the time has not yet elapsed
    //    while (elapsed < m_CurrentLerpTime)
    //    {
    //        // Track how much time passed in this loop
    //        elapsed += Time.deltaTime;

    //        if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
    //        {
    //            ParticleSystem.MinMaxCurve curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.rateOverTime = curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.rateOverDistance = curve;


    //            lerpingForward = false;
    //        }

    //        if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
    //        {
    //            elapsed = 0;
    //            //target = start;
    //            lerpingBack = true;
    //            lerpingForward = true;
    //        }

    //        //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

    //        // Yield until the next frame
    //        yield return null;
    //    }

    //    m_LerpComplete = true;
    //}



    //IEnumerator LerpShapeParticleSystemModule(ArrayList parameterInfo)
    //{
    //    Debug.Log("Lerping Main Particle System Module");
    //    // Set elapsed time to 0
    //    float elapsed = 0f;

    //    string parameterName = (string)parameterInfo[1];

    //    MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
    //    MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

    //    // Get the current value of the parameter
    //    ParticleSystem.ShapeModule start = (ParticleSystem.ShapeModule)getter.Invoke(m_Selection, null);


    //    bool lerpingBack = false;
    //    bool lerpingForward = true;

    //    // Loop while the time has not yet elapsed
    //    while (elapsed < m_CurrentLerpTime)
    //    {
    //        // Track how much time passed in this loop
    //        elapsed += Time.deltaTime;

    //        if (elapsed >= m_CurrentLerpTime / 2 && lerpingForward)
    //        {
    //            start.shapeType = ParticleSystemShapeType.Box;
    //            ParticleSystem.MinMaxCurve curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.rateOverTime = curve;

    //            curve = new ParticleSystem.MinMaxCurve();
    //            curve.constant = Random.value * 20f;
    //            start.rateOverDistance = curve;


    //            lerpingForward = false;
    //        }

    //        if (m_Bounce && elapsed >= m_CurrentLerpTime && !lerpingBack)
    //        {
    //            elapsed = 0;
    //            //target = start;
    //            lerpingBack = true;
    //            lerpingForward = true;
    //        }

    //        //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

    //        // Yield until the next frame
    //        yield return null;
    //    }

    //    m_LerpComplete = true;
    //}
}
