using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    public float speed;
    public float gravity;
    public float jumpForce;

    CharacterController controller;
    Animator animator;
    float yForce;

    void Start() {
        Camera.main.GetComponent<CameraController>().target = gameObject;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        Vector3 moveForce;
        float turboMult = 1f;
        if(Input.GetKey(KeyCode.LeftShift)) { turboMult = 2f; }
        moveForce = new Vector3(Input.GetAxis("Horizontal") * speed * turboMult, yForce, 0);
        if (controller.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {moveForce.y = jumpForce;}
            else { moveForce.y = -.5f; }
        } else {moveForce.y -= gravity;}
        yForce = moveForce.y;
        controller.Move(moveForce*Time.deltaTime);

        // Animation Update
        float y = transform.rotation.eulerAngles.y;
        if (moveForce.x < 0) {
            y = 270f;
        }
        else if (moveForce.x > 0) {
            y = 90f;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
        animator.SetFloat("Speed", Input.GetAxis("Horizontal")*turboMult);
        animator.SetFloat("Y Speed", moveForce.normalized.y);
        animator.SetBool("Grounded", controller.isGrounded);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit) {
        Transform objectHit = hit.transform;
        if (objectHit.CompareTag("Death")) {
            GameManager.EndGame("Game Over!");
        }
        else if(hit.normal.y < -.5f) {
            yForce = 0;
            switch (objectHit.tag) {
                case "Breakable":
                    Destroy(objectHit.gameObject);
                    GameManager.AddScore(100);
                    break;
                case "Coin Block":
                    GameManager.AddCoin();
                    break;
                default:
                    break;
            }
        }
    }
}
