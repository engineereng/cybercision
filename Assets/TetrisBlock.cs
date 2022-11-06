using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TetrisBlock : MonoBehaviour{
    public Camera cam;
    private float gridSize = 1.7f;
    public Grid grid;
    public SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDrag(){
        spriteRenderer.sortingOrder = 1;
        transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector2(gridSize + 0.2f, gridSize+ 0.2f);
    }

    void OnMouseUpAsButton(){
        spriteRenderer.sortingOrder = 0;
        transform.localScale = new Vector2(gridSize, gridSize);
        PlaceBlock(grid.mouseOverGrid);
    }


    private void PlaceBlock(bool onGrid){
        if(!onGrid){
            transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        }else{
            transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x/gridSize)*gridSize,
                Mathf.RoundToInt(transform.position.y/gridSize)*gridSize);
        }
    }
}
