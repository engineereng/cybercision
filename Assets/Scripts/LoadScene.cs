using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string debugButtonTarget, armButtonTarget, reprogramButtonTarget, chargeButtonTarget;

    public void debugButtonCallback() {
        LoadSceneByName(debugButtonTarget);
    }
    public void armButtonCallback() {
        LoadSceneByName(armButtonTarget);
    }
    public void reprogramButtonCallback() {
        LoadSceneByName(reprogramButtonTarget);
    }
    public void chargeButtonCallback() {
        LoadSceneByName(chargeButtonTarget);
    }
    public void LoadSceneByName(string target){
        Debug.Log("Loaded scene: " + target);
        SceneManager.LoadScene(target);
    }
}