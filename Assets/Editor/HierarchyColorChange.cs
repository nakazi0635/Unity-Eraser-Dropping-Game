using UnityEditor;
using UnityEngine;

// 起動時に処理させる
[InitializeOnLoad]
// "internal”他のアセンブリから参照されない
internal static class HierarchyColorChange
{

    private static readonly Color COLOR = new Color(100, 100, 100, 0.08f);    // 背景の色
    private static readonly int scene_text_height = 4;    // Hierarchyにあるシーン表記の高さ
    private static readonly int One_text_height = 16;    // Hierarchyにある各オブジェクト表記の高さ

    static HierarchyColorChange()
    {
        // EditorApplication.hierarchyWindowItemOnGUIは、HierarchyWindowにアクションがあるたびに呼ばれる
        // EditorApplication.hierarchyWindowItemOnGUIに、拡張メソッドを追加する
        EditorApplication.hierarchyWindowItemOnGUI += ColorChangeHierarchy;
        // EditorApplication.projectWindowItemOnGUI += ProjectColorChange;
        EditorApplication.projectWindowItemOnGUI += ColorChangeProject;
    }

    private static void ColorChangeHierarchy(int instanceID, Rect rect)
    {
        // rectの位置とオフセットから現在の行のインデックスを計算する
        // シーンの表記は非適用にするため、rect.yを下げる
        // 上から何番目に表記されているかインデックス取得
        var index = (int)(rect.y - scene_text_height) / One_text_height;

        // インデックスが偶数の場合は、この行の背景を色付けしない
        if (index % 2 == 0) return;

        // 矩形型の調整
        rect.xMax += 16;
        // 指定された位置とサイズに色で塗りつぶした矩形を描画
        EditorGUI.DrawRect(rect, COLOR);
    }

    // ProjectウィンドウにもColorChangeHierarchyと同等の機能を適用
    private static void ColorChangeProject( string guid, Rect rect )
    {
        var index = (int)( rect.y - scene_text_height) / One_text_height;
        
        if ( index % 2 == 0 )return;

        rect.xMax += 16;
        EditorGUI.DrawRect(rect, COLOR);
    }
}