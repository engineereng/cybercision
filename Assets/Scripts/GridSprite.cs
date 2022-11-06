using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSprite : MonoBehaviour
{
    public Collider2D girdBox;
    public bool mouseOverGrid = false;

    void Update(){
        mouseOverGrid = girdBox.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    
}
