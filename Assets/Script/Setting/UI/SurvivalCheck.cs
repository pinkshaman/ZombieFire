using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalCheck : MonoBehaviour
{
    public Text rankText;
    public Button showRank;

    public void Start()
    {
        SetRank();
        showRank.onClick.AddListener(ShowLeaderBoard);
    }
    public void SetRank()
    {
        var rank = SurvivalMode.Instance.survivalModeProgess;
        rankText.text = $"Score: {rank.highestScore} / Wave: {rank.highestWave}";
    }
    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
}
