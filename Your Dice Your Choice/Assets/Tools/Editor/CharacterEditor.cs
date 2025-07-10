using Assets.Scripts.CharacterPrefab;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    private Character _character;
    private Editor _settingsEditor;

    public void OnEnable()
    {
        _character = (Character)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        DrawSettingsEditor(_character.CharacterData, _character.OnSettingsUpdate,
            ref _character.SettingsFoldout, ref _settingsEditor);
    }

    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdate,
        ref bool foldout, ref Editor editor)
    {
        if (settings == null) return;

        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (foldout)
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();

                if (check.changed)
                    onSettingsUpdate?.Invoke();
            }
        }
    }
}
