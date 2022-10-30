using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour{
    public Rigidbody2D rb;
    public Camera cam;
    public Collider2D groundCollider;
    private bool onGround;

    void Start(){
        onGround = rb.IsTouching(groundCollider);
    }

    void Update(){
        if (!onGround && rb.IsTouching(groundCollider)){
            Debug.Log("Hit the ground!");
            onGround = true;
        }
    }

    void OnMouseDrag(){
        rb.velocity = new Vector2(0,0);
        rb.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition));
        if (!rb.IsTouching(groundCollider)){
            onGround = false;
        }
    }
}
