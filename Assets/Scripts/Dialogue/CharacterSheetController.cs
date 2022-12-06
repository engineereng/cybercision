using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetController : MonoBehaviour
{

    private CharacterSheetSettings settings;

    [SerializeField]
    public CharacterInformation currentCharacter;

    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponent<CharacterSheetSettings>();
    
        if (settings == null)
        {
            Debug.LogError("CharacterSheetController failed to initialize! No settings found...");
            Destroy(this);
            return;
        }

        if (!settings.IsValid())
        {
            Debug.LogError("Invalid settings found......");
            Destroy(this);
            return;
        }
        
        UpdateInformation();
    }

    public void SetCharacter(CharacterInformation character)
    {
        currentCharacter = character;
        UpdateInformation();
    }

    private void UpdateInformation()
    {
        settings.CharacterImage.sprite = currentCharacter.CharacterImage;
        settings.CharacterNameText.text = currentCharacter.CharacterName;
    }
    
}
