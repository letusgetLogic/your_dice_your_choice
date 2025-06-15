using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Action
{
    [CustomEditor(typeof(ActionData))]
    public class ActionDataEditor : Editor
    {
        private ActionData _actionData;

        private bool
            _showActionSpecifics,
            _showActionDetails;

        public override void OnInspectorGUI()
        {
            _actionData = (ActionData)target;

            Draw();

            _showActionSpecifics = EditorGUILayout.Foldout(_showActionSpecifics, "Specifics", true);
            if (_showActionSpecifics) DrawActionSpecifics();
        }

        private void Draw()
        {
            _actionData.ActionType = (ActionType)EditorGUILayout.EnumPopup("Action Type", _actionData.ActionType);

            


            _actionData.ActionKey = (ActionKey)EditorGUILayout.EnumPopup(_actionData.ActionKey);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("AllowedDiceNumber", GUILayout.Width(70));
            _actionData.AllowedDiceNumber = (AllowedDiceNumber)EditorGUILayout.EnumPopup(_actionData.AllowedDiceNumber);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("AllowedTile", GUILayout.Width(70));
            _actionData.AllowedTile = (AllowedTile)EditorGUILayout.EnumPopup(_actionData.AllowedTile);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Direction", GUILayout.Width(70));
            _actionData.Direction = (Direction)EditorGUILayout.EnumPopup(_actionData.Direction);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description", GUILayout.Width(70));
            _actionData.Description = EditorGUILayout.TextArea(_actionData.Description);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawActionSpecifics()
        {
           

            _showActionDetails = EditorGUILayout.Foldout(_showActionDetails, "Details", true);
            if (!_showActionDetails) return;

            switch ((_actionData.ActionType))
            {
                case ActionType.None:
                    break;
                case ActionType.Move:
                    break;
                case ActionType.Attack:
                    break;
                case ActionType.Defend:
                    break;
            }
        }

        private void DrawMoveFields()
        {
            _actionData.
        }
        
        private void DrawAttackFields()
        {

        }
        
        private void DrawDefendFields()
        {

        }

    }
}
