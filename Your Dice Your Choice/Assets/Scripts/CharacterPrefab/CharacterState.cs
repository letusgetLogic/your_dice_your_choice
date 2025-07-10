using System;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private GameObject _battleEyes;
    [SerializeField] private GameObject _downEyes;
    
    [SerializeField] private GameObject _downEyeAt1; // @
    [SerializeField] private GameObject _downEyeAt2; // @ 
    
    [SerializeField] private GameObject _downEyeX1; // X
    [SerializeField] private GameObject _downEyeX2; // X

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotateSpeed = 1000f;

    private enum BattleState 
    { 
        Battle, // battle eyes.
        DownAct1, // @ @
        DownAct2, // @ @ (rotate)
        DownAct3, // X X 
        DownAct4 // reserve
    }
    private BattleState _battleState;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        SetBattleState();
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        UpdateState();
    }

    /// <summary>
    /// Updates the character state.
    /// </summary>
    private void UpdateState()
    {
        switch (_battleState)
        {
            case BattleState.Battle:
                return;

            case BattleState.DownAct1:
                return;

            case BattleState.DownAct2:
                RotateDownEyes(new GameObject[]
                {
                    _downEyeAt1,
                    _downEyeAt2,
                });
                return;

            case BattleState.DownAct3:
                return;

            case BattleState.DownAct4:
                return;

            default:
                return;
        }
    }

    /// <summary>
    /// Switchs the character state.
    /// </summary>
    private void SwitchState()
    {
        switch (_battleState)
        {
            case BattleState.Battle:
                _battleEyes.SetActive(true);
                _downEyes.SetActive(false);
                return;


            case BattleState.DownAct1:
            case BattleState.DownAct2:
                _battleEyes.SetActive(false);
                _downEyes.SetActive(true);

                _downEyeX1.SetActive(false);
                _downEyeX2.SetActive(false);

                _downEyeAt1.SetActive(true);
                _downEyeAt2.SetActive(true);
                return;


            case BattleState.DownAct3:
                _battleEyes.SetActive(false);
                _downEyes.SetActive(true);

                _downEyeAt1.SetActive(false);
                _downEyeAt2.SetActive(false);

                _downEyeX1.SetActive(true);
                _downEyeX2.SetActive(true);
                return;


            case BattleState.DownAct4:
                return;


            default:
                _battleEyes.SetActive(true);
                _downEyes.SetActive(false);
                return;
        }
    }

    /// <summary>
    /// Sets the battle state.
    /// </summary>
    public void SetBattleState()
    {
        _battleState = BattleState.Battle;
        SwitchState();
    }

    /// <summary>
    /// Sets the down state.
    /// </summary>
    public void SetDownState()
    {
        int random = new System.Random().Next(1, Enum.GetNames(typeof(BattleState)).Length);

        _battleState = (BattleState)random;
        SwitchState();
    }

    /// <summary>
    /// Rotates the down eyes.
    /// </summary>
    private void RotateDownEyes(GameObject[] downEyes)
    {
        foreach (var item in downEyes)
        {
            item.transform.Rotate(_rotation * _rotateSpeed * Time.deltaTime);
        }
    }
}
