using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public GameObject _gamePass;
    public GameObject _gameOver;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gamePass.transform.Find("BtnRestart").GetComponent<Button>().onClick.AddListener(() => 
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;

        });
        _gameOver.transform.Find("BtnRestart").GetComponent<Button>().onClick.AddListener(() => 
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
