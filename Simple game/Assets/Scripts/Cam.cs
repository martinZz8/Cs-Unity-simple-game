using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody player = GameObject.Find("SpiderCube").GetComponent<Rigidbody>();
        t.position = new Vector3(player.position.x+1f, player.position.y+0.7f, player.position.z-5f);
    }
}
