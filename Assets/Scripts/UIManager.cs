using System;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : UGUIBase
{
    public Transform gridParent;
    public GameObject gridPrefab;
    
    private Button m_ConnectBtn;
    private Button m_DebugBtn;
    private Button m_ShowBtn;
    private Button m_UpdateBtn;
    private Button m_InsertBtn;
    private Text m_ConnectionStatesText;
    private InputField m_NameInput;
    private InputField m_AttackInput;
    private InputField m_HealthInput;
    private readonly SplHelper m_DataHelper = new SplHelper();
    protected override void Awake()
    {
        FindChildrenUIControl<Button>();
        FindChildrenUIControl<Text>();
        FindChildrenUIControl<InputField>();

        m_ConnectBtn = GetUIControl<Button>("ConnectButton");
        m_DebugBtn = GetUIControl<Button>("DebugData");
        m_ShowBtn = GetUIControl<Button>("ShowData");
        m_UpdateBtn = GetUIControl<Button>("UpdateData");
        m_InsertBtn = GetUIControl<Button>("InsertData");
        m_ConnectionStatesText = GetUIControl<Text>("StateText");
        m_NameInput = GetUIControl<InputField>("NameInput");
        m_AttackInput = GetUIControl<InputField>("AttackInput");
        m_HealthInput = GetUIControl<InputField>("HealthInput");
    }

    private void Start()
    {
        m_ConnectBtn.onClick.AddListener(() =>
        {
            if (m_DataHelper.ConnectSql())
            {
                m_ConnectionStatesText.text = "连接成功";
                m_ConnectionStatesText.color = Color.green;
                SwitchControlStates();
            }
            else
            {
                m_ConnectionStatesText.text = "连接失败";
                m_ConnectionStatesText.color = Color.red;
                SwitchControlStates();
            }
        });
        
        m_DebugBtn.onClick.AddListener(() =>
        {
            m_DataHelper.DebugData();
        });
        
        m_ShowBtn.onClick.AddListener(() =>
        {
            Transform[] list = gridParent.GetComponentsInChildren<Transform>();
            if (list.Length > 7)
            {
                for (int i = 7; i < list.Length; i++)
                {
                    Destroy(list[i].gameObject);
                }
            }
            foreach (string str in m_DataHelper.ShowData())
            {
                GameObject obj = Instantiate(gridPrefab, gridParent);
                obj.GetComponentInChildren<Text>().text = str;
            }
        });
        
        m_UpdateBtn.onClick.AddListener(() =>
        {
            m_DataHelper.GetData();
        });
        
        m_InsertBtn.onClick.AddListener(() =>
        {
            string name = m_NameInput.text;
            string attack = m_AttackInput.text;
            string health = m_HealthInput.text;
            m_DataHelper.InsertData(name, attack, health);
        });
    }

    private void SwitchControlStates()
    {
        if (m_DataHelper.Connection.State == ConnectionState.Open)
        {
            m_DebugBtn.interactable = true;
            m_ShowBtn.interactable = true;
            m_InsertBtn.interactable = true;
            m_UpdateBtn.interactable = true;
            m_NameInput.interactable = true;
            m_AttackInput.interactable = true;
            m_HealthInput.interactable = true;
        }
        else
        {
            m_DebugBtn.interactable = false;
            m_ShowBtn.interactable = false;
            m_InsertBtn.interactable = false;
            m_UpdateBtn.interactable = false;
            m_NameInput.interactable = false;
            m_AttackInput.interactable = false;
            m_HealthInput.interactable = false;
        }
    }
    
    void OnApplicationQuit()
    {
        if (m_DataHelper.Connection != null)
        {
            m_DataHelper.Connection.Close();
            Debug.Log("关闭连接！");
        }
    }
}