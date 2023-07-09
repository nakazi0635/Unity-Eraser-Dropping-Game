using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip SE1;
    [SerializeField] private GameObject effect;

    // エフェクトの寿命とタグ名を定数で管理
    private const float EFFECT_LIFETIME = 1.0f;
    private const string RED_ERASER_TAG = "RedEraser";

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // エフェクトを再生するメソッド
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == RED_ERASER_TAG)
        {
            audioSource.PlayOneShot(SE1);
            GameObject effectObject = Instantiate(effect, other.contacts[0].point, Quaternion.identity);
            Destroy(effectObject, EFFECT_LIFETIME);
        }
    }
}
