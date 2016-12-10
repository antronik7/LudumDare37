using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelSetter : EditorWindow {

    [MenuItem("Level Editor/New Level")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one. 
        EditorWindow.GetWindow(typeof(LevelSetter));
    }

    int scaleX;
    int scaleY;

    void OnGUI()
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 50;
        scaleX = EditorGUILayout.IntField("Largeur", scaleX, GUILayout.Width(80));
        scaleY = EditorGUILayout.IntField("Hauteur", scaleY, GUILayout.Width(80));

        if (GUILayout.Button("Créer level"))
        {
            newLevel(scaleX, scaleY);
            this.Close();
        }
        EditorGUILayout.EndHorizontal();
    }


    static void newLevel(int scaleX, int scaleY)
    {
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        closeAllScene();

        GameObject raycastBackgroundPrefab = Resources.Load("RaycastBackground") as GameObject;
        GameObject raycastBackground = (GameObject)Instantiate(raycastBackgroundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        raycastBackground.name = "RaycastBackground";
        raycastBackground.transform.localScale = new Vector3(scaleX, scaleY, 0);

        GameObject raycastGlobalBackgroundPrefab = Resources.Load("RaycastBackground") as GameObject;
        GameObject raycastGlobalBackground = (GameObject)Instantiate(raycastBackgroundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        raycastGlobalBackground.name = "RaycastGlobalBackground";
        raycastGlobalBackground.transform.localScale = new Vector3(50, 50, 0);

        GameObject oneRoom = new GameObject();
        oneRoom.name = "OneRoom";
            
        EditorSceneManager.SaveScene(newScene);
    }

    static void closeAllScene()
    {
        EditorSceneManager.SaveOpenScenes();
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            EditorSceneManager.CloseScene(EditorSceneManager.GetSceneAt(i), true);
        }
    }
}
