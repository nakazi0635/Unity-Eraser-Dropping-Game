using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    private BlueGaugeMove blueGaugeMove;
    private GaugeMove gaugeMove;
    public bool showExplain = false; 
    public GameObject textPanel;
    public GameObject explainPanel;
    public GameObject explain_1;
    public GameObject explain_2;
    public GameObject explain_3;
    public int explain_num = 1;

    // Start is called before the first frame update
    void Start()
    {
        explainPanel.SetActive(false);
        textPanel.SetActive(true);
        explain_2.SetActive(false);
        explain_3.SetActive(false);
        // Debug.Log(blueGaugeMove.gameOver);
        // Debug.Log(titleCanvasManager.showExplain);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && showExplain == false){
            explainPanel.SetActive(true);
            textPanel.SetActive(false);
            showExplain = true;
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && explain_num == 1){
            explain_num = 2;
            explain_1.SetActive(false);
            explain_2.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && explain_num == 2){
            explain_num = 3;
            explain_2.SetActive(false);
            explain_3.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && explain_num == 3){
            SceneManager.LoadScene("Game");
        }else if(Input.GetKeyDown(KeyCode.LeftArrow) && explain_num == 3){
            explain_num = 2;
            explain_3.SetActive(false);
            explain_2.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.LeftArrow) && explain_num == 2){
            explain_num = 1;
            explain_2.SetActive(false);
            explain_1.SetActive(true);
        }
    }
}
