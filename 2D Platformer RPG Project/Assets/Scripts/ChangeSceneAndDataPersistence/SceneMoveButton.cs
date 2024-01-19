using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMoveButton : MonoBehaviour
{
    // Check the scene build index and move to that specific Scene
    public int sceneBuildIndex;
    private Button button;
    private Text buttonText;

    // Start is called before the first frame update
    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
    }

    public void LoadLevel()
    {
        DontDestroy.DestroyPersistingObjects();
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
