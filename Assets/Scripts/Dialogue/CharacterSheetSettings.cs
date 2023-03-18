using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSheetSettings : MonoBehaviour
{
    [SerializeField]
    public TMP_Text CharacterNameText;

    [SerializeField]
    public Image CharacterImage;

    [SerializeField]
    public Image[] DialogueStrikes;


    public bool IsValid()
    {
        return CharacterNameText != null && CharacterImage != null && DialogueStrikes.Length > 0;
    }

}
