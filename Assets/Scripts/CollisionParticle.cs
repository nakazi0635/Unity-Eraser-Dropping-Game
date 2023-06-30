using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip SE1;
    public GameObject effect;
    private GameObject effectObject;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "RedEraser"){
        audioSource.PlayOneShot(SE1);
        effectObject = Instantiate(effect, other.contacts[0].point, Quaternion.identity);
        Destroy(effectObject, 1.0f);
        }
    }
}
