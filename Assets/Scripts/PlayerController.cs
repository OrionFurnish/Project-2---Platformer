using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float gravity;
    public float jumpForce;

    CharacterController controller;
    float yForce;

    void Start() {
        Camera.main.GetComponent<CameraController>().target = gameObject;
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        Vector3 moveForce;
        moveForce = new Vector3(Input.GetAxis("Horizontal") * speed, yForce, 0);
        if (controller.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {moveForce.y = jumpForce;}
        } else {moveForce.y -= gravity;}
        yForce = moveForce.y;
        controller.Move(moveForce*Time.deltaTime);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                switch (objectHit.tag) {
                    case "Breakable":
                        Destroy(objectHit.gameObject);
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
}
