using System;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;

public class SqlHelper
{
    public MySqlConnection Connection;
    private DataSet m_Data;

    private const string Server = "127.0.0.1";
    private const string Database = "my_new_database";
    private const string Uid = "hmxs";
    private const string Password = "wzh02156514986";
    private const string Table = "data_table";

    public bool ConnectSql()
    {
        string connectionString = "SERVER=" + Server + ";" + "DATABASE=" +
                                  Database + ";" + "UID=" + Uid + ";" + "PASSWORD=" + Password + ";";
 
        Connection = new MySqlConnection(connectionString);
 
        try
        {
            Connection.Open();
            Debug.Log("连接成功！");
            GetData();
            return true;
        }
        catch (MySqlException ex)
        {
            Debug.Log("连接失败：" + ex.Message);
            return false;
        }
    }

    public void GetData()
    {
        m_Data = new DataSet();
        if (Connection.State == ConnectionState.Open)
        {
            string command = "select * from " + Table;
            Debug.Log(command);
            try
            {
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(command, Connection);
                sqlDataAdapter.Fill(m_Data,Table);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + command + e.Message);
            }
        }
    }

    public void DebugData()
    {
        DataTable datas = m_Data.Tables[Table];
        foreach (DataRow row in datas.Rows)
        {
            string str = "";
            foreach (DataColumn column in datas.Columns) 
                str += row[column] + " ";
            Debug.Log(str);
        }
    }

    public List<string> ShowData()
    {
        List<string> temp = new List<string>();
        DataTable datas = m_Data.Tables[Table];
        foreach (DataRow row in datas.Rows)
        {
            foreach (DataColumn column in datas.Columns) 
                temp.Add(row[column].ToString());
        }
        return temp;
    }

    public void InsertData(string name,string attack,string health)
    {
        if (Connection.State == ConnectionState.Open)
        {
            string command = $"insert into {Table}(name,attack,health) values('{name}',{attack},{health})";
            Debug.Log(command);
            try
            {
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(command, Connection);
                sqlDataAdapter.Fill(m_Data,Table);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + command + e.Message);
            }
        }
    }
}
