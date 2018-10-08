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

    public Transform frontRightWheelT, frontLeftWheelT; //Front wheel transform
    public Transform backRightWheelT, backLeftWheelT; //Back wheel transform

    public float maxSteerAngle = 30;
    public float motorForce = 50;
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
        if (m_VerticalInput != 0)
        {
            frontLeftWheel.motorTorque = m_VerticalInput * motorForce;
            frontRightWheel.motorTorque = m_VerticalInput * motorForce;
            backLeftWheel.motorTorque = m_VerticalInput * motorForce * 0.2f;
            backRightWheel.motorTorque = m_VerticalInput * motorForce * 0.2f;
        }
        else
        {
            frontLeftWheel.motorTorque = 0f;
            frontRightWheel.motorTorque = 0f;
            backLeftWheel.motorTorque = 0f;
            backRightWheel.motorTorque = 0f;
        }
        //print("Gas " + m_VerticalInput);
    }

    public void FixedUpdate()
    {
        GetInput();
        Accelerate();
        Steer();
    }
}
