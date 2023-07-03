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

    // Start is called before the first frame update
    void Start()
    {
        explainPanel.SetActive(false);
        textPanel.SetActive(true);
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
        }else if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Game");
        }
    }
}
