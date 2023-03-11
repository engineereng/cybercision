using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSet : MonoBehaviour
{
    public NodeScript[] nodes;
    private NodeScript Node1;
    private NodeScript Node2;
    private NodeScript Node3;
    // Start is called before the first frame update
    void Start()
    {
        foreach(NodeScript node in nodes){
            node.init();
        }
        for(int i = 0 ; i < 7 ; i++){
            Node1 = nodes[i];
            Node2 = nodes[i + 1];
            Node3 = nodes[i + 2];
            if(Node1.gameObject.tag == Node2.gameObject.tag && 
                    Node1.gameObject.tag == Node3.gameObject.tag){
                if(Node3.gameObject.tag == "Circle"){
                    Node3.changeToSquare();
                }
                else{
                    Node3.changeToCircle();
                }
            }
        }
    }
}
