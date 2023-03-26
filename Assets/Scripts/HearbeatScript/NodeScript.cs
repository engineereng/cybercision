using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    public GameObject circleResonance;
    public GameObject squareResonance;
    public Sprite squareSprite;
    public Sprite circleSprite;
    private GameObject resonance;

    Image image;

    public void init(){
        image = GetComponent<Image>();
        int type = Random.Range(0, 2);
        if(type == 0)changeToCircle();
        else if(type == 1)changeToSquare();
    }
    public void spawnResonance(){
        Instantiate(resonance, transform.position, transform.rotation, this.transform);
    }

    public void changeToSquare(){
        gameObject.tag = "Square";
        resonance = squareResonance;
        image.sprite = squareSprite;
        image.rectTransform.sizeDelta = new Vector2(0.4f, 0.4f);
    }

    public void changeToCircle(){
        gameObject.tag = "Circle";
        resonance = circleResonance;
        image.sprite = circleSprite;
        image.rectTransform.sizeDelta = new Vector2(0.4f, 0.4f);
    }
}
