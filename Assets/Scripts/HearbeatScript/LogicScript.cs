using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public int heartBPM ;
    public Text BPMText;
    
    public GameObject slider;

    public GameObject gameOverScreen;

    void Start(){
        heartBPM = 60;
    }
    public void decreaseBPM(){
        heartBPM -= 10;
        if(heartBPM <= 0){
            heartBPM = 0;
        }
        BPMText.text = heartBPM.ToString();
    }

    public void increaseBPM(){
        heartBPM += 3;
        if(heartBPM > 100){
            heartBPM = 100;
        }
        BPMText.text = heartBPM.ToString();
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool isGameOver(){
        if(heartBPM == 0){
            gameOverScreen.SetActive(true);
            return true;
        };
        return false;
    }
}
