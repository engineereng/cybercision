using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameObject circleResonance;
    public GameObject squareResonance;
    public Sprite squareSprite;
    public Sprite circleSprite;
    private GameObject resonance;
    void Start(){
        int type = Random.Range(0, 2);
        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();
        if(type == 0){
            gameObject.tag = "Circle";
            resonance = circleResonance;
            spriteRender.sprite = circleSprite;
            spriteRender.color = new Color(209f, 219f, 26f, 1f);
            spriteRender.size = new Vector2(0.4f, 0.4f);
        }
        else if(type == 1){
            gameObject.tag = "Square";
            resonance = squareResonance;
            spriteRender.sprite = squareSprite;
            spriteRender.color = new Color(26, 54, 224, 1f);
            spriteRender.size = new Vector2(0.3f, 0.3f);
        }
    }
    public void spawnResonance(){
        Instantiate(resonance, transform.position, transform.rotation);
    }
}
