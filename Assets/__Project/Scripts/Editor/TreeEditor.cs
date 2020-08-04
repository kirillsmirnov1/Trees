using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeController))]
public class TreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TreeController tree = (TreeController)target;

        if(GUILayout.Button("Grow some branches"))
        {
            tree.GenerateNewBranches();
        }

        if(GUILayout.Button("Grow tree"))
        {
            tree.GrowFullTree();
        }

        if(GUILayout.Button("Reset tree"))
        {
            tree.ResetTree();
        }
    }
}
