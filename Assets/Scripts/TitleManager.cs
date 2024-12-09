using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private List<GameObject> slides;

    private int page = 0;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowTutorial);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame(){
        SceneManager.LoadScene("PlayScene");
    }

    private void ShowTutorial(){
        Debug.Log("Tutorial");
    }

    private void ExitGame(){
        Application.Quit();
        Debug.Log("Game Exit");
    }
}
