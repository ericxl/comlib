#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Scene auto loader.
/// </summary>
/// <description>
/// This class adds a File > Scene Autoload menu containing options to select
/// a "master scene" enable it to be auto-loaded when the user presses play
/// in the editor. When enabled, the selected scene will be loaded on play,
/// then the original scene will be reloaded on stop.
///
/// Based on an idea on this thread:
/// http://forum.unity3d.com/threads/157502-Executing-first-scene-in-build-settings-when-pressing-play-button-in-editor
/// </description>
[InitializeOnLoad]
static class SceneAutoLoader
{
    // Static constructor binds a playmode-changed callback.
    // [InitializeOnLoad] above makes sure this gets executed.
    static SceneAutoLoader()
    {
        EditorApplication.playmodeStateChanged += OnPlayModeChanged;
        EditorApplication.update += OnUpdate;
    }

    private static int countdown = 0;
    private static bool shouldLoad = false;
    // Menu items to select the "master" scene and control whether or not to load it.
    [MenuItem("File/Scene Autoload/Select Master Scene...")]
    private static void SelectMasterScene()
    {
        var masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
        if (string.IsNullOrEmpty(masterScene)) return;
        MasterScene = masterScene;
        LoadMasterOnPlay = true;
    }

    [MenuItem("File/Scene Autoload/Load Master On Play", true)]
    private static bool ShowLoadMasterOnPlay()
    {
        return !LoadMasterOnPlay;
    }
    [MenuItem("File/Scene Autoload/Load Master On Play")]
    private static void EnableLoadMasterOnPlay()
    {
        LoadMasterOnPlay = true;
    }

    [MenuItem("File/Scene Autoload/Don't Load Master On Play", true)]
    private static bool ShowDontLoadMasterOnPlay()
    {
        return LoadMasterOnPlay;
    }
    [MenuItem("File/Scene Autoload/Don't Load Master On Play")]
    private static void DisableLoadMasterOnPlay()
    {
        LoadMasterOnPlay = false;
    }

    // Play mode change callback handles the scene load/reload.
    private static void OnPlayModeChanged()
    {
        if (!LoadMasterOnPlay)
        {
            return;
        }

        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            // User pressed play -- autoload master scene.
            PreviousScenePath = EditorSceneManager.GetActiveScene().path;
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                //if (!EditorApplication.OpenScene(MasterScene))
                //{
                //    Debug.LogError(string.Format("error: scene not found: {0}", MasterScene));
                //    EditorApplication.isPlaying = false;
                //}
                EditorSceneManager.OpenScene(MasterScene);
            }
            else
            {
                // User cancelled the save operation -- cancel play as well.
                EditorApplication.isPlaying = false;
            }
        }
        if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            // User pressed stop -- reload previous scene.
            //if (!EditorApplication.OpenScene(PreviousScenePath))
            //{
            //    Debug.LogError(string.Format("error: scene not found: {0}", PreviousScenePath));
            //}
            //EditorSceneManager.LoadScene(PreviousScenePath);
            //EditorSceneManager.LoadScene(PreviousScenePath);
            //EditorSceneManager.OpenScene("C:/repos/TapFortress/Assets/_Scenes/" + PreviousScenePath + ".unity");
            shouldLoad = true;
            countdown = 2;
        }
    }

    private static void OnUpdate()
    {
        if (shouldLoad) countdown--;
        if (countdown > 0 || !shouldLoad) return;
        try
        {
            EditorSceneManager.OpenScene(Path.GetDirectoryName(Application.dataPath) + "/" + PreviousScenePath);
        }
        finally
        {
            shouldLoad = false;
        }
    }

    // Properties are remembered as editor preferences.
    private const string cEditorPrefLoadMasterOnPlay = "SceneAutoLoader.LoadMasterOnPlay";
    private const string cEditorPrefMasterScene = "SceneAutoLoader.MasterScene";
    private const string cEditorPrefPreviousScene = "SceneAutoLoader.PreviousScenePath";

    private static bool LoadMasterOnPlay
    {
        get { return EditorPrefs.GetBool(cEditorPrefLoadMasterOnPlay + "_" + PlayerSettings.productName, false); }
        set { EditorPrefs.SetBool(cEditorPrefLoadMasterOnPlay + "_" + PlayerSettings.productName, value); }
    }

    private static string MasterScene
    {
        get { return EditorPrefs.GetString(cEditorPrefMasterScene + "_" + PlayerSettings.productName, "Master.unity"); }
        set { EditorPrefs.SetString(cEditorPrefMasterScene + "_" + PlayerSettings.productName, value); }
    }

    private static string PreviousScenePath
    {
        get { return EditorPrefs.GetString(cEditorPrefPreviousScene + "_" + PlayerSettings.productName, EditorSceneManager.GetActiveScene().name); }
        set { EditorPrefs.SetString(cEditorPrefPreviousScene + "_" + PlayerSettings.productName, value); }
    }
}
#endif
