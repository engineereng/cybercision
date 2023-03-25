using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MinigameButtonTest : MonoBehaviour
{

    public void Win()
    {
        MinigameManager.GetManager().FinishMinigame(true);
    }

    public void Lose()
    {
        MinigameManager.GetManager().FinishMinigame(false);
    }

}
