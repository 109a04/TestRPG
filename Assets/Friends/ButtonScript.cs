using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainPanel;   //�D�n���O
    public GameObject buttonPanel;
    public GameObject panel1; // ���O1-�n�ͦC��   
    public GameObject panel2; // ���O2-�[�J�n��
    public GameObject panel3; // ���O3-�n�ͽT�{

    public Button button1; // ���s1
    public Button button2; // ���s2
    public Button button3; // ���s3


    private void Start()
    {
        // ���C�ӫ��s���I���ƥ�K�[��ť��
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
