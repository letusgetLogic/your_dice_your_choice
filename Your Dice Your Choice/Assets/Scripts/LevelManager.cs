using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] CharacterPanel;

    [SerializeField] private int _characterAmount;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        HideAllPanel();
        ShowPanelForCharacterOnField();
    }

    private void ShowPanelForCharacterOnField()
    {
        for (int i = 0; i < _characterAmount; i++)
        {
            CharacterPanel[i].SetActive(true);
        }
    }

    private void HideAllPanel()
    {
        foreach (var panel in CharacterPanel)
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
