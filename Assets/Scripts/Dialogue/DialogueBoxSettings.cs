using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxSettings : MonoBehaviour
{
    [SerializeField]
    public TMP_Text DialogueText;

    [SerializeField]
    public TMP_Text[] OptionTexts;

    [SerializeField]
    public GameObject[] OptionTextButtons;

    public bool IsValid()
    {
        return DialogueText != null;
    }
}
