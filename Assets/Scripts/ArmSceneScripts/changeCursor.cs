using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D enterCursorTexture;
    // public Texture2D exitCursorTexture;
    public Texture2D dragCursorTexture;

    void OnMouseEnter()
    {
        centerCrosshair(enterCursorTexture);
    }
    
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDrag()
    {
        centerCrosshair(dragCursorTexture);
    }

    void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseUpAsButton()
    {
        centerCrosshair(enterCursorTexture);
    }
    private void centerCrosshair(Texture2D crosshairTexture) {
        Vector2 cursorOffset = new Vector2(crosshairTexture.width / 2, crosshairTexture.height / 2); // for centered crosshairs
        Cursor.SetCursor(crosshairTexture, cursorOffset, CursorMode.Auto);
    }
}
