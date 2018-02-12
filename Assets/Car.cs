using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    Rigidbody m_Rigidbody;

    public float m_AccelerationFactor;
    public float m_BreakFactor;
    public float m_TurnFactor;

    private float m_fInputAcceleration;
    private float m_fInputDirection;
    private float m_fInputBrake;

	// Use this for initialization
	void Start () {
        InitComponents();
        InitComponentsVariables();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateInputs();
        ApplyPlayerInput();
        ApplyPhysicFriction();
        UpdateWeightTransfer();
    }

    void InitComponents()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void InitComponentsVariables()
    {
        m_fInputAcceleration = 0.0f;
        m_fInputDirection = 0.0f;
        m_fInputBrake = 0.0f;
    }

    void UpdateInputs()
    {
        m_fInputAcceleration = Input.GetAxisRaw("Vertical");
        m_fInputDirection = Input.GetAxisRaw("Horizontal");
        m_fInputBrake = Input.GetAxisRaw("Brake");
    }

    void  ApplyPlayerInput()
    {
        // Compute Acceleration Power. Priority to Acceleration for the moment
        Vector3 vAcceleration = Vector3.zero;
        float fAcceleration = 0.0f;
        if(m_fInputAcceleration>0)
        {
            fAcceleration = m_fInputAcceleration*m_AccelerationFactor;
        }
        else if(m_fInputBrake>0)
        {
            fAcceleration = -m_fInputBrake * m_BreakFactor;
        }
        vAcceleration.z = fAcceleration;
        vAcceleration *= m_AccelerationFactor;


        Quaternion qCarRotation = transform.rotation;
        Vector3 vRealAcceleration = qCarRotation * vAcceleration;
        m_Rigidbody.AddForce(vRealAcceleration, ForceMode.Acceleration);
        
    }

    void ApplyPhysicFriction()
    {
        // Air Friction
        Vector3 vVelocity = m_Rigidbody.velocity;
        Vector3 vDrag = -0.4257f * vVelocity.magnitude * vVelocity;
        m_Rigidbody.AddForce(vDrag, ForceMode.Force);

        // Ground Friction
        Vector3 vGroundDrag = -12.8f * vVelocity;
        vGroundDrag.y = 0.0f;
        m_Rigidbody.AddForce(vGroundDrag, ForceMode.Force);
    }

    public float GetCarSpeed()
    {
        Vector3 vSpeed = m_Rigidbody.velocity;
        float fMagnitude = vSpeed.magnitude;
        return fMagnitude;
    }

    private void UpdateWeightTransfer()
    {
        float fdTime = Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 vCOMWorldPosition = rigidbody.worldCenterOfMass;
        Vector3 vRelativePosition = rigidbody.centerOfMass;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vCOMWorldPosition, 1.0f);
        Gizmos.color = Color.gray;

        // BoundingBox
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Bounds boudingBox = boxCollider.bounds;
        Vector3 vBoundingBoxMin = boudingBox.min;
        Vector3 vBoundingBoxMax = boudingBox.max;
        
        Gizmos.color = Color.green;
        Debug.Log("BoundingBox min = " + vBoundingBoxMin);

        Gizmos.DrawWireSphere(vBoundingBoxMin,0.5f);
        Gizmos.DrawWireSphere(vBoundingBoxMax, 0.5f);


    }

    public void GetCenterOfMassInformation()
    {
        // BoundingBox
        


    }


    public Vector2 GetMassBoundingBoxRatio()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Bounds boudingBox = boxCollider.bounds;
        Vector3 vBoundingBoxMin = boudingBox.min;
        Vector3 vBoundingBoxMax = boudingBox.max;

        Quaternion qTransformRotation = transform.rotation;
        Quaternion invertRotation = Quaternion.Inverse(qTransformRotation);

        Vector3 minToMax = vBoundingBoxMax - vBoundingBoxMin;
        minToMax =  invertRotation *minToMax;

        Vector2 vBoundingHVector;
        vBoundingHVector.x = minToMax.x;
        vBoundingHVector.y = minToMax.z;

        return vBoundingHVector;

    }
    
}
