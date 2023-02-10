using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameObject resonance;
    public void spawnResonance(){
        Instantiate(resonance, transform.position, transform.rotation);
    }
}
