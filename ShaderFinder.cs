using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShaderFinder : EditorWindow
{
    Shader targetShader;
    Vector2 scroll;

    List<Material> foundMaterials = new List<Material>();

    [MenuItem("Tools/Find Materials Using Shader")]
    static void OpenWindow()
    {
        GetWindow<ShaderFinder>("Shader Finder");
    }

    void OnGUI()
    {
        targetShader = (Shader)EditorGUILayout.ObjectField("Shader to Find:", targetShader, typeof(Shader), false);

        if (GUILayout.Button("Search"))
        {
            FindMaterialsUsingShader();
        }

        if (foundMaterials.Count > 0)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var mat in foundMaterials)
            {
                EditorGUILayout.ObjectField(mat, typeof(Material), false);
            }
            EditorGUILayout.EndScrollView();
        }
    }

    void FindMaterialsUsingShader()
    {
        foundMaterials.Clear();

        // Get all renderers in the currently open scene
        Renderer[] renderersInScene = GameObject.FindObjectsOfType<Renderer>();

        HashSet<Material> uniqueMaterials = new HashSet<Material>();

        foreach (Renderer renderer in renderersInScene)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat != null && mat.shader == targetShader)
                {
                    uniqueMaterials.Add(mat);
                }
            }
        }

        foundMaterials.AddRange(uniqueMaterials);
    }
}