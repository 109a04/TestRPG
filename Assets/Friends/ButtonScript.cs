using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainPanel;   //主要面板
    public GameObject buttonPanel;
    public GameObject panel1; // 面板1-好友列表   
    public GameObject panel2; // 面板2-加入好友
    public GameObject panel3; // 面板3-好友確認

    public Button button1; // 按鈕1
    public Button button2; // 按鈕2
    public Button button3; // 按鈕3


    private void Start()
    {
        // 為每個按鈕的點擊事件添加監聽器
        button1.onClick.AddListener(OnButton1Clicked);
        button2.onClick.AddListener(OnButton2Clicked);
        button3.onClick.AddListener(OnButton3Clicked);

    }

    public void OnButton1Clicked()
    {
        panel1.SetActive(true);
        buttonPanel.SetActive(true);
    }

    public void OnButton2Clicked()
    {
        panel2.SetActive(true);
        buttonPanel.SetActive(true);
    }

    public void OnButton3Clicked()
    {
        panel3.SetActive(true);
        buttonPanel.SetActive(true);

    }
}
