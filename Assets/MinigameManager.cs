using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    
    private string ReturnSceneName;
    private int ReturnIndex;

    private int NumberOfGamesLeft;

    private bool WonAllGames;

    private List<string> possibleMinigames;

    private bool RunningMinigames;

    public static MinigameManager GetManager()
    {
        MinigameManager manager = GameObject.FindObjectOfType<MinigameManager>();
        if (manager == null)
        {
            GameObject gameObject = new GameObject("Minigame Manager");
            gameObject.AddComponent<MinigameManager>();

            Debug.Log("Created minigame manager...");

            manager = gameObject.GetComponent<MinigameManager>();
           

            manager.RunningMinigames = false;
            SceneManager.sceneLoaded += manager.OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        return manager;
    }

    //Allow developers to skip minigames
#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            FinishMinigame(true);
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            FinishMinigame(false);
        }
    }

#endif

    public void StartMinigames(string[] minigamesToPlay, int ReturnIndex)
    {
        RunningMinigames = true;
        NumberOfGamesLeft = minigamesToPlay.Length;
        this.ReturnSceneName = SceneManager.GetActiveScene().name;
        this.ReturnIndex = ReturnIndex;
        WonAllGames = true;
        possibleMinigames = new List<string>();
        foreach (string minigameSceneName in minigamesToPlay)
        {
            possibleMinigames.Add(minigameSceneName);
        }
        StartRandomMinigame();
    }

    public void FinishMinigame(bool Win)
    {
        WonAllGames &= Win;
        NumberOfGamesLeft--;
        if(NumberOfGamesLeft <= 0)
        {
            Debug.Log("Minigames finished, returning to whence we came");
            ReturnToDialogue();
        }
        else
        {
            Debug.Log("Minigame finished, playing " + NumberOfGamesLeft + " more minigames...");
            StartRandomMinigame();
        }
    }

    private void StartRandomMinigame()
    {
        int randomIndex = Random.Range(0, possibleMinigames.Count - 1);

        string randomGame = possibleMinigames[randomIndex];
        possibleMinigames.RemoveAt(randomIndex);

        SceneManager.LoadScene(randomGame);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (RunningMinigames && NumberOfGamesLeft <= 0)
        {

            RunningMinigames = false;

            CyberDialogueScript script = FindObjectOfType<CyberDialogueScript>();

            if (script)
            {
                script.index = ReturnIndex;
                script.WonAllMinigames = WonAllGames;
                Debug.Log("Returned to scene " + ReturnSceneName + ", skipping dialogue to " + ReturnIndex + ".");
            }
            else
            {
                Debug.LogError("Couldn't find script in loaded scene. I guess we just die now.");
            }

        }
        
    }

    private void ReturnToDialogue()
    {
        SceneManager.LoadScene(ReturnSceneName, LoadSceneMode.Single);
    }
    
}
