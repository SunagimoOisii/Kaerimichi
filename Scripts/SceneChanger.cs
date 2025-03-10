using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    //Scenes�e�v�f�ɑΉ�����V�[����
    //���̖��O��Scenes�v�f�ƈ�v�����邱��
    public enum Scenes
    {
        Title,
        StageSelect,
        Stage_00,
        Stage_01,
        Stage_02,
        Stage_03,
        Stage_04
    }

    public static void SceneLoad(Scenes scene)
    {
        Debug.Log(scene);
        string sceneStr = scene.ToString();
        if(FindSameScenes(sceneStr))
        {
            SceneManager.LoadScene(sceneStr);
        }
        else
        {
            Debug.LogError($"�V�[�� '{sceneStr}' �͑��݂��Ȃ�");
        }
    }

    //�����ƈ�v���閼�O�̃V�[�������邩�Ő^�U�l��Ԃ�
    private static bool FindSameScenes(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}