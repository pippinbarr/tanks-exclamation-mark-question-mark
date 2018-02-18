using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

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


    protected virtual void Start()
    {
        m_Parameters = new ArrayList();
        m_LerpComplete = false;
    }

    public virtual IEnumerator StartLerp()
    {

        ArrayList parameterToLerp = (ArrayList)m_Parameters[Mathf.FloorToInt(Random.value * m_Parameters.Count)];
        string parameterName = (string)parameterToLerp[1];
        PropertyInfo pInfo = m_Selection.GetType().GetProperty(parameterName);
        if (pInfo != null)
        {

            Debug.Log("Parameter: " + parameterToLerp[1]);

            LerpManager.output.text = m_Selection.name + ":" + type + ":" + parameterToLerp[1];

            m_CurrentLerpTime = m_MinimumLerpTime + Random.value * (m_MaximumLerpTime - m_MinimumLerpTime);
            StartCoroutine((string)parameterToLerp[0], parameterToLerp);
        }

        yield return null;
    }




    IEnumerator LerpFloat(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        float target = (float)parameterInfo[2] + Random.value * ((float)parameterInfo[3] - (float)parameterInfo[2]);

        Debug.Log("Parameter: " + parameterName);
        Debug.Log("Target: " + target);

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        float start = (float)getter.Invoke(m_Selection, null);

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


    IEnumerator LerpEnum(ArrayList parameterInfo)
    {
        Debug.Log("LerpEnum");
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

        Debug.Log(target.ToString());

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

        Debug.Log("Target: " + target);

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


    IEnumerator LerpPosition(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        Vector3 target = Random.onUnitSphere * 1f;

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Vector3 start = (Vector3)getter.Invoke(m_Selection, null);

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


    IEnumerator LerpScale(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        Vector3 target = Random.onUnitSphere * 2f;

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Vector3 start = (Vector3)getter.Invoke(m_Selection, null);

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


    IEnumerator LerpRotation(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        Quaternion target = Random.rotation;

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Quaternion start = (Quaternion)getter.Invoke(m_Selection, null);

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


    IEnumerator LerpRect(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        Rect target = new Rect(Random.value, Random.value, Random.value, Random.value);

        // Obtain the getter and setter methods for the float property to lerp


        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Rect start = (Rect)getter.Invoke(m_Selection, null);

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
}
