using System;
using UnityEngine;

public class CharacterEye : MonoBehaviour
{
    public GameObject NormalState;
    public GameObject DownState;
    public GameObject DownEye1;
    public GameObject DownEye2;

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotateSpeed = 1000f;

    private GameObject[] _downEyes;
    private bool _isDown = false;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        NormalState.SetActive(true);
        DownState.SetActive(false);
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _downEyes = new GameObject[]
        {
            DownEye1,
            DownEye2,
        };
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (_isDown)
        {
            RotateDownEye();
        }
        
    }

    /// <summary>
    /// Sets the down state.
    /// </summary>
    public void SetDownState()
    {
        _isDown = true;
        NormalState.SetActive(false);
        DownState.SetActive(true);
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
