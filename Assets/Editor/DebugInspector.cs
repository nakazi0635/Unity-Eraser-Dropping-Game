using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugInspector : EditorWindow
{
    Vector2 scrollPosition = Vector2.zero; // スクロールの位置を管理するための変数
    string search = string.Empty; // 検索文字列を管理するための変数
    bool searchScriptsOnly = false; // スクリプトのみを検索するかどうかを管理するためのフラグ

    // ウィンドウを表示するためのメニュー項目
    [MenuItem("Window/Debug Inspector")]
    public static void ShowWindow()
    {
        GetWindow<DebugInspector>("Debug Inspector"); // ウィンドウを開く
    }

    void OnGUI()
    {
        // 検索バーのUIを作成
        GUILayout.BeginHorizontal("HelpBox");
        GUILayout.Label("Search: ", EditorStyles.boldLabel);
        search = EditorGUILayout.TextField(search);
        searchScriptsOnly = EditorGUILayout.ToggleLeft("Scripts Only", searchScriptsOnly);
        GUILayout.EndHorizontal();

        // オブジェクトのリストをスクロール可能なビューで表示
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            // 検索文字列が含まれないオブジェクトはスキップ
            if (!string.IsNullOrEmpty(search) && !go.name.ToLower().Contains(search.ToLower()))
                continue;

            // "Scripts Only"が選択されていて、オブジェクトが自作スクリプトを持っていない場合はスキップ
            if (searchScriptsOnly && !HasUserScript(go))
                continue;

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("Object Name: " + go.name, EditorStyles.boldLabel);
            foreach (Component comp in go.GetComponents<Component>())
            {
                // "Scripts Only"が選択されていて、コンポーネントが自作のスクリプトでない場合はスキップ
                if (searchScriptsOnly && !(comp is MonoBehaviour) || (comp.GetType().Namespace != null && comp.GetType().Namespace.StartsWith("UnityEngine")))
                    continue;

                EditorGUILayout.LabelField("Script Name: " + comp.GetType().Name, EditorStyles.boldLabel);

                SerializedObject so = new SerializedObject(comp);
                SerializedProperty prop = so.GetIterator();
                while (prop.NextVisible(true))
                {
                    // m_Scriptのプロパティはスキップ
                    if (prop.name == "m_Script") continue;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Variable Name: " + prop.name);
                    EditorGUILayout.LabelField("Value: " + GetPropertyValue(prop));
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }

    // オブジェクトが自作スクリプトを持っているかどうかをチェックするためのメソッド
    bool HasUserScript(GameObject go)
    {
        foreach (Component comp in go.GetComponents<Component>())
        {
            // コンポーネントが自作のスクリプトでない場合はスキップ
            if (!(comp is MonoBehaviour) || (comp.GetType().Namespace != null && comp.GetType().Namespace.StartsWith("UnityEngine")))
                continue;
            
            return true; // 自作のスクリプトが見つかったら true を返す
        }

        return false; // 自作のスクリプトが見つからなかったら false を返す
    }

    // SerializedProperty の値を文字列で取得するためのメソッド
    string GetPropertyValue(SerializedProperty prop)
    {
        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                return prop.intValue.ToString();
            case SerializedPropertyType.Boolean:
                return prop.boolValue.ToString();
            case SerializedPropertyType.Float:
                return prop.floatValue.ToString();
            case SerializedPropertyType.String:
                return prop.stringValue;
            case SerializedPropertyType.Color:
                return prop.colorValue.ToString();
            case SerializedPropertyType.ObjectReference:
                return prop.objectReferenceValue ? prop.objectReferenceValue.ToString() : "null";
            // 必要に応じて他の型も追加する
            default:
                return "<unknown>"; // 未知の型は<unknown>を返す
        }
    }
}
