using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public ScratchShow scratchShow;


    public void ShowScratch()
    {
        scratchShow.ShowRandomScratch();
    }
}
 
