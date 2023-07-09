using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    // 必要なコンポーネントやGameObjectをインスペクタから設定できるように[SerializeField]を使用
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject explainPanel;
    [SerializeField] private GameObject explain_1;
    [SerializeField] private GameObject explain_2;
    [SerializeField] private GameObject explain_3;

    // シーン名は一箇所で管理
    private static readonly string GAME_SCENE = "Game";

    private bool showExplain = false; 
    private int explainNum = 1;

    // 最初の設定を行うメソッド
    private void Start()
    {
        InitializeScene();
    }

    // 入力に応じて処理を行うメソッド
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpaceKeyPress();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleRightKeyPress();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HandleLeftKeyPress();
        }
    }

    // 初期化用のメソッド
    private void InitializeScene()
    {
        explainPanel.SetActive(false);
        textPanel.SetActive(true);
        explain_2.SetActive(false);
        explain_3.SetActive(false);
    }

    // スペースキーが押された時の処理
    private void HandleSpaceKeyPress()
    {
        if (showExplain == false)
        {
            explainPanel.SetActive(true);
            textPanel.SetActive(false);
            showExplain = true;
        }
    }

    // 右キーが押された時の処理
    private void HandleRightKeyPress()
    {
        switch (explainNum)
        {
            case 1:
                explainNum = 2;
                explain_1.SetActive(false);
                explain_2.SetActive(true);
                break;
            case 2:
                explainNum = 3;
                explain_2.SetActive(false);
                explain_3.SetActive(true);
                break;
            case 3:
                SceneManager.LoadScene(GAME_SCENE);
                break;
        }
    }

    // 左キーが押された時の処理
    private void HandleLeftKeyPress()
    {
        switch (explainNum)
        {
            case 2:
                explainNum = 1;
                explain_2.SetActive(false);
                explain_1.SetActive(true);
                break;
            case 3:
                explainNum = 2;
                explain_3.SetActive(false);
                explain_2.SetActive(true);
                break;
        }
    }
}
