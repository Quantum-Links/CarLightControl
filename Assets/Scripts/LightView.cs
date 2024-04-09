using System;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class LightView : MonoBehaviour
{
	static Color32 OnColor = new Color32(7, 79, 160, 255);
	static Color32 OffColor = new Color32(255, 255, 255, 255);
	public LightModel LightModel;
	Text lightNameText;
	Toggle lightToggle;
	Image onImage;
	bool isOn;
	private void Awake()
	{
		lightToggle = GetComponent<Toggle>();
		lightToggle.onValueChanged.AddListener(ToggleChanged);
		lightNameText = transform.Find("Txt_Des").GetComponent<Text>();
		onImage = transform.Find("Toggle").GetComponent<Image>();
	}
	public void Init(LightModel lightModel)
	{
		LightModel = lightModel;
		lightNameText.text = LightModel.LightName;
	}
	async void ToggleChanged(bool ison)
	{
		isOn = ison;
		try
		{
			var buffer = Encoding.UTF8.GetBytes(ison ? LightModel.LightOnProtocol : LightModel.LightOffProtocol);
			await SettingPanel.TcpClient.GetStream().WriteAsync(buffer);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		onImage.CrossFadeColor(isOn ? OnColor : OffColor, 0.1f, true, false);
		LightModel.LightObject.SetActive(ison);
	}
	private void Update()
	{
		if (isOn && LightModel.IsBlinking && Time.frameCount % 40 == 0)
		{
			ToggleLightObject();
		}
	}
	private void ToggleLightObject()
	{
		if (LightModel != null && LightModel.LightObject != null)
		{
			LightModel.LightObject.SetActive(!LightModel.LightObject.activeInHierarchy);
		}
	}
}
