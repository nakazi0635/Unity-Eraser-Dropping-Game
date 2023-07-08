using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 赤プレイヤーの操作を定義したクラス
public class RedEraserScript : PlayerController
{
    // 赤プレイヤーの入力をチェック
    protected override void CheckInput()
    {
        // Wキーが押された時の処理
        if (Input.GetKeyDown(KeyCode.W)){
            MovePower = 0;
            pushEffectObject.SetActive(true);
            audioSource.Play();
        }else if (Input.GetKey(KeyCode.W)){
            MovePower += Time.deltaTime * 100;

            // ゲームスタート中にWキーが押された時のための処理
            if(firstPushed){
                firstPushed = false;
                pushEffectObject.SetActive(true);
                audioSource.Play();
            }
            
            // MovePowerが100を超えたら0に戻す
            if (MovePower > 100){
                MovePower = 0;
            }
        // Wキーが離された時の処理
        }else if (Input.GetKeyUp(KeyCode.W)){
            rb.AddForce(transform.forward * -1 * MovePower / 5, ForceMode.Impulse);
            pushEffectObject.SetActive(false);
            audioSource.Stop();
        }
        // Aキーが押された時の処理
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(0, -1, 0);
        }
        // Dキーが押された時の処理
        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(0, 1, 0);
        }
        // Sキーが押された時の処理
        if(Input.GetKey(KeyCode.S) && time > 2){
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
