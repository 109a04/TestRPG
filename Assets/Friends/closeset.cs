using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeset : MonoBehaviour
{
    public Button buttonClose;
    public GameObject friendMainPanel; // �n�ͭ��O

    // Start is called before the first frame update
    void Start()
    {
        // �����s���I���ƥ�K�[��ť��
        buttonClose.onClick.AddListener(OnButton1Clicked);

    }

    public void OnButton1Clicked()
    {
        friendMainPanel.SetActive(false);
    }


}