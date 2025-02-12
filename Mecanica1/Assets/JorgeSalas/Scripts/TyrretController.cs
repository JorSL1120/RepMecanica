using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrretController : MonoBehaviour
{
    public Transform turret, barrel, shootPoint, crosshair;
    public float rotSpeed, bulletSpeed; //La rapidez es la magnitud de la velocidad
    public GameObject bulletPrefab;
    private float angleX;

    void Update()
    {
        TurretRotation();
        BarrelRotation();
        Fire();
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

    float FlyTime()
    {
        float y0 = shootPoint.position.y;
        float g = 9.8f;
        Vector3 V0 = bulletSpeed * shootPoint.forward;
        float vy0 = V0.y;
        return (vy0 + Mathf.Sqrt(vy0 * vy0 + 2*g*y0)) / g;
    }
}
