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
            if (rigidbodyComponent.velocity.y == 0)
                jumpCounter = 0;

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

        //Movement logic
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2f, rigidbodyComponent.velocity.y, 0);

        //Check if player falls over the map
        if (rigidbodyComponent.position.y <= -10)
        {
            rigidbodyComponent.position = new Vector3(0f, 2f, 0f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            score++;
        }
    }

}
