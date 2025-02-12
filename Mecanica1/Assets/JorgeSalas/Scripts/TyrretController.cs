using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrretController : MonoBehaviour
{
    public Transform turret, barrel, shootPoint, crosshair;
    public float rotSpeed, bulletSpeed; //La rapidez es la magnitud de la velocidad
    public GameObject bulletPrefab, RBbulletPrefab;
    private float angleX;

    void Update()
    {
        TurretRotation();
        BarrelRotation();
        Fire();
        FireRB();

        float T = FlyTime();
        crosshair.position = PositionFunction(T);
    }

    void TurretRotation()
    {
        float dt = Time.deltaTime;
        float hInput = Input.GetAxis("Horizontal");
        float angleY = rotSpeed * hInput * dt;
        Vector3 eulerAngkles = new Vector3(0, angleY, 0);
        turret.Rotate(eulerAngkles, Space.Self);
    }

    void BarrelRotation()
    {
        float dt = Time.deltaTime;
        float vInput = Input.GetAxis("Vertical");
        angleX -= rotSpeed * vInput * dt;
        angleX = Mathf.Clamp(angleX, -90f, 0f);
        barrel.localRotation = Quaternion.Euler(angleX, 0, 0);
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 P0 = shootPoint.position;
            Vector3 direction = shootPoint.forward;
            GameObject bullet = Instantiate(bulletPrefab, P0, Quaternion.identity);
            bullet.GetComponent<KinematicBullet>().initialPosition = P0;
            bullet.GetComponent<KinematicBullet>().initialVelocity = bulletSpeed * direction;
            Destroy(bullet, 7f);
        }
    }

    void FireRB()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 P0 = shootPoint.position;
            Vector3 direction = shootPoint.forward;
            GameObject bulletRB = Instantiate(RBbulletPrefab, P0, Quaternion.identity);
            bulletRB.GetComponent<Rigidbody>().velocity = bulletSpeed * direction;
            Destroy(bulletRB, 10f);
        }
    }

    float FlyTime()
    {
        float y0 = shootPoint.position.y;
        float g = 9.8f;
        Vector3 V0 = bulletSpeed * shootPoint.forward;
        float vy0 = V0.y;
        return (vy0 + Mathf.Sqrt(vy0 * vy0 + 2*g*y0)) / g;
    }

    Vector3 PositionFunction(float time)
    {
        Vector3 gravity = new Vector3(0, -9.8f, 0);
        Vector3 initialPos = shootPoint.position;
        Vector3 initialVel = bulletSpeed * shootPoint.forward;
        return 0.5f * gravity * time * time + initialVel * time + initialPos;
    }
}
