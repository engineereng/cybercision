using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int heartBPM ;
    public Text BPMText;

    void Start(){
        heartBPM = 60;
    }

    [ContextMenu("decrease BPM")]
    public void decreaseBPM(){
        heartBPM -= 10;
        if(heartBPM < 0){
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
}
