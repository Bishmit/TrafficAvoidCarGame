using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    public Transform frontRightWheel;
    public Transform frontLeftWheel;
    public Transform backRightWheel;
    public Transform backLeftWheel;

    public float motorForce = 100f;
    public float steeringValue = 30;
    float verticalInput;
    float horizontalInput;
    float steeringAngle;

    void FixedUpdate()
    {
        GetInput();
        Move();
        Steer();
        UpdateWheels();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
    }

    void Steer()
    {
        steeringAngle = steeringValue * horizontalInput;
        frontLeftWheelCollider.steerAngle = steeringAngle;
        frontRightWheelCollider.steerAngle = steeringAngle;
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheel);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheel);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheel);
        UpdateSingleWheel(backRightWheelCollider, backRightWheel);
    }

    void UpdateSingleWheel(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
