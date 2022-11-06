using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Collider2D girdBox;
    public bool mouseOverGrid = false;

    void Start(){
        this.gameObject.layer = LayerMask.NameToLayer("Background");
    }

    void Update(){
        mouseOverGrid = girdBox.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    
}
