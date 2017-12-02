using UnityEditor;

[CustomEditor(typeof(UI_Element))]
public class UI_ElementEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UI_Element[] uies = FindObjectsOfType<UI_Element>();
        foreach (UI_Element uie in uies)
        {
            uie.Start();
        }
    }
}
