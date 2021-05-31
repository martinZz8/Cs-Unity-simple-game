using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;

    private float speed = 5f;
    private Rigidbody rb;
    private int damage = 10;
    private float end_x;
    private float range = 8f;
    private string turn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 proper_vec = transform.right;
        if (proper_vec.x >= 0)
        {
            turn = "right";
            end_x = rb.position.x + range;
            proper_vec.x = 1f;
        }
        else
        {
            turn = "left";
            end_x = rb.position.x - range;
            proper_vec.x = -1f;
        }

        rb.velocity = proper_vec * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if ((turn == "right" && (rb.position.x >= end_x)) || (turn == "left" && (rb.position.x <= end_x)))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider hit_info)
    {
        //Debug.Log(hit_info.name);
        int layer = hit_info.gameObject.layer;
        if (layer != 6 && layer != 7) //if player doesn't collide with himself and coin
        {
            if (layer == 10) //if player collides with enemy
            {
                hit_info.GetComponent<Enemy>().TakeDamage(damage);
            }
            GameObject elem = Instantiate(impactEffect, rb.position, rb.rotation);
            Destroy(elem, 0.3f);
            Destroy(gameObject);
        }
    }
}
