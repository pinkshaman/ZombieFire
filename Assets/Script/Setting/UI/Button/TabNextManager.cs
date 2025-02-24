using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabNextManager : MonoBehaviour
{
    public GameObject[] tabs;
    public Button nextButton;
    public Button prevButton;

    private int currentTab = 0;

    public void Start()
    {
        ShowTab(currentTab);
        nextButton.onClick.AddListener(NextTab);
        prevButton.onClick.AddListener(PrevTab);
    }

    public void ShowTab(int tabIndex)
    {
        if (tabIndex < 0 || tabIndex >= tabs.Length)
            return;

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[tabIndex].SetActive(true);
        currentTab = tabIndex;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        nextButton.interactable = currentTab < tabs.Length - 1;
        prevButton.interactable = currentTab > 0;
    }

    private void NextTab()
    {
        ShowTab(currentTab + 1);
    }

    private void PrevTab()
    {
        ShowTab(currentTab - 1);
    }
}
