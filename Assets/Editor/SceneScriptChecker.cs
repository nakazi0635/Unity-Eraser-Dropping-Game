using UnityEditor;
using UnityEngine;

public class SceneScriptChecker : EditorWindow
{
    private Vector2 scrollPosition;
    private bool showAllComponents = true;
    private string searchKeyword = "";

    [MenuItem("Custom/Script Checker")]
    public static void ShowWindow()
    {
        SceneScriptChecker window = EditorWindow.GetWindow<SceneScriptChecker>();
        window.titleContent = new GUIContent("Script Checker");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Used Scripts", EditorStyles.boldLabel);

        GUILayout.Space(5f);

        showAllComponents = EditorGUILayout.Toggle("Show All Components", showAllComponents);

        GUILayout.Space(5f);

        searchKeyword = EditorGUILayout.TextField("Search", searchKeyword);

        GUILayout.Space(5f);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in sceneObjects)
        {
            Component[] components = gameObject.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component != null && !EditorUtility.IsPersistent(component))
                {
                    if (showAllComponents || IsUserScript(component))
                    {
                        DisplayComponentInfo(gameObject.name, component);
                    }
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private bool IsUserScript(Component component)
    {
        MonoBehaviour monoBehaviour = component as MonoBehaviour;
        if (monoBehaviour == null)
        {
            return false;
        }

        string scriptAssemblyName = monoBehaviour.GetType().Assembly.GetName().Name;
        return scriptAssemblyName == "Assembly-CSharp";
    }

    private void DisplayComponentInfo(string gameObjectName, Component component)
    {
        string objectName = "<b>" + gameObjectName + "</b>";
        string componentName = "<b>" + component.GetType().Name + "</b>";

        if (string.IsNullOrEmpty(searchKeyword) ||
            objectName.Contains(searchKeyword) ||
            componentName.Contains(searchKeyword))
        {
            EditorGUILayout.LabelField(objectName + " - " + componentName);

            SerializedObject serializedComponent = new SerializedObject(component);
            SerializedProperty iterator = serializedComponent.GetIterator();

            while (iterator.NextVisible(true))
            {
                if (iterator.propertyType != SerializedPropertyType.ObjectReference)
                {
                    EditorGUILayout.LabelField("    " + iterator.displayName + ": " + GetSerializedPropertyValue(iterator));
                }
            }

            serializedComponent.ApplyModifiedProperties();
        }
    }

    private string GetSerializedPropertyValue(SerializedProperty property)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                return property.intValue.ToString();
            case SerializedPropertyType.Boolean:
                return property.boolValue.ToString();
            case SerializedPropertyType.Float:
                return property.floatValue.ToString();
            case SerializedPropertyType.String:
                return property.stringValue;
            case SerializedPropertyType.Vector2:
                return property.vector2Value.ToString();
            case SerializedPropertyType.Vector3:
                return property.vector3Value.ToString();
            case SerializedPropertyType.Enum:
                return property.enumDisplayNames[property.enumValueIndex];
            default:
                return "";
        }
    }
}
