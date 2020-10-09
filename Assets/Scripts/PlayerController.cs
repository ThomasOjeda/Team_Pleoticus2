using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    public CharacterController player;
    private Vector3 playerInput = new Vector3();
    private float norm;

    public float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        norm = horizontalMove + verticalMove;
        playerInput.Set(horizontalMove/norm,0,verticalMove/norm);
        player.Move(playerInput * playerSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        
    }
}
