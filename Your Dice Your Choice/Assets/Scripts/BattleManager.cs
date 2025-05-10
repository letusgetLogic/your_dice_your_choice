using UnityEngine;
using Assets.Scripts.Characters;
using System;
using System.Collections;
using System.Collections.Generic;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GameObject[] CharacterPanel;

    public GameObject[,] Fields { get; private set; }
    public GameObject[] Character { get; private set; }
    


    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Initializes the array Fields.
    /// </summary>
    public void InitializeCharacter()
    {
        Character = new GameObject[LevelManager.Instance.Data.CharacterAmount];
    }
    
    /// <summary>
    /// Initializes the array Fields.
    /// </summary>
    public void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }

    /// <summary>
    /// Initializes the array Character and references Character and CharacterPanel.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetCharacter(GameObject characterPrefab, int index)
    {
        Character[index] = characterPrefab;
        CharacterPanel[index].SetActive(true); 

        Character[index].GetComponent<Character>().SetPanel(CharacterPanel[index]);
        CharacterPanel[index].GetComponent<CharacterPanel>().SetCharacter(Character[index]);
        //CharacterPanel[index].GetComponent<CharacterPanel>().SetAction();
        //StartCoroutine(SetAction());

        var characterName = Character[index].name;
        var characterPanel = CharacterPanel[index].name;
    }

    /// <summary>
    /// References the action for each DicePanel.
    /// </summary>
    public void SetAction()
    {
        Debug.Log("BattleManager SetAction() Character.Length " + Character.Length);
        for (int i = 0; i < Character.Length; i++)
        {
            var character = Character[i].GetComponent<Character>();
            Debug.Log(character);
            //character.Panel.GetComponent<CharacterPanel>().SetAction();
            CharacterPanel[i].GetComponent<CharacterPanel>().SetAction();
            Debug.Log(i + CharacterPanel[i].name);
        }

        //foreach (var panel in CharacterPanel)
        //{
        //    panel.GetComponent<CharacterPanel>().SetAction();
        //}

        //yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Initializes the array Character.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject field, int j, int i)
    {
        Fields[j, i] = field;
    }

    /// <summary>
    /// Hides all chracter panel.
    /// </summary>
    public void HideAllPanel()
    {
        foreach (GameObject panel in CharacterPanel)
        {
            panel.gameObject.SetActive(false);
        }
    }
}

