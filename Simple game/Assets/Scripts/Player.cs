using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyPressed = false;
    private float horizontalInput = 0;
    private int jumpCounter = 0;
    private int score = 0;
    private int maxScore = 0;
    private string turn = "front";
    private float rotation_step = 17f; //degress
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        //Count max score
        maxScore = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Check if space is clicked
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            turn = "right";
        }
        else if (Input.GetKey(KeyCode.A))
        {
            turn = "left";
        }
        else
        {
            turn = "front";
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        //Check if player has collision with other object
        Collider[] colliders = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask);
        if (colliders.Length != 0)
        {
            //Check if player is grounded
            if (rigidbodyComponent.velocity.y <= 0)
            {
                jumpCounter = 0;
            }

            //Check if player is in collision with EndStick
            foreach (Collider collider in colliders)
            {
                //Check for the end of the map
                if ((collider.gameObject.layer == 8) && (score == maxScore))
                {
                    GameObject.FindGameObjectWithTag("EndText").GetComponent<Renderer>().enabled = true;
                    break;
                }
            }
        }

        //Jump logic
        if (jumpKeyPressed)
        {
            if (jumpCounter < 2)
            {
                rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, 0, rigidbodyComponent.velocity.z);
                rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
                jumpCounter++;
            }
            jumpKeyPressed = false;
        }

        //Debug.Log("x: "+GetComponent<Transform>().position.x+" y: "+GetComponent<Transform>().position.y); //Pokazanie pozycji gracza
        //Movement logic
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2f, rigidbodyComponent.velocity.y, 0);

        //Check if player falls over the map
        if (rigidbodyComponent.position.y <= -7)
        {
            ResetPlayerPosition();
        }

        //Roatation of player logic
        float act_rotation_y = rigidbodyComponent.rotation.eulerAngles.y;
        float rotate_degree = 0f;
        if (turn == "right")
        {
            if (act_rotation_y > 0)
            {
                rotate_degree = -rotation_step;
                if ((act_rotation_y + rotate_degree) < 0f)
                    rotate_degree = -act_rotation_y;
            }
        }
        else if (turn == "left")
        {
            if (act_rotation_y < 180f)
            {
                rotate_degree = rotation_step;
                if ((act_rotation_y + rotate_degree) > 180f)
                    rotate_degree = 180f - act_rotation_y;
            }
        }
        else if (turn == "front")
        {
            if (act_rotation_y > 90f)
            {
                rotate_degree = -rotation_step;
                if ((act_rotation_y + rotate_degree) < 90f)
                    rotate_degree = 90f - act_rotation_y;
            }
            else if (act_rotation_y < 90f)
            {
                rotate_degree = rotation_step;
                if ((act_rotation_y + rotate_degree) > 90f)
                    rotate_degree = 90f - act_rotation_y;
            }
        }
        rigidbodyComponent.transform.Rotate(0f, rotate_degree, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;
        if (layer == 7) //if player collides with coints
        {
            Destroy(other.gameObject);
            score++;
        }
        else if (layer == 10) //if player collides with enemy - reset send to spawn point and reset health of all enemies
        {
            ResetPlayerPosition();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().SetHealth(30);
            }
        }
    }

    public void ResetPlayerPosition()
    {
        rigidbodyComponent.position = new Vector3(0f, 3f, 0f);
    }
}
