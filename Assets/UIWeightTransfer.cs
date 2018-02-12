using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIWeightTransfer : MonoBehaviour {

    public Car m_CarController;

    public Image m_Background;
    public Image m_CenterOfMass;




	// Use this for initialization
	void Start ()
    {
        InitSize();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void InitSize()
    {
        Vector2 vBoundingHVector = m_CarController.GetMassBoundingBoxRatio();
        ResizeCarShape(vBoundingHVector);
        
    }

    private void ResizeCarShape(Vector2 _vSize)
    {
        if(_vSize.x>_vSize.y)
        {
            _vSize /= _vSize.x;
        }
        else
        {
            _vSize /= _vSize.y;
        }

        Vector3 vLocalScale = m_Background.rectTransform.localScale;

        vLocalScale.x *= _vSize.x;
        vLocalScale.y *= _vSize.y;
        m_Background.rectTransform.localScale = vLocalScale;
    }

    
}
