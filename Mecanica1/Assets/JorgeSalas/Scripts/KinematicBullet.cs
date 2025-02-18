using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicBullet : MonoBehaviour
{
    public Vector3 initialPosition, initialVelocity;
    private Vector3 gravity = new Vector3 (0f, -9.8f, 0f);
    private float time;

    void Update()
    {
        time += Time.deltaTime;
        transform.position = PositionFunction();
        transform.forward = VelocityFunction();
    }

    Vector3 PositionFunction()
    {
        float time2 = time * time;
        return 0.5f * gravity * time2 + initialVelocity * time + initialPosition;
    }

    Vector3 VelocityFunction()
    {
        return gravity * time + initialVelocity;
    }
}
