using UnityEditor;

[CustomEditor(typeof(UI_Layer))]
public class UI_LayerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UI_Layer uil = target as UI_Layer;
        uil.Start();
    }
}
