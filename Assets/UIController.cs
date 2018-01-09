using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public Car m_PlayerCar;

    public Text m_SpeedText;
    public Image m_AccelerationGauge;
    public Image m_BrakeGauge;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCarSpeed();
        UpdatePlayerInput();
    }

    void UpdateCarSpeed()
    {
        float fCarSpeed = m_PlayerCar.GetCarSpeed();

        // to km/h
        fCarSpeed *= 3.6f;

        m_SpeedText.text = fCarSpeed.ToString("0");

    }

    void UpdatePlayerInput()
    {

        SetGauge(m_AccelerationGauge, "Acceleration");
        SetGauge(m_BrakeGauge, "Brake");
    }

    void SetGauge(Image _image, string _sAxis)
    {
        float fAxisInput = Input.GetAxisRaw(_sAxis);

        Vector3 vImageLocalScale = _image.rectTransform.localScale;
        vImageLocalScale.y = fAxisInput;
        _image.rectTransform.localScale = vImageLocalScale;

    }
}
