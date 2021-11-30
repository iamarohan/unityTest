using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private bool altKeyWasPressed;
    private float horizontalInput;
    private int superJumpsRemaining;
    private Rigidbody rigidbodyComponent;

    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            jumpKeyWasPressed = true;
        } 
        if (Input.GetKeyDown(KeyCode.LeftAlt)){
            altKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate() {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){
            return;
        }

        if (jumpKeyWasPressed){
            float jumpPower = 5f;
            if (superJumpsRemaining > 0 && altKeyWasPressed) {
                jumpPower*=2;
                superJumpsRemaining--;
                altKeyWasPressed = false;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower,ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        } 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 9) {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}