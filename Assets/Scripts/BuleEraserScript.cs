using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuleEraserScript : PlayerController
{
    protected override void CheckInput()
    {
        // Input checks for the Blue player
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            MovePower = 0;
            pushEffectObject.SetActive(true);
            audioSource.Play(); // 効果音のループ再生を開始
        }else if(Input.GetKey(KeyCode.UpArrow)){
            MovePower += Time.deltaTime * 100;

            if(firstPushed){
                firstPushed = false;
                pushEffectObject.SetActive(true);
                audioSource.Play(); // 効果音のループ再生を開始
            }
            
            if(MovePower > 100){
                MovePower = 0;
            }
        }else if(Input.GetKeyUp(KeyCode.UpArrow)){
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
        if(Input.GetKey(KeyCode.Space) && gameOver){
            SceneManager.LoadScene("Game");
        }
    }
}
