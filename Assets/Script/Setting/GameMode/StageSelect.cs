using System.Collections;
using System.Collections.Generic;
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

    public void SetArena(Arena arena)
    {
        arenaImage.sprite = arena.areraImage;
        arenaName.text = $"ARENA {arena.areaNumber}";
    }
    public void LoadStageSelectData(List<Stage> stageList, List<StageProgess> stageProgessList,Arena arena)
    {
        SetArena(arena);
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

    }

    public void InitButton(UnityAction<int> changeArenaAction)
    {
        OnChangeArena = changeArenaAction;
        nextArena.onClick.AddListener(() => OnChangeArena?.Invoke(1));
        prevArena.onClick.AddListener(() => OnChangeArena?.Invoke(-1));
    }

    public void UpdateButtonState(int currentArena, int maxArena)
    {
        nextArena.interactable = (currentArena < maxArena);
        prevArena.interactable = (currentArena > 1);
    }



}
