using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_HorizontalInput;
    private float m_VerticalInput;
    private float m_SteeringAngle;

    public WheelCollider frontRightWheel, frontLeftWheel; //Front wheelcolliders
    public WheelCollider backRightWheel, backLeftWheel; //Back wheelcolliders

    public Transform frontRightWheelT,frontLeftWheelT; //Front wheel transform
    public Transform backRightWheelT,backLeftWheelT; //Back wheel transform
    public float maxSteerAngle = 300;
    public float motorForce = 5000;

    public void GetInput()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");
        //print("Horizoninput= " + m_HorizontalInput);
        //print("VerticInput= " + m_VerticalInput);
    }
    private void Steer()
    {
        m_SteeringAngle = maxSteerAngle * m_HorizontalInput;
        frontLeftWheel.steerAngle = m_SteeringAngle;
        frontRightWheel.steerAngle = m_SteeringAngle;
    }
    private void Accelerate()
    {
        frontLeftWheel.motorTorque = m_VerticalInput * motorForce;
        //print(frontLeftWheel.motorTorque + " force");
        frontRightWheel.motorTorque = m_VerticalInput * motorForce;
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
