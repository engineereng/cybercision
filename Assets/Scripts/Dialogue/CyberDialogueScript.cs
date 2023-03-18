using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
            if(line.Length == 0)
            {
                continue;
            }
            string sanitized = line;
            if (sanitized.EndsWith("\r"))
            {
                sanitized = line.Substring(0, line.Length - 1);//remove the trailing \n
            }
            if(sanitized.Length > 0)
            {
                if (sanitized.StartsWith(";"))
                {
                    continue;
                }
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
        else if (parts[0].Equals("load"))
        {
            string sceneName = concat(parts, 1);

            if(sceneName.Length > 0)
            {

                SaveGame(sceneName);

                Debug.Log("Loading scene " + sceneName);
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                
            }
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
        else if (parts[0].Equals("music"))
        {

            if (parts[1].Equals("play"))
            {
                string musicName = parts[2];

                AudioClip audioClip = null;

                foreach (AudioClip ac in dialogueBoxController.music)
                {
                    if (ac.name.Equals(musicName))
                    {
                        audioClip = ac;
                        break;
                    }
                }

                if (audioClip)
                {
                    dialogueBoxController.musicSource.clip = audioClip;
                    dialogueBoxController.musicSource.loop = true;
                    dialogueBoxController.musicSource.Play();
                    Debug.Log("Playing music " + audioClip.name);
                }
                else
                {
                    Debug.LogWarning("Couldn't find music named " + musicName + ", have you added it to the music load list?");
                }
            }else if (parts[1].Equals("stop"))
            {
                dialogueBoxController.musicSource.Stop();
            }

            return true;
        }

        Debug.LogWarning("Couldn't parse dialogue command: " + command);
        return true;
    }

    private void SaveGame(string nextScene)
    {
        Save save = new Save();

        save.SavedChoices = rememberedChoices;
        save.SceneToLoad = nextScene;

        string path = Application.persistentDataPath + "/gamesave.save";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Saved game to " + path);
    }

    private static string concat(string[] parts, int indexFrom)
    {
        string concatted = "";
        for(int i = indexFrom;i < parts.Length; i++)
        {
            concatted += parts[i] + " ";
        }
        return concatted.Substring(0, concatted.Length - 1);
    }


}
