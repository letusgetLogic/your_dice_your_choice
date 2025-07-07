using System;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public GameObject NormalState;
    public GameObject EyeDownState;
    
    public GameObject DownEyeAt1; // @
    public GameObject DownEyeAt2; // @ 

    public GameObject DownEyeX1; // X
    public GameObject DownEyeX2; // X

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotateSpeed = 1000f;

    enum BattleState { normal, downAct1, downAct2, downAct3, downAct4 }
    private BattleState _battleState;

    private GameObject[] _downEyes;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        NormalState.SetActive(true);
        EyeDownState.SetActive(false);
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _downEyes = new GameObject[]
        {
            DownEyeAt1,
            DownEyeAt2,
        };
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
                RotateDownEye();
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
                NormalState.SetActive(true);
                EyeDownState.SetActive(false);
                return;


            case BattleState.downAct1:
            case BattleState.downAct2:
                NormalState.SetActive(false);
                EyeDownState.SetActive(true);

                DownEyeX1.SetActive(false);
                DownEyeX2.SetActive(false);

                DownEyeAt1.SetActive(true);
                DownEyeAt2.SetActive(true);
                return;


            case BattleState.downAct3:
                NormalState.SetActive(false);
                EyeDownState.SetActive(true);

                DownEyeAt1.SetActive(false);
                DownEyeAt2.SetActive(false);

                DownEyeX1.SetActive(true);
                DownEyeX2.SetActive(true);
                return;


            case BattleState.downAct4:
                return;


            default:
                NormalState.SetActive(true);
                EyeDownState.SetActive(false);
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
    private void RotateDownEye()
    {
        foreach (var item in _downEyes)
        {
            item.transform.Rotate(_rotation * _rotateSpeed * Time.deltaTime);
        }
    }
}
