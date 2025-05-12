using UnityEngine;
using Assets.Scripts.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    
    public GameObject[] Character { get; private set; }
    
    public Player CurrentPlayer {  get; private set; }

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
    /// Start method.
    /// </summary>
    private void Start()
    {
        
    }

    

    /// <summary>
    /// Initializes the array Fields.
    /// </summary>
    public void InitializeCharacter()
    {
        Character = new GameObject[LevelManager.Instance.Data.CharacterAmount];
    }

    /// <summary>
    /// References the action for each DicePanel.
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < Character.Length; i++)
        {
           CharacterPanel[i].GetComponent<CharacterPanel>().SetAction();
        }
    }

    private void StartMatch()
    {

    }
}

