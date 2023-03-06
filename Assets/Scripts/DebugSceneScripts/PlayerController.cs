using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
 
    public int count;
    public int scoreToWin;
 
    public int timeLeft = 15;
    public TextMeshProUGUI countdownText;

    public moveBug[] bugScripts;
    public GameObject bugParentObject;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        scoreToWin = 5;
        SetCountText();
        winText.text = "";
        bugScripts = bugParentObject.GetComponentsInChildren<moveBug>();
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("Time Left: " + timeLeft);
 
        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";
            EndGame();
        }

        foreach (moveBug bug in bugScripts) {
            count += bug.bugShotCount;
            SetCountText();
        }

    }

    public void AddCount() {
        count = count + 1;
        SetCountText();
    }

    public void SetCountText()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= scoreToWin)
        {
            winText.text = "You Win!";
            EndGame();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void EndGame()
    {
        foreach (moveBug bug in bugScripts) {
            bug.isShootable = false;
        }

        // closes application
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
