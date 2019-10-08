using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controll : MonoBehaviour
{
    [SerializeField] GameObject Player_vechicle;
    [SerializeField] float moveSpeed=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var pos = transform.position;
        pos.x += h * Time.deltaTime*moveSpeed;
        pos.z += v * Time.deltaTime*moveSpeed;
        transform.position = pos;
    }
}
