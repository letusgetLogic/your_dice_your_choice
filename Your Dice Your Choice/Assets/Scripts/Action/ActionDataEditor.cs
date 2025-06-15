using System;
using System.Collections.Generic;
using NUnit.Framework;
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
            _actionData = (ActionData)target;

            Draw();
        }

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
                    break;
                case ActionType.Defend:
                    break;
            }
        }

        private void DrawMoveFields()
        {
            _actionData.AllowedTile = (AllowedTile)EditorGUILayout.EnumPopup("Allowed Tile", _actionData.AllowedTile);
            _actionData.AllowedDiceNumber = (AllowedDiceNumber)EditorGUILayout.EnumPopup("Allowed Dice Number", _actionData.AllowedDiceNumber);
            _actionData.Direction = (Direction)EditorGUILayout.EnumPopup("Direction", _actionData.Direction);

            if (_actionData.AllowedTile != AllowedTile.None &&
               _actionData.AllowedDiceNumber != AllowedDiceNumber.None &&
               _actionData.Direction != Direction.None)
            { 
                DrawKeyAndDescription(); 
            }
        }

        private void DrawKeyAndDescription()
        {
            List<object> enumList = new List<object>
            {
                _actionData.ActionType,
                _actionData.AllowedTile,
                _actionData.AllowedDiceNumber,
                _actionData.Direction
            };

            _actionData.MovementKey = EnumConverter.CreateEnumFrom(enumList);
            EditorGUILayout.EnumPopup("Movement Key", _actionData.MovementKey);

            _actionData.Description = MovementType.Description[_actionData.MovementKey];
            EditorGUILayout.PrefixLabel("Description");
            EditorGUILayout.TextArea(_actionData.Description);
        }

        private void DrawAttackFields()
        {

        }
        
        private void DrawDefendFields()
        {

        }

    }
}
