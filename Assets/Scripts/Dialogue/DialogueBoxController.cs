using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour
{
    
    private DialogueBoxSettings settings;

    private TypedText CurrentDialogueText;

    private TypedText[] CurrentOptionTexts;
    
    [SerializeField]
    public string CurrentText;

    [SerializeField]
    public string[] CurrentOptions;

    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponent<DialogueBoxSettings>();

        if (settings == null)
        {
            Debug.LogError("DialogueBoxController failed to initialize! No settings found...");
            Destroy(this);
            return;
        }

        if (!settings.IsValid())
        {
            Debug.LogError("Invalid settings found......");
            Destroy(this);
            return;
        }

        CurrentOptionTexts = new TypedText[settings.OptionTexts.Length];
        CurrentText = "";

        CurrentDialogueText = new TypedText();
        for (int i = 0; i < CurrentOptionTexts.Length; i++)
        {
            CurrentOptionTexts[i] = new TypedText();
        }

        CurrentOptions = new string[settings.OptionTexts.Length];

        Reset();
    }

    // Update is called once per frame
    void Update()
    {

        CurrentDialogueText.SetText(CurrentText);

        float Delta = Time.deltaTime;

        CurrentDialogueText.Update(Delta);
        settings.DialogueText.text = CurrentDialogueText.GetVisibleText();

        for (int i = 0; i < CurrentOptionTexts.Length; i++)
        {
            CurrentOptionTexts[i].SetText(CurrentOptions[i]);
            CurrentOptionTexts[i].Update(Delta);
            settings.OptionTexts[i].text = CurrentOptionTexts[i].GetVisibleText();
        }
    }

    private void Reset()
    {
        settings.DialogueText.text = "";
        for(int i = 0; i < settings.OptionTexts.Length; i++)
        {
            settings.OptionTexts[i].text = "";
        }
    }
}
