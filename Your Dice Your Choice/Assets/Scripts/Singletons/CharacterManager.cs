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
        public void SetInteractibleEnemyCharacters(Vector2Int characterFieldIndexOrigin, 
                                                    Vector2Int[] actionDirections, 
                                                    int directionRange)
        {
            if (InteractibleCharacters != null)
            {
                DeactivateCharacters();
            }

            InteractibleCharacters = new();

            foreach (Vector2Int actionDirection in actionDirections)
            {
                var fieldIndex = characterFieldIndexOrigin;
                fieldIndex += actionDirection * directionRange;

                if (FieldManager.Instance.IsTargetOutOfRange(fieldIndex))
                    continue;

                if (EnemyCharacter(characterFieldIndexOrigin, actionDirection, 
                                    directionRange) == null)
                    continue;

                var enemyObject = EnemyCharacter(characterFieldIndexOrigin, 
                                                actionDirection, directionRange);
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
                character.SetComponentEnabled(borderColor, true);

                var beingAttacked = 
                    characterObject.GetComponent<Character>().CharacterBeingAttacked;
                character.SetComponentEnabled(beingAttacked, true);
            }
        }

        /// <summary>
        /// Checks enemy between character and target field.
        /// </summary>
        /// <param name="characterFieldIndexOrigin"></param>
        /// <param name="actionDirection"></param>
        /// <param name="directionRange"></param>
        /// <returns></returns>
        private GameObject EnemyCharacter(Vector2Int characterFieldIndexOrigin, 
                                            Vector2Int actionDirection, 
                                            int directionRange)
        {
            for (int i = 1; i <= directionRange; i++)
            {
                var fieldIndex = characterFieldIndexOrigin;
                fieldIndex += actionDirection * i;

                if (FieldManager.Instance.IsTargetOutOfRange(fieldIndex))
                    continue;

                var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].
                    GetComponent<Field>();

                if (field.EnemyObject(TurnManager.Instance.Turn) != null)
                    return field.CharacterObject;
            }

            return null;
        }

        /// <summary>
        /// Deactivates the interactable characters and sets the InteractableCharacters to null.
        /// </summary>
        public void DeactivateCharacters()
        {
            if (InteractibleCharacters == null)
                return;

            if (InteractibleCharacters.Count == 0)
            {
                 InteractibleCharacters = null;
                return;
            }

            foreach (var characterObject in InteractibleCharacters)
            {
                var character = characterObject.GetComponent<Character>();
                var borderColor = characterObject.GetComponent<CharacterBorderColor>();
                character.SetComponentEnabled(borderColor, false);

                var beingAttacked =
                    characterObject.GetComponent<Character>().CharacterBeingAttacked;
                character.SetComponentEnabled(beingAttacked, false);
            }

            InteractibleCharacters = null;
        }

    }
}
