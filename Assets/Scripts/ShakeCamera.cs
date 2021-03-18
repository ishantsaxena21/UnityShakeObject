using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System;
using SimpleJSON;

[Serializable]
public class MyClass
{
    public int level;
    public string advice;
}

public class ShakeCamera : MonoBehaviour
{
    Vector3 originalPos;
    public float shakeTime, shakeRange, skipTime = 2 * 60.0f;
    [SerializeField] GameObject winPanel;
    [SerializeField] Text text;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            winPanel.SetActive(true);
            GetData();
        }
    }
    private void Start()
    {
         
        originalPos = transform.position;

        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine()
    {
        
        while(true)
        {
            float elapsedTime = 0;
            while (elapsedTime < shakeTime)
            {
                Vector3 pos = originalPos + UnityEngine.Random.insideUnitSphere * shakeRange;
                pos.z = originalPos.z;

                transform.position = pos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = originalPos;
            yield return new WaitForSeconds(skipTime); 
        }
    }

    private void GetData()
    {
        string data;
        string url = "https://api.adviceslip.com/advice";

        using (var client = new WebClient())
        {
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            data = client.DownloadString(url);
            
        }
        var jsonData = JSON.Parse(data);
        text.text = (jsonData["slip"]["advice"]);

    }
    

        
}
