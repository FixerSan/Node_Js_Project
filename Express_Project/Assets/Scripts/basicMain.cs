using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class basicMain : MonoBehaviour
{
    public Button Hello;        //hello 버튼 선언
    public string host;         //주소 변수 선언
    public int port;            //포트 번호 선언(0~25000)

    private void Start()
    {
        this.Hello.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/", host, port);
            Debug.Log(url);
            StartCoroutine(this.GetBasic(url, (raw) =>
            {
                Debug.LogFormat("{0}", raw);
            }
            ));
        }
        );
    }

    private IEnumerator GetBasic(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("----->" + webRequest.downloadHandler.text);

        if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) //접속 커넥션에 문제가 있을때, 프로토콜에 문제가 있을 때
        {
            Debug.Log("네트워크 환경이 좋지 않아 통신 불가능");

        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }

    }


}
