using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float fire_rate = 5f;
    private float next_time_to_fire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If left button is pressed - fire
        if (Input.GetButton("Fire1") && Time.time >= next_time_to_fire)
        {
            float angle_y = GetComponent<Rigidbody>().gameObject.transform.eulerAngles.y;
            if (angle_y == 180f || angle_y == 0f)
            {
                next_time_to_fire = Time.time + 1f / fire_rate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
