using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    [SerializeField]
    public string CharacterName;

    [SerializeReference]
    public Sprite CharacterImage;
}
