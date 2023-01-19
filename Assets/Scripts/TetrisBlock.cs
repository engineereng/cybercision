using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TetrisBlock : MonoBehaviour{
    public Camera cam;
    public Grid grid;
    public GridSprite gridSprite;
    public SpriteRenderer spriteRenderer;
    private float cellSize;
    private bool isSelected;

    void Awake() {
        cellSize = 1.0f;
    }
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isSelected) {
            if (Input.GetButtonDown("RotateClockwise"))
                transform.Rotate(0, 0, -90);

            if (Input.GetButtonDown("RotateCounterClockwise"))
                transform.Rotate(0, 0, 90);
        }
    }

    void OnMouseDrag() {
        isSelected = true;
        spriteRenderer.sortingOrder = 1;
        transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector2(cellSize + 0.2f, cellSize + 0.2f);
    }
    
    void OnMouseUpAsButton() {
        isSelected = false;
        spriteRenderer.sortingOrder = 0;
        transform.localScale = new Vector2(cellSize, cellSize);
        PlaceBlock(gridSprite.mouseOverGrid);
    }


    private void PlaceBlock(bool onGrid) {
        if (!onGrid) {
            transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        } else {
            Vector3Int cellPosition =  grid.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));
            transform.position = grid.CellToWorld(cellPosition) + new Vector3(cellSize/2,cellSize/2,cellSize/2);
        }
    }
}
