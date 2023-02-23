using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip[] clips;
    public VideoPlayer BPMPlayer;
    public void Healthy(){
        BPMPlayer.clip = clips[0];
    }
    public void Flatline(){
        BPMPlayer.clip = clips[1];
    }
}
