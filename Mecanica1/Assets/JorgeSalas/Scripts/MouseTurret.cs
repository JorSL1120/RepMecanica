using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTurret : MonoBehaviour
{
    public float rotationSpeed, bulletSpeed, planeDistance;
    public Transform turret, barrel, shootPoint;
    public Canvas canvas;
    public GameObject bulletPrefab;
    public RectTransform image;

    void Update()
    {
        float z0 = shootPoint.position.z;
        float vz0 = bulletSpeed * shootPoint.forward.z;
        float T = (planeDistance - z0) / vz0;
        //canvas.GetComponentInChildren<RectTransform>().position = PositionFunction(T);
        image.position = PositionFunction(T);
        canvas.planeDistance = planeDistance;
        MouseController();
        Fire();
    }

    void MouseController()
    {
        float dt = Time.deltaTime;
        float yRotation = dt * rotationSpeed * Input.GetAxis("Mouse X");
        float xRotation = dt * rotationSpeed * Input.GetAxis("Mouse Y");
        turret.Rotate(Vector3.up, yRotation, Space.Self);
        barrel.Rotate(Vector3.right, -xRotation, Space.Self);
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<KinematicBullet>().initialPosition = shootPoint.position;
            bullet.GetComponent<KinematicBullet>().initialVelocity = bulletSpeed * shootPoint.forward;
            Destroy(bullet, 15f);
        }
    }

    Vector3 PositionFunction(float t)
    {
        Vector3 P0 = shootPoint.position;
        Vector3 V0 = bulletSpeed * shootPoint.forward;
        Vector3 g = new Vector3(0, -9.8f, 0);
        return 0.5f * g * t * t + V0 * t + P0;
    }
}
