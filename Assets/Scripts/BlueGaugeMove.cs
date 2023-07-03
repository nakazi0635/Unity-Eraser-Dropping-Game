using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueGaugeMove : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        MovePower = 0;
        pushEffectObject.SetActive(false);
        jumpEffectObject.SetActive(false);

        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
        audioSource.clip = SE1; // 効果音を設定
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart) return;

        time += Time.deltaTime;
        if(transform.position.y < -1 || transform.position.z > 13 || transform.position.z < -13 || transform.position.x > 20 || transform.position.x < 0){
            fallCamera.SetActive(true);
            cmVcam.SetActive(true);
            sliders.SetActive(false);
            StartCoroutine(GameOver());
        }
        PowerGauge.value = MovePower;
        JumpGauge.value = time;
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            MovePower = 0;
            pushEffectObject.SetActive(true);
            audioSource.Play(); // 効果音のループ再生を開始
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            MovePower += Time.deltaTime * 100;
            
            if(MovePower > 100){
                MovePower = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            rb.AddForce(transform.forward * -1 * MovePower/5, ForceMode.Impulse);
            pushEffectObject.SetActive(false);
            audioSource.Stop(); // 効果音のループ再生を停止
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0, -1, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0, 1, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow) && time > 2){
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
