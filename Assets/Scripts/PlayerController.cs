using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class PlayerController : MonoBehaviour
{
    protected AudioSource audioSource;
    public AudioClip SE1;
    protected float MovePower;
    public Slider PowerGauge;
    public Slider JumpGauge;
    public Rigidbody rb;
    public GameObject pushEffectObject;
    public GameObject jumpEffectObject;
    public GameObject fallCamera;
    public GameObject cmVcam;
    public GameObject sliders;
    public GameObject texts;
    protected float time = 3f;
    protected bool gameStart = false;
    protected bool gameOver = false;
    protected bool firstPushed = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MovePower = 0;
        pushEffectObject.SetActive(false);
        jumpEffectObject.SetActive(false);
        texts.SetActive(false);

        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得
        audioSource.clip = SE1; // 効果音を設定
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    protected virtual void Update()
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

        CheckInput();
    }

    protected abstract void CheckInput();

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