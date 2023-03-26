using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField]
    Sprite outlineSprite;
    [SerializeField]
    Sprite noOutlineSprite;
    // AnimateFire wiggleAnimation;
    [SerializeField]
    Sprite[] wiggleAnimation;
    bool backwards;
    bool animatingEnabled;
    int index = 0;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNoOutline();
        spriteRenderer = GetComponent<SpriteRenderer>();
        backwards = false;
    }

    public void SetNoOutline()
    {
        spriteRenderer.sprite = noOutlineSprite;
    }

    public void StartWiggle()
    {
        animatingEnabled = true;
        StartCoroutine(Animate());
    }

    public void StopWiggle()
    {
        StopAllCoroutines();
        animatingEnabled = false;
        spriteRenderer.sprite = outlineSprite;
    }

    IEnumerator Animate()
    {
        if (animatingEnabled) {
            for (;;) {
                if (backwards)
                index--;
                else 
                    index++;
                if (index >= wiggleAnimation.Length) {
                    backwards = true;
                } else if (index < 0) {
                    backwards = false;
                } else {
                    spriteRenderer.sprite = wiggleAnimation[index];       
                }
                yield return new WaitForSeconds(.125f);
            }
        }
    }
    
}
