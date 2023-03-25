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
        // choose new wiggler out of a random one
        int indexNewWiggler = Random.Range(0, NUM_FINGERS);
        while (alreadyChosen.Contains(indexNewWiggler))
            indexNewWiggler = Random.Range(0, NUM_FINGERS);
        currentChosen = indexNewWiggler;
        alreadyChosen.Add(indexNewWiggler);
        Transform randomChild = transform.GetChild(indexNewWiggler);
        randomChild.GetChild(0).GetComponent<SnapToGoal>().goalEnabled = true;
        Finger finger = randomChild.GetComponent<Finger>();
        finger.SetOutline();
    }

    public void setDone()
    {
        // stop wiggling the current wiggler
        if (alreadyChosen.Count < NUM_FINGERS)
            wiggleRandom();
        else {
            win();
        }
    }
    private void win()
    {
        endText.enabled = true;
        MinigameManager.GetManager().FinishMinigame(true);
        // TODO move onto next scene
    }

    public void setLost()
    {
        endText.text = "You lost.";
        endText.enabled = true;
        MinigameManager.GetManager().FinishMinigame(false);
    }

    // temporary scene reloader for this prototype
    // void OnGUI()
    // {
    //     Event m_Event = Event.current;
    //     if (Event.current.Equals(Event.KeyboardEvent(KeyCode.R.ToString()))) {
    //         Debug.Log("Pressed R to reload");
    //         SceneManager.LoadScene("ArmScene");
    //     }
    // }
}
