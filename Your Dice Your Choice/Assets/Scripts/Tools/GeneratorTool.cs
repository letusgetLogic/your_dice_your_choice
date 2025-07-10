using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneratorToolWindow : EditorWindow
{
    private static GeneratorToolWindow _window;

    [MenuItem("Tools/Generation Tool")]
    public static void ShowWindow()
    {
        _window = GetWindow<GeneratorToolWindow>("GeneratorToolWindow");
        // Create / Show the window
        _window.Show();
    }

    private void CreateGUI()
    {
        VisualElement root = new VisualElement();
        rootVisualElement.Add(root);
        root.style.flexDirection = FlexDirection.Row;

        VisualElement verticalGroup0 = new VisualElement()
        {
            style =
            {
                width = 300,
                backgroundColor = Color.blue,
            }
        };

        VisualElement verticalGroup1 = new VisualElement()
        {
            style =
            {
                width = 300,
                backgroundColor = Color.red,
            }
        };

        Label text = new Label("Hallo");
        TextField inputField = new TextField();
        Label inputLabel = new Label();

        inputField.RegisterValueChangedCallback(
            (callback) =>
            {
                if (callback.newValue == "Hallo")
                {
                    inputLabel.text = "Hallo Back!";
                }

                inputLabel.text = inputField.text;
            });

        Button button = new Button();
        button.text = "Click";
        button.clicked += () =>
        {
            Label clickLabel = new Label("Button Clicked!");
            verticalGroup1.Add(clickLabel);
        };

        RadioButton radioButton = new RadioButton();
        radioButton.text = "Set A";
        radioButton.style.color = Color.black;

        root.Add(text);
        root.Add(verticalGroup0);
        root.Add(verticalGroup1);

        verticalGroup0.Add(inputField);
        verticalGroup0.Add(inputLabel);

        verticalGroup1.Add(button);
        verticalGroup1.Add(radioButton);
    }
}
