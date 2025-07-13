using System;
using System.Collections.Generic;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.CharacterPrefab.CharacterBody;
using Assets.Scripts.FieldPrefab;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance { get; private set; }

        public List<GameObject> InteractibleCharacters { get; private set; }

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
        /// Sets the interactible enmey characters.
        /// </summary>
        /// <param name="characterFieldIndexOrigin"></param>
        /// <param name="actionDirections"></param>
        /// <param name="directionRange"></param>
        public void SetInteractibleEnemyCharacters(Vector2Int characterFieldIndexOrigin, Vector2Int[] actionDirections, int directionRange)
        {
            InteractibleCharacters = new();

            foreach (Vector2Int actionDirection in actionDirections)
            {
                var fieldIndex = characterFieldIndexOrigin;
                fieldIndex += actionDirection * directionRange;

                if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                    continue;
                if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                    continue;
                if (EnemyCharacter(characterFieldIndexOrigin, actionDirection, directionRange) == null)
                    continue;

                var enemyObject = EnemyCharacter(characterFieldIndexOrigin, actionDirection, directionRange);
                InteractibleCharacters.Add(enemyObject);
            }
        }

        /// <summary>
        /// Shows the interactible characters.
        /// </summary>
        public void ShowInteractibleCharacters()
        {
            foreach (var characterObject in InteractibleCharacters)
            {
                var borderColor = characterObject.GetComponent<CharacterBorderColor>();
                var character = characterObject.GetComponent<Character>();
                character.SetEnabled(borderColor, true);

                var mouseEvent = characterObject.GetComponent<Character>().CharacterMouseEvent;
                mouseEvent.SetIsBeingAttacked(true);
            }
        }

        /// <summary>
        /// Checks enemy between character and target field.
        /// </summary>
        /// <param name="characterFieldIndexOrigin"></param>
        /// <param name="actionDirection"></param>
        /// <param name="directionRange"></param>
        /// <returns></returns>
        private GameObject EnemyCharacter(Vector2Int characterFieldIndexOrigin, Vector2Int actionDirection, int directionRange)
        {
            var enemyObject = new GameObject();

            for (int i = 1; i <= directionRange; i++)
            {
                var fieldIndex = characterFieldIndexOrigin;
                fieldIndex += actionDirection * i;

                if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                    continue;
                if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                    continue;

                var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].GetComponent<Field>();

                if (field.EnemyObject(TurnManager.Instance.Turn) != null)
                    return field.CharacterObject;
            }

            return null;
        }

        /// <summary>
        /// Deactivates the interactible characters.
        /// </summary>
        public void DeactivateCharacters()
        {
            if (InteractibleCharacters == null || InteractibleCharacters.Count == 0)
                return;

            foreach (var characterObject in InteractibleCharacters)
            {
                var borderColor = characterObject.GetComponent<CharacterBorderColor>();
                var character = characterObject.GetComponent<Character>();
                character.SetEnabled(borderColor, false);
            }

            InteractibleCharacters.Clear();
        }

    }
}
