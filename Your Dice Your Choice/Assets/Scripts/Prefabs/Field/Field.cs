using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject Character;
    public Vector2Int Index { get; private set; }
    public GameObject Obstacle { get; private set; }

    //private int _count = 0;

    /// <summary>
    /// Sets the field index and the name.
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(Vector2Int index)
    {
        this.Index = index;
        gameObject.name = $"Field {index.x} / {index.y}";
    }

    ///// <summary>
    ///// OnTriggerEnter2D.
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Character"))
    //    {
    //        if (CharacterObject != null)
    //            return;

    //        CharacterObject = collision.transform.root.gameObject;
    //        Character = CharacterObject;
    //        _count++;
    //        ObstacleCount = _count;
    //    }

    //    else if (collision.CompareTag("Obstacle"))
    //    {
    //        _count++;
    //        ObstacleCount = _count;
    //    }
    //}

    public void SetObject(GameObject obstacle)
    {
        if (Obstacle != null)
            throw new System.Exception($"Field {Index.x} / {Index.y} has {obstacle.name}");

        Obstacle = obstacle;
        Character = Obstacle;
    }

    public void SetObjectNull()
    {
        if (Obstacle == null)
            return;

        Obstacle = null;
    }

    ///// <summary>
    ///// OnTriggerExit2D.
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Character") &&
    //        collision.gameObject.GetComponent<Character>().FieldIndex == In)
    //    {
    //        CharacterObject = null;
    //        Character = null;
    //        _count--;
    //        ObstacleCount = _count;
    //    }

    //    else if (collision.CompareTag("Obstacle"))
    //    {
    //        _count--;
    //        ObstacleCount = _count;
    //    }
    //}

    /// <summary>
    /// Checks OnTrigger counter.
    /// </summary>
    /// <returns></returns>
    public bool IsAnyObstacleOnField()
    {
        if (Obstacle == null)
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
        if (Obstacle == null)
            return null;

        if (!Obstacle.CompareTag("Character"))
            return null;

        var character = Obstacle.GetComponent<Character>();

        if (character.Player.PlayerType != currentPlayer)
        {
            return Obstacle;
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
}

