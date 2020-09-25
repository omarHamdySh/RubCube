using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneMappingNode : MonoBehaviour
{
    public SceneName sceneName;
    public void ChangeScene()
    {
        SceneMappingManager.Instance.changeScene(sceneName);
    }

    public void setNextScene(SceneMappingNode newNode) {
        sceneName = newNode.sceneName;
    }
}
