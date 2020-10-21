using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Mirror;

public class PlayerMovementScript : NetworkBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Animator anim;
    private Vector3 posAnt;

    private Vector3 vel;
    Vector3 velocity;
    bool isGrounded;
    float x, z;
    private Vector3 refAdelante = new Vector3(0,0,1);
    private Vector3 refDerecha = new Vector3(-1, 0, 0);
    float rotacion;
    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        posAnt = transform.position;
        if (!hasAuthority) { return; }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        vel.Set(transform.position.x - posAnt.x, 0, transform.position.z - posAnt.z);
        rotacion = Vector3.Angle(refAdelante, playerBody.forward);
        rotacion = Mathf.Sign(Vector3.Dot(playerBody.forward, refDerecha)) * rotacion;
        vel = Quaternion.Euler(0,rotacion,0)*vel;
        vel = vel.normalized;
        Debug.Log(rotacion);
        Debug.Log(vel);
        anim.SetFloat("VelX", vel.x);
        anim.SetFloat("VelY", vel.z);
    }
}
