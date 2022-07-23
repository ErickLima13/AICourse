using System;
using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject fuel;

    public bool isAuto;

    void Start()
    {

    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if (isAuto)
        {
            if (CalculateDistance() > 5)
            {
                AutoPilot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isAuto = !isAuto;
        }
    }

    private void AutoPilot()
    {
        Vector3 direction = fuel.transform.position - transform.position;
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);

        if (direction.magnitude > 0.1f)
        {
            CalculateAngle();
        }
    }

    private void CalculateAngle()
    {
        Vector3 tankForward = transform.up;
        Vector3 fuelDirection = fuel.transform.position - transform.position;

        float dot = tankForward.x * fuelDirection.x + tankForward.y * fuelDirection.y;

        float angle = MathF.Acos(dot / (tankForward.magnitude * fuelDirection.magnitude));

        print("Angle :" + angle * Mathf.Rad2Deg);
        print("Unity Angle:" + Vector3.Angle(tankForward, fuelDirection));

        Debug.DrawRay(transform.position, tankForward * 10, Color.green, 2);
        Debug.DrawRay(transform.position, fuelDirection, Color.red, 2);

        int clockwise = 1;
        if (Cross(tankForward, fuelDirection).z < 0)
        {
            clockwise = -1;
        }

        //transform.Rotate(0, 0, (angle * Mathf.Rad2Deg * clockwise) * 0.02f);

        float unityAngle = Vector3.SignedAngle(tankForward, fuelDirection, transform.forward);

        transform.Rotate(0, 0, unityAngle * 0.02f);
    }

    private Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    private float CalculateDistance()
    {
        Vector3 tankPos = this.transform.position;
        Vector3 FuelPos = fuel.transform.position;

        float distance = MathF.Sqrt(
            MathF.Pow(tankPos.x - FuelPos.x, 2) + MathF.Pow(tankPos.y - FuelPos.y, 2) + MathF.Pow(tankPos.z - FuelPos.z, 2));

        float unityDistance = Vector3.Distance(tankPos, FuelPos);

        print("Distance:" + distance);
        print("Unity Distance:" + unityDistance);

        return unityDistance;
    }
}