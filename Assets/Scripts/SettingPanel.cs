using System;
using System.Net;
using System.Net.Sockets;

using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
	public static TcpClient TcpClient;
	InputField IPInputField, PortInputField, SpeedInputField;
	Button LinkButton, LoadButton;
	Text ErrorText, LinkText;

	string fileType;
	private void Awake()
	{
		LinkButton = transform.Find("Btn_Link").GetComponent<Button>();
		LoadButton = transform.Find("Btn_Load").GetComponent<Button>();
		IPInputField = transform.Find("InputField_IP").GetComponent<InputField>();
		PortInputField = transform.Find("InputField_Port").GetComponent<InputField>();
		SpeedInputField = transform.Find("InputField_Speed").GetComponent<InputField>();
		SpeedInputField.text = PlayerPrefs.GetFloat("rotateSpeed", 1).ToString();

		ErrorText = transform.Find("Txt_Error").GetComponent<Text>();
		LinkText = LinkButton.GetComponentInChildren<Text>();
		LinkButton.onClick.AddListener(Link);
		LoadButton.onClick.AddListener(LoadJson);
		SpeedInputField.onEndEdit.AddListener(x =>
		{
			PlayerPrefs.SetFloat("rotateSpeed", float.Parse(x));
			CameraControl.Instance.mouseRotSpeed = PlayerPrefs.GetFloat("rotateSpeed", 1);
		});
		//fileType = NativeFilePicker.ConvertExtensionToFileType("json");
	}
	void LoadJson()
	{
		//NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
		//{
		//	if (path == null)
		//		return;
		//	else
		//	{
		//		try
		//		{
		//			ErrorText.color = Color.green;
		//			ErrorText.text = "��ȡ�ɹ�";
		//			ScriptMain.Instance.LoadJson(path);
		//		}
		//		catch (Exception e)
		//		{
		//			ErrorText.color = Color.red;
		//			ErrorText.text = "��ȡʧ��";
		//		}
		//	}
		//	Debug.Log("Picked file: " + path);
		//}, new string[] { fileType });
	}
	async void Link()
	{
		//ErrorText.color = Color.red;
		//ErrorText.text = null;
		//if (TcpClient != null)
		//{
		//	TcpClient.Dispose();
		//	TcpClient = null;
		//	LinkText.text = "����";
		//	return;
		//}
		//LinkButton.interactable = false;
		//IPAddress ip;
		//try
		//{
		//	ip = IPAddress.Parse(IPInputField.text);
		//}
		//catch
		//{
		//	LinkButton.interactable = true;
		//	ErrorText.text = "IP�������";
		//	return;
		//}
		//try
		//{
		//	LinkText.text = "������";
		//	TcpClient = new TcpClient();
		//	await TcpClient.ConnectAsync(ip, int.Parse(PortInputField.text));
		//}
		//catch (Exception ex)
		//{
		//	Debug.LogError(ex);
		//	LinkButton.interactable = true;
		//	ErrorText.text = "����ʧ��";
		//	return;
		//}
		//LinkButton.interactable = true;
		//ErrorText.color = Color.green;
		//ErrorText.text = "���ӳɹ�";
		//LinkText.text = "�Ͽ�����";
	}
}
