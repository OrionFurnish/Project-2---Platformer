using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;
    public Vector2 minCameraBounds;
    public Vector2 maxCameraBounds;

    void Update() {
        if(target != null) {
            Vector3 targetPos = target.transform.position;
            if(targetPos.x < minCameraBounds.x) { targetPos.x = minCameraBounds.x; }
            if(targetPos.y < minCameraBounds.y) { targetPos.y = minCameraBounds.y; }
            if(targetPos.x > maxCameraBounds.x) { targetPos.x = maxCameraBounds.x; }
            if(targetPos.y > maxCameraBounds.y) { targetPos.y = maxCameraBounds.y; }
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }
}
