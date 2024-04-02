using System;
using System.Text;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class LightView : MonoBehaviour
{
    [SerializeField] Text LightNameText;
    [SerializeField] Toggle LightToggle;
    [SerializeField] Image OnImage;
    [SerializeField] Text OnText;
    [SerializeField] Image OffImage;
    [SerializeField] Text OffText;
    static Color32 OnColor = new Color32(0x14, 0x82, 0x6F, 0xff);
    static Color32 OnTextColor = new Color32(255, 255, 255, 255);
    static Color32 OffColor = new Color32(0, 0, 0, 245);
    static Color32 OffTextColor = new Color32(138, 255, 250, 161);
    public LightModel LightModel;
	bool isOn;
    public void Init(LightModel lightModel)
    {
        LightModel = lightModel;
        LightNameText.text = LightModel.LightName;
    }
	public async void ToggleChanged(bool ison)
	{
		isOn = ison;
		StopAllTweens();
		try
		{
			var buffer = Encoding.UTF8.GetBytes(ison ? LightModel.LightOnProtocol : LightModel.LightOffProtocol);
			await SettingPanel.TcpClient.GetStream().WriteAsync(buffer);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		AnimateToggle(ison);
		LightModel.LightObject.SetActive(ison);
	}
	private void AnimateToggle(bool isOn)
	{
		if (isOn)
		{
			OnImage.DOColor(OnColor, 0.3f);
			OnText.DOColor(OnTextColor, 0.3f);
			OffImage.DOColor(OffColor, 0.3f);
			OffText.DOColor(OffTextColor, 0.3f);
		}
		else
		{
			OnImage.DOColor(OffColor, 0.3f);
			OnText.DOColor(OffTextColor, 0.3f);
			OffImage.DOColor(OnColor, 0.3f);
			OffText.DOColor(OnTextColor, 0.3f);
		}
	}
	private void StopAllTweens()
	{
		OnImage.DOKill();
		OnText.DOKill();
		OffImage.DOKill();
		OffText.DOKill();
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
