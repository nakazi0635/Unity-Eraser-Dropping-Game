using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeMove : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip SE1;
    private float MovePower;
    public Slider PowerGauge;
    public Slider JumpGauge;
    public Rigidbody rb;
    public GameObject pushEffectObject;
    public GameObject jumpEffectObject;
    public GameObject fallCamera;
    public GameObject cmVcam;
    public GameObject sliders;
    public GameObject texts;
    private float time = 3;
    private bool gameStart = false;
    public bool gameOver = false;

    void Start()
    {
        MovePower = 0;
        pushEffectObject.SetActive(false);
        jumpEffectObject.SetActive(false);
        texts.SetActive(false);

        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
        audioSource.clip = SE1; // 効果音を設定
        StartCoroutine(GameStart());
    }

    void Update()
    {
        if (!gameStart) return;

        time += Time.deltaTime;
        if(transform.position.y < -1){
            fallCamera.SetActive(true);
            cmVcam.SetActive(true);
            sliders.SetActive(false);
            StartCoroutine(GameOver());
        }
        PowerGauge.value = MovePower;
        JumpGauge.value = time;

        if (Input.GetKeyDown(KeyCode.W)){
            MovePower = 0;
            pushEffectObject.SetActive(true);
            audioSource.Play(); // 効果音のループ再生を開始
        }

        if (Input.GetKey(KeyCode.W)){
            MovePower += Time.deltaTime * 100;
            
            if (MovePower > 100){
                MovePower = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.W)){
            rb.AddForce(transform.forward * -1 * MovePower / 5, ForceMode.Impulse);
            pushEffectObject.SetActive(false);
            audioSource.Stop(); // 効果音のループ再生を停止
        }

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(0, 1, 0);
        }
        if(Input.GetKey(KeyCode.S) && time > 2){
            jumpEffectObject.SetActive(true);
            rb.AddForce(0, 6, 0, ForceMode.Impulse);
            time = 0;
        }else if(time > 1.0){
            jumpEffectObject.SetActive(false);
        }
        
    }
    IEnumerator GameStart(){
        yield return new WaitForSeconds(2f);
        gameStart = true;
    }
    IEnumerator GameOver(){
        yield return new WaitForSeconds(3f);
        texts.SetActive(true);
        gameOver = true;
    }
}
