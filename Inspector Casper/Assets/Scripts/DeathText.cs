using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private float textSize;
    private Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textSize = textMeshPro.fontSize;
        originalColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.color = Color.Lerp(originalColor, Color.red, Mathf.PingPong(Time.time, 1));
        textMeshPro.fontSize = Mathf.Lerp(textSize, textSize + 5, Mathf.PingPong(Time.time, 1));
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    
}
