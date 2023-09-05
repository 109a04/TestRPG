using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class friendpageManager : MonoBehaviour
{
    public GameObject friendListPanel;
    public GameObject addFriendPanel;
    public GameObject inviteListPanel;

    public GameObject mainPanel;   //主要面板
    public GameObject buttonPanel; // 按钮面板

    public Button button1; // 按鈕1
    public Button button2; // 按鈕2
    public Button button3; // 按鈕3

    private void Start()
    {

        // 默認顯示好友列表面板
        ShowFriendList();
        

        // 為每個按鈕的點擊事件添加監聽器
        button1.onClick.AddListener(OnButton1Clicked);
        button2.onClick.AddListener(OnButton2Clicked);
        button3.onClick.AddListener(OnButton3Clicked);

    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        buttonPanel.SetActive(true);
    }

    public void ShowFriendList()
    {
        friendListPanel.SetActive(true);
        addFriendPanel.SetActive(false);
        inviteListPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void ShowAddFriend()
    {
        friendListPanel.SetActive(false);
        addFriendPanel.SetActive(true);
        inviteListPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void ShowInviteList()
    {
        friendListPanel.SetActive(false);
        addFriendPanel.SetActive(false);
        inviteListPanel.SetActive(true);
        buttonPanel.SetActive(true);
    }

    public void OnButton1Clicked()
    {
        
        ShowFriendList();

    }

    public void OnButton2Clicked()
    {
        
        ShowAddFriend();
    }

    public void OnButton3Clicked()
    {
        ShowInviteList();
        
    }

}
