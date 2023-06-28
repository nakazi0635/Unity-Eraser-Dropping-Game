using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeMove : MonoBehaviour
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

        if(Input.GetKey(KeyCode.W)){
            MovePower += Time.deltaTime * 100;
            
            if(MovePower > 100){
                MovePower = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.W)){
            rb.AddForce(transform.forward * -1 * MovePower, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0, -1, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(0, 1, 0);
        }
    }
}
