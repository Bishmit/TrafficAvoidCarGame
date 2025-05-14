using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;

    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;

    [SerializeField] private Transform carCenterOfMassTransform; 
    [SerializeField] private Rigidbody rigidBody;  

    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float steeringValue = 30f;
    [SerializeField] private float brakeForce = 1000f;
    
    float verticalInput;
    float horizontalInput;
    float steeringAngle;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); 
        rigidBody.centerOfMass = carCenterOfMassTransform.localPosition; 
    }

    void FixedUpdate()
    {
        GetInput();
        Move();
        Steer();
        UpdateWheels();
        Brake(); 
        AutoSteeringAdjustToCentre(); 
        Debug.Log(GetCarSpeed()); 
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    void Brake()
    {
    if (Input.GetKey(KeyCode.Space))
    {
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        backLeftWheelCollider.brakeTorque = brakeForce;
        backRightWheelCollider.brakeTorque = brakeForce;
        rigidBody.linearDamping = 2f; 
    }
    else
    {
        frontLeftWheelCollider.brakeTorque = 0f;
        frontRightWheelCollider.brakeTorque = 0f;
        backLeftWheelCollider.brakeTorque = 0f;
        backRightWheelCollider.brakeTorque = 0f;
        rigidBody.linearDamping = 0f; 
    }
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

    void AutoSteeringAdjustToCentre(){
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        float rotationSpeed = 2f;

        if (!IsSteering() && Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotation, 
                Time.deltaTime * rotationSpeed
            );
        } 
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    void UpdateSingleWheel(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    bool IsSteering(){
        return horizontalInput != 0; 
    }

    public float GetCarSpeed(){
       return rigidBody.linearVelocity.magnitude; 
    }
}

