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
        for(int i = 0 ; i < 3 ; i++){
            Node1 = nodes[i*3];
            Node2 = nodes[i*3 + 1];
            Node3 = nodes[i*3 + 2];
            if(Node1.gameObject.tag == Node2.gameObject.tag && 
                    Node1.gameObject.tag == Node3.gameObject.tag){
                if(Node3.gameObject.tag == "Circle"){
                    print("A");
                    Node3.changeToSquare();
                }
                else{
                    print("B");
                    Node3.changeToCircle();
                }
            }
        }
    }
}
