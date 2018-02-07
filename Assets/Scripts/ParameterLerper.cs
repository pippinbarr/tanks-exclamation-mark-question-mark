using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class ParameterLerper : MonoBehaviour {

    //public Text m_ParameterText;
    public float m_MinimumLerpTime = 1f;
    public float m_MaximumLerpTime = 2f;

    protected Light m_Selection;

    protected string m_CurrentParameterName;
    protected float m_CurrentLerpTime;
    protected bool m_LerpComplete = true;

    protected ArrayList m_Parameters;

    protected virtual void Start()
    {
        m_Parameters = new ArrayList();
    }

    public IEnumerator StartLerps()
    {
        while (true)
        {
            if (m_LerpComplete)
            {
                m_LerpComplete = false;
                ArrayList parameterToLerp = (ArrayList)m_Parameters[Mathf.FloorToInt(Random.value * m_Parameters.Count)];
                m_CurrentLerpTime = m_MinimumLerpTime + Random.value * (m_MaximumLerpTime - m_MinimumLerpTime);
                for (int i = 0; i < m_Parameters.Count; i++)
                {
                    parameterToLerp = (ArrayList)m_Parameters[i];
                    StartCoroutine((string)parameterToLerp[0], parameterToLerp);
                }
            }
            yield return null;
        }
    }

    IEnumerator LerpFloat(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        // Set the target float value based on m_Parameters
        float targetFloat = (float)parameterInfo[2] + Random.value * ((float)parameterInfo[3] - (float)parameterInfo[2]);

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        float startFloat = (float)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startFloat;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            float newFloat = Mathf.Lerp(startFloat, targetFloat, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { newFloat });

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
        Color targetColor = Random.ColorHSV();

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Color startColor = (Color)getter.Invoke(m_Selection, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startColor;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Color newColor = Color.Lerp(startColor, targetColor, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection, new object[] { newColor });

            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newColor;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }


    IEnumerator LerpEnum(ArrayList parameterInfo)
    {
        // Set elapsed time to 0
        float elapsed = 0f;

        string parameterName = (string)parameterInfo[1];

        string target = (string)parameterInfo[2 + Mathf.FloorToInt(Random.value * (parameterInfo.Count - 2))];

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.GetType().GetProperty(parameterName).GetSetMethod();

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            if (elapsed >= m_CurrentLerpTime/2)
            {
                setter.Invoke(m_Selection, new object[] { parameterInfo.IndexOf(target) - 2 });
            }



            //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + target;

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
        Quaternion targetRotation = Random.rotation;

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.transform.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.transform.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Quaternion startRotation = (Quaternion)getter.Invoke(m_Selection.transform, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection.transform, new object[] { newRotation });

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
        Vector3 target = Random.onUnitSphere * 10f;

        // Obtain the getter and setter methods for the float property to lerp
        MethodInfo getter = m_Selection.transform.GetType().GetProperty(parameterName).GetGetMethod();
        MethodInfo setter = m_Selection.transform.GetType().GetProperty(parameterName).GetSetMethod();

        // Get the current value of the parameter
        Vector3 startPosition = (Vector3)getter.Invoke(m_Selection.transform, null);

        //m_ParameterText.text = m_Selection.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + startRotation;

        // Loop while the time has not yet elapsed
        while (elapsed < m_CurrentLerpTime)
        {
            // Track how much time passed in this loop
            elapsed += Time.deltaTime;

            // Calculate the required interpolation amount
            float t = elapsed / m_CurrentLerpTime;

            // Calculate the new value of the float required through interpolation
            Vector3 newPosition = Vector3.Lerp(startPosition, target, t);

            // Set the new value of the float parameter

            setter.Invoke(m_Selection.transform, new object[] { newPosition });

            //m_ParameterText.text = m_Selection.transform.GetType() + " " + m_Selection.name + " " + m_Selection.GetInstanceID() + "\n" + parameterName + ": " + newRotation;

            // Yield until the next frame
            yield return null;
        }

        m_LerpComplete = true;
    }
}
