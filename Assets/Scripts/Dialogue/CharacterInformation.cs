using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{

    //The name for referencing this character in a script
    [SerializeField]
    public string ScriptReferenceName;

    [SerializeField]
    public string CharacterName;

    [SerializeReference]
    public Sprite CharacterImage;
}
