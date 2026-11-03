
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SelecionarChefeAutomaticamente
{
    static SelecionarChefeAutomaticamente()
    {
        EditorSceneManager.sceneOpened += AoAbrirCena;
    }

    private static void AoAbrirCena(Scene cena, OpenSceneMode modo)
    {
        if (cena.name != "Gameplay")
            return;

        if (!EditorApplication.isPlaying)
            return;

        EditorApplication.delayCall += SelecionarChefe;
    }

    private static void SelecionarChefe()
    {
        GameObject chefe = GameObject.Find("Enemy 1");

        if (chefe == null)
            chefe = GameObject.Find("Boss");

        if (chefe != null)
        {
            Selection.activeGameObject = chefe;
            EditorGUIUtility.PingObject(chefe);
        }
        else
        {
            Debug.LogWarning("Chefe não encontrado na cena Gameplay.");
        }
    }
}
