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
    public Rigidbody rb;
    public GameObject pushEffectObject;
    public GameObject fallCamera;
    public GameObject cmVcam;

    void Start()
    {
        MovePower = 0;
        pushEffectObject.SetActive(false);

        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
        audioSource.clip = SE1; // 効果音を設定
    }

    void Update()
    {
        if(transform.position.y < -1){
            fallCamera.SetActive(true);
            cmVcam.SetActive(true);
        }
        PowerGauge.value = MovePower;

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
    }
}
