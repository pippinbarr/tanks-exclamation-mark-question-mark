using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SliderLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "direction", "Slider.Direction.BottomToTop", "Slider.Direction.TopToBottom", "Slider.Direction.LeftToRight", "Slider.Direction.RightToLeft" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "minValue", 25f, 50f, 0f, 200f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "maxValue", 25f, 50f, 0f, 200f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "value", 25f, 50f, 0f, 200f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "interactable" }));


        m_Selection = GetComponent<Slider>();

        type = "Slider";

        StartCoroutine(StartLerp());
    }
	
}
