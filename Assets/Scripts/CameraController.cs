using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;

    void Update() {
        if(target != null) {
            Vector3 targetPos = target.transform.position;
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }
}
