using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField]
    Sprite outlineSprite;
    [SerializeField]
    Sprite noOutlineSprite;
    [SerializeField]
    Sprite[] wiggleAnimation;
    
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNoOutline();
    }

    // Update is called once per frame
    void Update()
    {
        // wiggle
    }

    public void SetOutline()
    {
        spriteRenderer.sprite = outlineSprite;
    }

    public void SetNoOutline()
    {
        spriteRenderer.sprite = noOutlineSprite;
    }

    public void StartWiggle()
    {
        // TODO
    }

    public void StopWiggle()
    {

    }
}
