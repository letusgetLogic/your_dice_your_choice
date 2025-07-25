﻿using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2Int Index { get; private set; }
    public GameObject CharacterObject { get; private set; }

    private int _count = 0;

    /// <summary>
    /// Sets the field index and the name.
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(Vector2Int index)
    {
        this.Index = index;
        gameObject.name = $"Field {index.x} / {index.y}";
    }

    /// <summary>
    /// OnTriggerEnter2D.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            CharacterObject = collision.transform.root.gameObject;
            _count++;
        }

        else if (collision.CompareTag("Obstacle"))
        {
            _count++;
        }
    }

    /// <summary>
    /// OnTriggerExit2D.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            CharacterObject = null;
            _count--;
        }

        else if (collision.CompareTag("Obstacle"))
        {
            _count--;
        }
    }

    /// <summary>
    /// Checks OnTrigger counter.
    /// </summary>
    /// <returns></returns>
    public bool IsAnyObstacleOnField()
    {
        if (_count == 0)
            return false;

        return true;
    }

    /// <summary>
    /// Gets the enemy character.
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    public GameObject EnemyObject(PlayerType currentPlayer)
    {
        if (CharacterObject == null)
            return null;

        var character = CharacterObject.GetComponent<Character>();

        if (character.PlayerType != currentPlayer)
        {
            return CharacterObject;
        }

        return null;
    }

    /// <summary>
    /// Sets the component enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public void SetComponentEnabled(Component component, bool value)
    {
        if (component is Behaviour behaviour)
        {
            behaviour.enabled = value;
        }
    }

    /// <summary>
    /// Set the character obejct null.
    /// </summary>
    public void SetCharacterObjectNull()
    {
        CharacterObject = null;
    }
}

