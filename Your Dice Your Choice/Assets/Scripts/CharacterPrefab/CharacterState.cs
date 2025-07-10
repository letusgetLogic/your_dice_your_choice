using System;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private GameObject _normalState;
    [SerializeField] private GameObject _eyeDownState;
    
    [SerializeField] private GameObject _downEyeAt1; // @
    [SerializeField] private GameObject _downEyeAt2; // @ 
    
    [SerializeField] private GameObject _downEyeX1; // X
    [SerializeField] private GameObject _downEyeX2; // X

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotateSpeed = 1000f;

    private enum BattleState 
    { 
        normal, // normal eyes in battle mode.
        downAct1, // @ @
        downAct2, // @ @ (rotate)
        downAct3, // X X 
        downAct4 // reserve
    }
    private BattleState _battleState;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        _normalState.SetActive(true);
        _eyeDownState.SetActive(false);
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
            case BattleState.normal:
                return;

            case BattleState.downAct1:
                return;

            case BattleState.downAct2:
                RotateDownEyes(new GameObject[]
                {
                    _downEyeAt1,
                    _downEyeAt2,
                });
                return;

            case BattleState.downAct3:
                return;

            case BattleState.downAct4:
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
            case BattleState.normal:
                _normalState.SetActive(true);
                _eyeDownState.SetActive(false);
                return;


            case BattleState.downAct1:
            case BattleState.downAct2:
                _normalState.SetActive(false);
                _eyeDownState.SetActive(true);

                _downEyeX1.SetActive(false);
                _downEyeX2.SetActive(false);

                _downEyeAt1.SetActive(true);
                _downEyeAt2.SetActive(true);
                return;


            case BattleState.downAct3:
                _normalState.SetActive(false);
                _eyeDownState.SetActive(true);

                _downEyeAt1.SetActive(false);
                _downEyeAt2.SetActive(false);

                _downEyeX1.SetActive(true);
                _downEyeX2.SetActive(true);
                return;


            case BattleState.downAct4:
                return;


            default:
                _normalState.SetActive(true);
                _eyeDownState.SetActive(false);
                return;
        }
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
