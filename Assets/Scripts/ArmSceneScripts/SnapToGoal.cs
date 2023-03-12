using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGoal : MonoBehaviour
{
    [SerializeField] private GameObject wire;
    [SerializeField] public bool goalEnabled = false;

    private void OnTriggerStay2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        if (goalEnabled && otherObject.Equals(wire) && !otherObject.GetComponent<MoveWire>().following){
            Debug.Log("Hit goal!");
            gameObject.GetComponentInParent<FingerController>().setDone();
            // set wire to look like a wire is in it
            gameObject.GetComponent<SpriteRenderer>().color = otherObject.GetComponent<SpriteRenderer>().color;
            Destroy(otherObject);
        }
    }
}
