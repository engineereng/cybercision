using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CyberDialogueScript : MonoBehaviour
{
    
    [SerializeField]
    public TextAsset defaultDialogueScript;

    [SerializeField]
    public List<string> commands;

    [SerializeField]
    public int index;
    
    private Dictionary<string, int> labels;

    private DialogueBoxController dialogueBoxController;

    private List<string> choices;

    void Start()
    {
        commands = new List<string>();
        choices = new List<string>();
        labels = new Dictionary<string, int>();
        dialogueBoxController = GetComponent<DialogueBoxController>();

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
                    labels[sanitized] = index + 1;//When this label is jumped to it should go to the instruction below this line so add 1
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
        if(index < 0 || index >= commands.Count)
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
                if(i >= dialogueBoxController.CurrentOptions.Length)
                {
                    Debug.LogWarning("Tried to show more dialogue options than we have room for...");
                    break;
                }

                dialogueBoxController.CurrentOptions[i] = choices[i];

            }

            choices.Clear();

            return false;
        }
        else if(parts[0].Equals("goto"))
        {
            string label = concat(parts, 1);

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
