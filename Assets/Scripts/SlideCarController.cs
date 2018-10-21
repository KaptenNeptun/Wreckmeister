using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCarController : MonoBehaviour
{
    private float m_HorizontalInput;
    private float m_VerticalInput;
    private float m_SteeringAngle;

    public WheelCollider frontRightWheel, frontLeftWheel; //Front wheelcolliders
    public WheelCollider backRightWheel, backLeftWheel; //Back wheelcolliders

    public GameObject motorPosition;
    public Transform frontRightWheelT, frontLeftWheelT; //Front wheel transform
    public Transform backRightWheelT, backLeftWheelT; //Back wheel transform

    public float maxSteerAngle = 30;
    public float motorForce = 50f;
    Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();


    }

    public void GetInput()
    {
        m_HorizontalInput = Input.GetAxis("XboneLsHori");
        m_VerticalInput = Input.GetAxis("XboneTriggerShared");

    }
    private void Steer()
    {
        m_SteeringAngle = maxSteerAngle * m_HorizontalInput;
        frontLeftWheel.steerAngle = m_SteeringAngle;
        frontRightWheel.steerAngle = m_SteeringAngle;

    }
    private void Accelerate()
    {
        //m_Rigidbody.AddForceAtPosition(m_Rigidbody.transform.forward * m_VerticalInput * motorForce, motorPosition.transform.position, ForceMode.Acceleration);
        if(m_Rigidbody.velocity.magnitude>0 && m_VerticalInput<0)
        {

        }
        else if (m_VerticalInput != 0)
        {
            frontLeftWheel.motorTorque = m_VerticalInput * motorForce;
            frontRightWheel.motorTorque = m_VerticalInput * motorForce;
        }
        else
        {
            frontLeftWheel.motorTorque = 0;
            frontRightWheel.motorTorque = 0;
        }
        //backLeftWheel.motorTorque = m_VerticalInput * motorForce * 0.2f;
        //backRightWheel.motorTorque = m_VerticalInput * motorForce * 0.2f;
        //print("Gas " + m_VerticalInput);
    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRightWheel, frontRightWheelT);
        UpdateWheelPose(frontLeftWheel, frontLeftWheelT);
        UpdateWheelPose(backLeftWheel, backLeftWheelT);
        UpdateWheelPose(backRightWheel, backRightWheelT);
    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
    }
    public void FixedUpdate()
    {
        GetInput();
        Accelerate();
        Steer();
        UpdateWheelPoses();
    }
}
