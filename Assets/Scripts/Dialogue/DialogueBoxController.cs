using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    public int SelectedOption;

    [SerializeReference]
    public AudioClip[] music;
    
    private bool WaitingForInput;

    public AudioSource musicSource;

    [SerializeField]
    public AudioSource scrollSoundSource, selectSoundSource;
    
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

        TypedText Previous = CurrentDialogueText;
        for (int i = 0; i < CurrentOptionTexts.Length; i++)
        {
            CurrentOptionTexts[i] = new TypedText();
            Previous = CurrentOptionTexts[i];
        }

        CurrentOptions = new string[settings.OptionTexts.Length];
        for(int i = 0; i < CurrentOptions.Length; i++)
        {
            CurrentOptions[i] = "";
        }

        Reset();

        WaitingForInput = false;
    }

    public void ShowDialogue(string Dialogue, bool AwaitInput = true)
    {
        WaitingForInput = AwaitInput;
        CurrentText = Dialogue;
    }

    public bool IsSelectingOption()
    {
        foreach(string text in CurrentOptions)
        {
            if(text.Length > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsWaitingForInput()
    {
        return WaitingForInput;
    }

    private bool IsTextFinishedTypingOut()
    {
        if (!CurrentDialogueText.IsFinished())
        {
            return false;
        }
        foreach(TypedText tt in CurrentOptionTexts)
        {
            if (!tt.IsFinished())
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if (IsWaitingForInput())
        {
            if (IsSelectingOption())
            {
                scrollSoundSource.pitch = 0.8f - SelectedOption / 6f;
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    SelectedOption = Mathf.Max(0, SelectedOption - 1);
                    if (scrollSoundSource)
                    {
                        scrollSoundSource.Play();
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SelectedOption = Mathf.Min(CurrentOptions.Length - 1, SelectedOption + 1);
                    if (scrollSoundSource)
                    {
                        scrollSoundSource.Play();
                    }
                }
            }
            

            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (IsTextFinishedTypingOut())
                {
                    WaitingForInput = false;

                    for (int i = 0; i < CurrentOptions.Length; i++)
                    {
                        CurrentOptions[i] = "";
                    }

                    if (selectSoundSource)
                    {
                        selectSoundSource.Play();
                    }
                }
                else
                {
                    CurrentDialogueText.ForceFinish();
                    foreach(TypedText tt in CurrentOptionTexts)
                    {
                        tt.ForceFinish();
                    }

                    if (scrollSoundSource)
                    {
                        scrollSoundSource.pitch = 0.5f;
                        scrollSoundSource.Play();
                    }
                }

                
            }

        }

        CurrentDialogueText.SetText(CurrentText);

        float Delta = Time.deltaTime;

        CurrentDialogueText.Update(Delta);
        settings.DialogueText.text = CurrentDialogueText.GetVisibleText();

        for (int i = 0; i < CurrentOptionTexts.Length; i++)
        {
            settings.OptionTexts[i].transform.parent.gameObject.SetActive(true);
            CurrentOptionTexts[i].SetText(CurrentOptions[i]);
            CurrentOptionTexts[i].Update(Delta);
            settings.OptionTexts[i].text = CurrentOptionTexts[i].GetVisibleText();
            if (i == SelectedOption)
            {
                settings.OptionTexts[i].color = new Color(1, 0, 0);
            }
            else
            {
                settings.OptionTexts[i].color = new Color(1, 1, 1);
            }
        }

        for (int i = 0; i < settings.OptionTextButtons.Length; i++)
        {
            settings.OptionTextButtons[i].SetActive(CurrentOptions[i].Length > 0);
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
