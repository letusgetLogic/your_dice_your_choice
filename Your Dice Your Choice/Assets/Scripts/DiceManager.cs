using UnityEngine;

[ExecuteAlways]
public class DiceManager : MonoBehaviour
{
    // Standard Aufbau für Editor Menüs
    public GeneratorModel Settings { get => _settingsInstance; }

    [SerializeField]
    private GeneratorModel _settings;

    private GeneratorModel _settingsInstance;

    [HideInInspector]
    public bool SettingsFoldout;
    //

    private GeneratorModel _cubeSettings;

    public void GenerateDice()
    {

    }
}