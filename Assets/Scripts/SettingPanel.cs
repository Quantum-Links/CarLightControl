using System;
using System.Net;
using System.Net.Sockets;

using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
	public static TcpClient TcpClient;
	[SerializeField] InputField IPInputField;
	[SerializeField] InputField PortInputField;
	[SerializeField] Text ErrorText;
	[SerializeField] Button LinkButton;
	[SerializeField] Text LinkText;
	Button LoadButton;

	string fileType;
	private void Awake()
	{
		LoadButton = transform.Find("Btn_Load").GetComponent<Button>();
		LoadButton.onClick.AddListener(LoadJson);
		fileType = NativeFilePicker.ConvertExtensionToFileType("json");
	}
	void LoadJson()
	{
		NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
		{
			if (path == null)
				return;
			else
			{
				try
				{
					ErrorText.color = Color.green;
					ErrorText.text = "读取成功";
					ScriptMain.Instance.LoadJson(path);
				}
				catch (Exception e)
				{
					ErrorText.color = Color.red;
					ErrorText.text = "读取失败";
				}
			}
				Debug.Log("Picked file: " + path);
		}, new string[] { fileType });
	}


	public async void Link()
	{
		ErrorText.color = Color.red;
		ErrorText.text = null;
		if (TcpClient != null)
		{
			TcpClient.Dispose();
			TcpClient = null;
			LinkText.text = "连接";
			return;
		}
		LinkButton.interactable = false;
		IPAddress ip;
		try
		{
			ip = IPAddress.Parse(IPInputField.text);
		}
		catch
		{
			LinkButton.interactable = true;
			ErrorText.text = "IP输入错误";
			return;
		}
		try
		{
			TcpClient = new TcpClient();
			await TcpClient.ConnectAsync(ip, int.Parse(PortInputField.text));
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			LinkButton.interactable = true;
			ErrorText.text = "连接失败";
			return;
		}
		LinkButton.interactable = true;
		ErrorText.color = Color.green;
		ErrorText.text = "连接成功";
		LinkText.text = "断开连接";
	}
}
