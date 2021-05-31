using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cam : MonoBehaviour
{
    private Transform t;
    private double multiplier_value;
    private double divider_step;
    private readonly float max_add_move;
    private readonly float min_add_move;
    private readonly float one_addition;
    private float act_add_move;

    public Cam() 
    {
        multiplier_value = 1.2d;
        divider_step = 700d;
        max_add_move = (float)(Math.Sin(Math.PI / 2d) * multiplier_value);
        min_add_move = (float)(Math.Sin(-Math.PI / 2d) * multiplier_value);
        one_addition = (float)Math.Sin(Math.PI / divider_step);
        act_add_move = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody player = GameObject.Find("SpiderCube").GetComponent<Rigidbody>();
        float new_pos_x = player.position.x;
        float new_pos_y = player.position.y + 1.5f;
        float new_pos_z = player.position.z - 4.2f;
        if (Input.GetKey(KeyCode.D))
        {
            act_add_move += one_addition;
            if (act_add_move > max_add_move)
                act_add_move = max_add_move;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            act_add_move -= one_addition;
            if (act_add_move < min_add_move)
                act_add_move = min_add_move;
        }
        else
        {
            if (act_add_move > 0)
            {
                act_add_move -= one_addition;
                if (act_add_move < 0)
                    act_add_move = 0;
            }
            else if (act_add_move < 0)
            {
                act_add_move += one_addition;
                if (act_add_move > 0)
                    act_add_move = 0;
            }
        }
        new_pos_x += act_add_move;

        t.position = new Vector3(new_pos_x, new_pos_y, new_pos_z);
    }
}
