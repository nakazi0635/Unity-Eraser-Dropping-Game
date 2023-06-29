using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    public GameObject effect;
    private GameObject effectObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "RedEraser"){
        effectObject = Instantiate(effect, other.contacts[0].point, Quaternion.identity);
        Destroy(effectObject, 1.0f);
        }
    }
}
