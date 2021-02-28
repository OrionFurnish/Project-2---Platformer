using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
        GameManager.EndGame("You Win!");
    }
}
