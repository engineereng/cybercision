using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScrollUp : MonoBehaviour
{

    public float ScrollSpeed = 2.5f;
    public float ScrollFinishY = 0;
    public string ExitSceneName = "MainScene";

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < ScrollFinishY)
        {
            transform.Translate(0, Time.deltaTime * ScrollSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(ExitSceneName, LoadSceneMode.Single);
        }

    }
}
