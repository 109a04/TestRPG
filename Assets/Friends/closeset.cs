using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeset : MonoBehaviour
{
    public Button buttonClose;
    public GameObject friendMainPanel; // 好友面板

    // Start is called before the first frame update
    void Start()
    {
        // 為按鈕的點擊事件添加監聽器
        buttonClose.onClick.AddListener(OnButton1Clicked);

    }

    public void OnButton1Clicked()
    {
        friendMainPanel.SetActive(false);
    }


}