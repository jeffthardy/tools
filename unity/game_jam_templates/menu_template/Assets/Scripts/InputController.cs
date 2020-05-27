using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public delegate void InputHandler(string info);
    public event InputHandler OnInput;


    // Start is called before the first frame update
    void Start()
    {
    }
    void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            OnInput.Invoke("a");
        if (Input.GetKeyDown(KeyCode.S))
            OnInput.Invoke("ssss");
        if (Input.GetKeyDown(KeyCode.D))
            OnInput.Invoke("cat");
        if (Input.GetKeyDown(KeyCode.UpArrow))
            OnInput.Invoke("up");
        if (Input.GetKeyDown(KeyCode.DownArrow))
            OnInput.Invoke("down");
    }
}
