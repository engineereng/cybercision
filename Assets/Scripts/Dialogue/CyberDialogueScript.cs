using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CyberDialogueScript : MonoBehaviour
{

    [SerializeField]
    public CharacterInformation[] characters;
    
    [SerializeField]
    public TextAsset defaultDialogueScript;

    [SerializeField]
    public List<string> commands;

    [SerializeField]
    public int index;
    
    private Dictionary<string, int> labels;

    private DialogueBoxController dialogueBoxController;

    [SerializeField]
    public CharacterSheetController speakerSheetController;

    private List<string> choices;

    private Dictionary<string, int> rememberedChoices;

    void Start()
    {
        commands = new List<string>();
        choices = new List<string>();
        labels = new Dictionary<string, int>();
        dialogueBoxController = GetComponent<DialogueBoxController>();
        rememberedChoices = new Dictionary<string, int>();

        if (dialogueBoxController == null)
        {
            Debug.LogError("dialogeBoxController could not be initialized...");
            Destroy(this);
            return;
        }
    
        if(defaultDialogueScript != null)
        {
            LoadScript(defaultDialogueScript);
        }

        if(speakerSheetController == null)
        {
            Debug.LogWarning("No speaker sheet controller set!");
        }


    }

    private void Update()
    {
        if (!dialogueBoxController.IsWaitingForInput())
        {
            ExecuteScript();
        }
    }

    //Runs this dialogue script until interuppted by one of the commands
    //This can be caused by player input being waited for among other things as well
    public void ExecuteScript()
    {
        while(index < commands.Count)
        {
            bool executeAnother = executeOneCommand();

            if (!executeAnother)
            {
                break;
            }
        }
    }

    public void LoadScript(TextAsset textAsset)
    {
        Debug.Log("Loading script " + textAsset.name);

        commands = new List<string>();

        int index = 0;

        foreach(string line in textAsset.text.Split("\n"))
        {
            string sanitized = line.Substring(0, line.Length-1);//remove the trailing \n
            if(sanitized.Length > 0)
            {
                if (sanitized.EndsWith(":"))//Label
                {
                    labels[sanitized.Substring(0, sanitized.Length - 1)] = commands.Count;//When this label is jumped to it should go to the instruction below this line so add 1
                }
                else
                {
                    commands.Add(sanitized);
                    index++;
                }
            }
        }

    }

    //Executes a single command.
    //Returns true if another command should be executed or if the dialogue script should pause until instructed to continue
    bool executeOneCommand()
    {
        if (index < 0 || index >= commands.Count)
        {
            return false;
        }
        string command = commands[index];

        //Increment index after reading the last command
        index++;

        string[] parts = command.Split(" ");
        parts[0] = parts[0].ToLower();//Make the command part lower case for easy parsing


        if (parts[0].Equals("dialogue"))
        {
            dialogueBoxController.ShowDialogue(concat(parts, 1), true);
            return false;
        }
        else if (parts[0].Equals("choice"))
        {
            choices.Add(concat(parts, 1));
            return true;
        }
        else if (parts[0].Equals("makechoice"))
        {

            string choiceText = concat(parts, 1);

            dialogueBoxController.ShowDialogue(choiceText, true);

            for (int i = 0; i < choices.Count; i++)
            {
                if (i >= dialogueBoxController.CurrentOptions.Length)
                {
                    Debug.LogWarning("Tried to show more dialogue options than we have room for...");
                    break;
                }

                dialogueBoxController.CurrentOptions[i] = choices[i];

            }

            choices.Clear();

            return false;
        }
        else if (parts[0].Equals("goto"))
        {
            string label = parts[1];

            if (labels.ContainsKey(label))
            {
                index = labels[label];
            }
            else
            {
                Debug.LogWarning("Tried to jump to a label called " + label + " but it doesnt exist!");
            }
            return true;
        }
        else if (parts[0].Equals("remember"))
        {
            string saveName = parts[1];

            int Selected = dialogueBoxController.SelectedOption;

            rememberedChoices[saveName] = Selected;

            Debug.Log("Saving choice " + saveName + " as " + Selected);
            return true;
        }
        else if (parts[0].Equals("recall"))
        {
            string saveName = parts[1];

            if (rememberedChoices.ContainsKey(saveName))
            {
                dialogueBoxController.SelectedOption = rememberedChoices[saveName];
                Debug.Log("Recalled choice " + saveName + " as " + rememberedChoices[saveName]);
            }
            else
            {
                Debug.Log("Couldn't recall choice " + saveName + " as we don't have it saved!");
            }
            return true;
        }
        else if (parts[0].Equals("branch"))
        {
            string[] branchLabels = parts[1].Split(";");

            int Selected = dialogueBoxController.SelectedOption;

            if (Selected < 0 || Selected >= branchLabels.Length)
            {
                Debug.LogError("Branch had " + branchLabels.Length + " labels but selection was " + (Selected + 1));
            }
            else
            {
                string label = branchLabels[dialogueBoxController.SelectedOption];

                if (labels.ContainsKey(label))
                {
                    index = labels[label];
                }
                else
                {
                    Debug.LogWarning("Tried to jump to a label called " + label + " but it doesnt exist!");
                }

            }

            return true;
        }
        else if (parts[0].Equals("speaker"))
        {
            string newSpeakerScriptName = parts[1];

            CharacterInformation selected = null;

            foreach(CharacterInformation ci in characters)
            {
                if (ci.ScriptReferenceName.Equals(newSpeakerScriptName))
                {
                    selected = ci;
                    break;
                }
            }

            if(selected == null)
            {
                Debug.LogWarning("Could not switch speaker to " + newSpeakerScriptName + ", did you add their character information to the dialogue script?");
            }
            else
            {
                speakerSheetController.SetCharacter(selected);
            }

            return true;
            
        }

        Debug.LogWarning("Couldn't parse dialogue command: " + command);
        return true;
    }

    private static string concat(string[] parts, int indexFrom)
    {
        string concatted = "";
        for(int i = indexFrom;i < parts.Length; i++)
        {
            concatted += parts[i] + " ";
        }
        return concatted;
    }


}
