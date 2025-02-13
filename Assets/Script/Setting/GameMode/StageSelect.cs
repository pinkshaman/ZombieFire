using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Button nextArena;
    public Button prevArena;
    public Image arenaImage;
    public Text arenaName;
    public List<StageUi> stageUiList;
    public UnityAction<int> OnChangeArena;
    private Arena currentArena;
    private ArenaProgess progessArena;
    private int choosedArena = 1;
    private int maxArena;

    public void Start()
    {
        maxArena = StageGameMode.Instance.ReturnArenaList();
        SetDataArena();
        UpdateButtonState();
    }
    public void SetDataArena()
    { 
        currentArena = StageGameMode.Instance.ReturnArena(choosedArena);
        progessArena = StageGameMode.Instance.ReturnProgessArena(choosedArena);
        if(progessArena.arenaNumber ==1)
        {
            progessArena.isActiveArena = true;
        }
        StageGameMode.Instance.currentArenaLoad = choosedArena;
        LoadStageSelectData(currentArena.stageList.stageLists, progessArena.stageProgessList.stageProgessLists);
        SetUiArena(currentArena);
        
    }
    public void SetUiArena(Arena arena)
    {
        arenaImage.sprite = arena.areraImage;
        arenaName.text = $"ARENA {arena.areaNumber}";
    }
    public void LoadStageSelectData(List<Stage> stageList, List<StageProgess> stageProgessList)
    {
        for (int i = 0; i < stageUiList.Count; i++)
        {
            if (i < stageList.Count)
            {
                stageProgessList[i].isCanPlay = (i == 0);
                if (i > 0 && stageProgessList[i - 1].isComplete)
                {
                    stageProgessList[i].isCanPlay = true;

                }
                stageUiList[i].SetData(stageList[i], stageProgessList[i]);
                stageUiList[i].gameObject.SetActive(true);
                
            }
            else
            {
                stageUiList[i].gameObject.SetActive(false);
            }
        }
        CheckAndUnlockNextArena(stageProgessList);
    }

    public void InitButton(UnityAction<int> changeArenaAction)
    {
        OnChangeArena = changeArenaAction;
        nextArena.onClick.AddListener(() => OnChangeArena?.Invoke(1));
        prevArena.onClick.AddListener(() => OnChangeArena?.Invoke(-1));
    }
    public void UpdateButtonState()
    {
        nextArena.interactable = (choosedArena < maxArena);
        prevArena.interactable = (choosedArena > 1);
    }
    public bool CheckAndUnlockNextArena(List<StageProgess> stageProgessList)
    {
        bool allStagesCompleted = stageProgessList.All(stage => stage.isComplete);
        return allStagesCompleted;
    }
    public bool IsValidArena(int arena)
    {
        return arena >= 1 && arena <= maxArena;
    }
    public void ChangeArena(int index)
    {
        int newArena = choosedArena + index;
        if (IsValidArena(newArena))
        {
            choosedArena = newArena;
            SetDataArena();
        }
    }

}
