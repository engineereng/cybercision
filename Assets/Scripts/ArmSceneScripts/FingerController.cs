using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
    [SerializeField] private GameObject finger1, finger2, finger3, finger4, finger5;
    public int NUM_FINGERS = 5;
    HashSet<int> alreadyChosen = new HashSet<int>();

    // Start is called before the first frame update
    void Start()
    {
        WiggleRandom();
    }

    void WiggleRandom()
    {
        Color newColor = Color.green; // the color is a placeholder for wiggling animation
        // choose new wiggler out of a random one
        int indexNewWiggler = Random.Range(0, NUM_FINGERS);
        while (alreadyChosen.Contains(indexNewWiggler))
            indexNewWiggler = Random.Range(0, NUM_FINGERS);
        alreadyChosen.Add(indexNewWiggler);
        Transform randomChild = transform.GetChild(indexNewWiggler);
        SpriteRenderer renderer = randomChild.GetComponent<SpriteRenderer>();
        renderer.color = newColor;
    }

    public void setDone(int index)
    {
        
    }
}
