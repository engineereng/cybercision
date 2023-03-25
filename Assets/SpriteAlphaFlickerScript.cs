using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAlphaFlickerScript : MonoBehaviour
{

    private SpriteRenderer sprite;

    public float minAlpha = 0.5f, maxAlpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if(sprite == null)
        {
            Debug.LogWarning("No sprite renderer for SpriteAlphaFlickerScript");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color c = sprite.color;
        c.a = Mathf.Abs(Mathf.Sin(Time.fixedTime)) * (maxAlpha - minAlpha) + minAlpha;
        sprite.color = c;
    }
}
