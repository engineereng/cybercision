using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour
{
    
    private DialogueBoxSettings settings;

    //TODO: Rename this
    private TypedText CurrentDialogueText;

    //And this...
    private TypedText[] CurrentOptionTexts;
    
    [SerializeField]
    public string CurrentText;

    [SerializeField]
    public string[] CurrentOptions;

    private bool WaitingForInput;

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

        WaitingForInput = false;
    }

    public void ShowDialogue(string Dialogue, bool AwaitInput = true)
    {
        WaitingForInput = AwaitInput;
        CurrentText = Dialogue;
    }

    public bool IsWaitingForInput()
    {
        return WaitingForInput;
    }

    // Update is called once per frame
    void Update()
    {

        if (IsWaitingForInput())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WaitingForInput = false;
            }
        }

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
