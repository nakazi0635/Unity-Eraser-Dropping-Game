using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueGaugeMove : MonoBehaviour
{
    private float MovePower;
    public Slider PowerGauge;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        MovePower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PowerGauge.value = MovePower;
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            MovePower = 0;
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            MovePower += Time.deltaTime * 100;
            
            if(MovePower > 100){
                MovePower = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            rb.AddForce(transform.forward * -1 * MovePower/10, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0, -1, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0, 1, 0);
        }
    }
}
