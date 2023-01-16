using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TypedText
{

    static Random random = new Random();

    TypedText Parent;

    string CurrentText;
    int CurrentIndex;
    float TypeSpeed;

    float TimeAccumulator;

    char FlavourCharacter;

    public TypedText(TypedText Parent = null)
    {
        this.Parent = Parent;
        CurrentText = "";
        TypeSpeed = 0.025f;
        CurrentIndex = 0;
    }

    public string GetVisibleText()
    {
        string Text = CurrentText.Substring(0, CurrentIndex);

        if (!IsFinished())
        {
            //Add a single character to the end to make things more flavourful, /shrug
            Text += FlavourCharacter;
        }

        return Text;
    }

    public bool IsFinished()
    {
        return CurrentIndex >= CurrentText.Length;
    }

    public void SetText(string NewText, float NewSpeed = 0.025f)
    {

        if (NewText == null || NewText.Equals(CurrentText))
        {
            return;
        }

        if (NewSpeed < 0.0f)
        {
            NewSpeed = 0.1f;
        }

        CurrentText = NewText;
        TypeSpeed = NewSpeed;
        CurrentIndex = 0;
        TimeAccumulator = 0;
    }

    public void Update(float Delta)
    {

        if(Parent != null && !Parent.IsFinished())
        {
            return;
        }

        if (CurrentIndex < CurrentText.Length)
        {
            TimeAccumulator += Delta;
            if (TimeAccumulator >= TypeSpeed)
            {
                TimeAccumulator -= TypeSpeed;
                CurrentIndex++;
                FlavourCharacter = GetRandomFlavourCharacter();
            }
        }
    }

    static char GetRandomFlavourCharacter()
    {
        char[] FlavourCharacters = new char[] { '!', '@', '#', '$', '%' };

        return FlavourCharacters[random.Next(0, FlavourCharacters.Length - 1)];
    }

}