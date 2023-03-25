using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFire : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites;
    bool backwards;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    int index;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        backwards = false;
        index = 0;
        InvokeRepeating("Animate", 0f, 0.125f);
    }

    // Update is called once per frame
    void Animate()
    {
        if (backwards)
            index--;
        else 
            index++;
        if (index >= sprites.Length) {
            backwards = true;
        } else if (index < 0) {
            backwards = false;
        } else {
            spriteRenderer.sprite = sprites[index];       
            // transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);     
        }
    }
}
