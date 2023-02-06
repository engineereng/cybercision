using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FingerController : MonoBehaviour
{
    [SerializeField] private GameObject finger1, finger2, finger3, finger4;
    [SerializeField] private int NUM_FINGERS = 4;
    HashSet<int> alreadyChosen = new HashSet<int>();
    [SerializeField] private int currentChosen;
    [SerializeField] TMP_Text endText;

    // Start is called before the first frame update
    void Start()
    {
        wiggleRandom();
    }
    void wiggleRandom()
    {
        Color newColor = Color.green; // the color is a placeholder for wiggling animation
        // choose new wiggler out of a random one
        int indexNewWiggler = Random.Range(0, NUM_FINGERS);
        while (alreadyChosen.Contains(indexNewWiggler))
            indexNewWiggler = Random.Range(0, NUM_FINGERS);
        currentChosen = indexNewWiggler;
        alreadyChosen.Add(indexNewWiggler);
        Transform randomChild = transform.GetChild(indexNewWiggler);
        randomChild.GetChild(1).GetComponent<SnapToGoal>().goalEnabled = true;
        SpriteRenderer renderer = randomChild.GetComponent<SpriteRenderer>();
        renderer.color = newColor;
    }

    public void setDone()
    {
        // stop wiggling the current wiggler
        transform.GetChild(currentChosen).GetComponent<SpriteRenderer>().color = Color.black;
        if (alreadyChosen.Count < NUM_FINGERS)
            wiggleRandom();
        else {
            endText.enabled = true;
        }
    }

    public void setLost()
    {
        endText.text = "You lost. Press R to restart the demo.";
        endText.enabled = true;
    }

    // TODO remove this temporary scene reloader for this prototype
    void OnGUI()
    {
        Event m_Event = Event.current;
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.R.ToString()))) {
            Debug.Log("Pressed R to reload");
            SceneManager.LoadScene("ArmScene");
        }
    }
}
