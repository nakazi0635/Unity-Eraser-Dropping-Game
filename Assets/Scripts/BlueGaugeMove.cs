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
    public Rigidbody rb;
    public GameObject pushEffectObject;
    // Start is called before the first frame update
    void Start()
    {
        MovePower = 0;
        pushEffectObject.SetActive(false);

        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
        audioSource.clip = SE1; // 効果音を設定
    }

    // Update is called once per frame
    void Update()
    {
        PowerGauge.value = MovePower;
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
    }
}
