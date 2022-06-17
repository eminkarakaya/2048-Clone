using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    int lastKacakac;
    float lastScale;
    float lastDistance;
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public float _4x4Scale = 1f;
    public float _5x5Scale = .8f;
    public float _6x6Scale = .6f;
    public float _8x8Scale = .4f;
    GameObject winCanvas;
    GameObject loseCanvas;
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Retry()
    {
        LoadScene(1);
    }
    public void Home()
    {
        LoadScene(0);
        SceneManager.LoadScene(0);
    }
    public void Btn_4x4()
    {
        LoadScene(1);
    }
    public void Btn_5x5()
    {
        LoadScene(2);
    }
    public void Btn_6x6()
    {
        LoadScene(3);
    }
    public void Btn_8x8()
    {
        LoadScene(4);
    }
}
