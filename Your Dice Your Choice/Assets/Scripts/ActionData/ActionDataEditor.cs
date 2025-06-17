using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Action
{
    [CustomEditor(typeof(ActionData))]
    public class ActionDataEditor : Editor
    {
        private ActionData _actionData;

        public override void OnInspectorGUI()
        {
            EditorStyles.textField.wordWrap = true;

           _actionData = (ActionData)target;

            Draw();
        }

        /// <summary>
        /// Draw the inspector.
        /// </summary>
        private void Draw()
        {
            _actionData.ActionType = (ActionType)EditorGUILayout.EnumPopup("Action Type", _actionData.ActionType);

            switch ((_actionData.ActionType))
            {
                case ActionType.None:
                    break;

                case ActionType.Move:
                    DrawMoveFields();
                    break;

                case ActionType.Attack:
                    _actionData.Description = "Move the dice over it to get more information";
                    DrawDescriptionFields();
                    break;

                case ActionType.Defend:
                    _actionData.Description = "Move the dice over it to get more information";
                    DrawDescriptionFields();
                    break;
            }
        }

        /// <summary>
        /// Draw the fields for the action 'Move'.
        /// </summary>
        private void DrawMoveFields()
        {
            _actionData.AllowedTile = (AllowedTile)EditorGUILayout.EnumPopup("Allowed Tile", _actionData.AllowedTile);
            _actionData.AllowedDiceNumber = (AllowedDiceNumber)EditorGUILayout.EnumPopup("Allowed Dice Number", _actionData.AllowedDiceNumber);
            _actionData.Direction = (Direction)EditorGUILayout.EnumPopup("Direction", _actionData.Direction);

            if (_actionData.AllowedTile != AllowedTile.None &&
               _actionData.AllowedDiceNumber != AllowedDiceNumber.None &&
               _actionData.Direction != Direction.None)
            {
                _actionData.MovementKey = EnumConverter.CreateEnumFrom(EnumsList());

                EditorGUILayout.TextField("Movement Key", _actionData.MovementKey.ToString());

                _actionData.Description = MovementType.Description[_actionData.MovementKey];

                DrawDescriptionFields();
            }
        }

        /// <summary>
        /// Enums list.
        /// </summary>
        /// <returns></returns>
        private List<object> EnumsList()
        {
            List<object> enumsList = new List<object>
            {
                _actionData.ActionType,
                _actionData.AllowedTile,
                _actionData.AllowedDiceNumber,
                _actionData.Direction
            };

            return enumsList;
        }

        /// <summary>
        /// Draw the fields for the description.
        /// </summary>
        private void DrawDescriptionFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Description");
            EditorGUILayout.TextArea(_actionData.Description);
            EditorGUILayout.EndHorizontal();
        }
    }
}
