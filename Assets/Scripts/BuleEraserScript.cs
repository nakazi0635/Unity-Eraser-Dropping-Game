using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 青プレイヤーの操作を定義したクラス
public class BuleEraserScript : PlayerController
{
    // 青プレイヤーの入力をチェック
    protected override void CheckInput()
    {
        // UpArrowキーが押された時の処理
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            MovePower = 0;
            pushEffectObject.SetActive(true);
            audioSource.Play();
        }else if(Input.GetKey(KeyCode.UpArrow)){
            MovePower += Time.deltaTime * 100;

            // ゲームスタート中にUpArrowキーが押された時のための処理
            if(firstPushed){
                firstPushed = false;
                pushEffectObject.SetActive(true);
                audioSource.Play();
            }

            // MovePowerが100を超えたら0に戻す
            if(MovePower > 100){
                MovePower = 0;
            }
        // UpArrowキーが離された時の処理
        }else if(Input.GetKeyUp(KeyCode.UpArrow)){
            rb.AddForce(transform.forward * -1 * MovePower/5, ForceMode.Impulse);
            pushEffectObject.SetActive(false);
            audioSource.Stop();
        }
        // LeftArrowキーが押された時の処理
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0, -1, 0);
        }
        // RightArrowキーが押された時の処理
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0, 1, 0);
        }
        // DownArrowキーが押された時の処理
        if(Input.GetKey(KeyCode.DownArrow) && time > 2){
            jumpEffectObject.SetActive(true);
            rb.AddForce(0, 6, 0, ForceMode.Impulse);
            time = 0;
        }else if(time > 1.0){
            jumpEffectObject.SetActive(false);
        }
        // ゲーム終了後にスペースキーが押された時の処理
        if(Input.GetKey(KeyCode.Space) && gameOver){
            SceneManager.LoadScene("Game");
        }
    }
}
