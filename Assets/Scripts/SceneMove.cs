using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    private static bool DoNotDestroy = false;
    private BlueGaugeMove blueGaugeMove;
    private GaugeMove gaugeMove;

    // Start is called before the first frame update
    void Start()
    {
        if (DoNotDestroy == true)
        {
            Destroy(this.gameObject);
        }else{
            DontDestroyOnLoad(this.gameObject);
            DoNotDestroy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Game");
        }else if (Input.GetKeyDown(KeyCode.Space) && (blueGaugeMove.gameOver || gaugeMove.gameOver)){
            SceneManager.LoadScene("Title");
        }
    }
}
