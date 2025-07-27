using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject Character;
    public Vector2Int Index { get; private set; }
    public GameObject Obstacle { get; private set; }


    /// <summary>
    /// Sets the field index and the name.
    /// </summary>
    /// <param name="index"></param>
    public void SetIndex(Vector2Int index)
    {
        this.Index = index;
        gameObject.name = $"Field {index.x} / {index.y}";
    }
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

    /// <summary>
    /// Gets the enemy character.
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    public GameObject EnemyObject(PlayerType currentPlayer)
    {
        if (!IsAnyObstacleOnField())
            return null;

        if (!Obstacle.CompareTag("Character"))
            return null;

        var character = Obstacle.GetComponent<Character>();
        if (character.Player.PlayerType == currentPlayer)
            return null;

        return Obstacle;
    }

    /// <summary>
    /// Is any obstacle on the field?
    /// </summary>
    /// <returns></returns>
    public bool IsAnyObstacleOnField()
    {
        if (Obstacle == null)
            return false;

        return true;
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

