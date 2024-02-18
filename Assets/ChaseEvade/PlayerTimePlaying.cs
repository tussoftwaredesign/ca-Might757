using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerTimePlaying : MonoBehaviour
{
    private TextMeshProUGUI textClock;

    public float elapsedTime;
    // Start is called before the first frame update

    void Awake()
    {
        textClock = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        double seconds = Mathf.FloorToInt(elapsedTime % 60);
        textClock.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); 
    }
}
