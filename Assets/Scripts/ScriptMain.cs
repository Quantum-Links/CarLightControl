using System.IO;

using DG.Tweening;

using UnityEngine;

public class ScriptMain : MonoBehaviour
{
	public static ScriptMain Instance { get;private set; }
	LightView LigheToggle;
	[SerializeField] RectTransform HeadTransform;
	[SerializeField] RectTransform TrailTransform;
	[SerializeField] CanvasGroup SettingGroup;
	[SerializeField] CanvasGroup ExtraGroup;

	LightList LightList;

	Transform Car02_Lights_Emissive;
	const float AnimationDuration = 0.25f;
	private void Awake()
	{
		Instance = this;
		LigheToggle = Resources.Load<LightView>("Tog_Light");
		Car02_Lights_Emissive = GameObject.Find("Car02_Lights_Emissive").transform;

		var filePath = PlayerPrefs.GetString("jsonPath", $"{Application.persistentDataPath}/Light.json");
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, JsonUtility.ToJson(new LightList()));
		}
		LoadJson(filePath);
	}
	public void LoadJson(string path)
	{
		LightList = JsonUtility.FromJson<LightList>(File.ReadAllText(path));
		foreach (var item in LightList.HeadItems)
		{
			item.LightObject = Car02_Lights_Emissive.Find(item.LightObjectName).gameObject;
			InstantiateAndInit(LigheToggle, item, HeadTransform);
		}
		foreach (var item in LightList.TrailItems)
		{
			item.LightObject = Car02_Lights_Emissive.Find(item.LightObjectName).gameObject;
			InstantiateAndInit(LigheToggle, item, TrailTransform);
		}
		PlayerPrefs.SetString("jsonPath", path);
	}
	private void InstantiateAndInit(LightView lightPrefab, LightModel lightModel, RectTransform parentTransform)
	{
		var iLight = Instantiate(lightPrefab, parentTransform);
		iLight.Init(lightModel);
	}
	public void ShowHead()
	{
		CameraControl.Instance.SetTargetRo(new Vector3(10, -80, 0));
		KillAllAnim();
		HeadTransform.DOPivot(new Vector2(0, 0.5f), AnimationDuration);
		TrailTransform.DOPivot(new Vector2(0, 0.5f), AnimationDuration);
		UpdateSettingGroup(false);
	}
	public void ShowTrail()
	{
		CameraControl.Instance.SetTargetRo(new Vector3(10, 60, 0));
		KillAllAnim();
		HeadTransform.DOPivot(new Vector2(1, 0.5f), AnimationDuration);
		TrailTransform.DOPivot(new Vector2(1, 0.5f), AnimationDuration);
		UpdateSettingGroup(false);
	}
	public void ShowSetting()
	{
		KillAllAnim();
		HeadTransform.DOPivot(new Vector2(1, 0.5f), AnimationDuration);
		TrailTransform.DOPivot(new Vector2(0, 0.5f), AnimationDuration);
		UpdateSettingGroup(true);
	}
	public void ShowExtraSetting()
	{
		KillAllAnim();
		HeadTransform.DOPivot(new Vector2(1, 0.5f), AnimationDuration);
		TrailTransform.DOPivot(new Vector2(0, 0.5f), AnimationDuration);
		UpdateExtraSettingGroup(true);
	}

	private void KillAllAnim()
	{
		HeadTransform.DOKill();
		TrailTransform.DOKill();
		SettingGroup.DOKill();
		ExtraGroup.DOKill();
		UpdateSettingGroup(false);
		UpdateExtraSettingGroup(false);
	}
	private void UpdateSettingGroup(bool show)
	{
		SettingGroup.blocksRaycasts = show;
		SettingGroup.DOFade(show ? 1 : 0, AnimationDuration);
	}
	private void UpdateExtraSettingGroup(bool show)
	{
		ExtraGroup.blocksRaycasts = show;
		ExtraGroup.DOFade(show ? 1 : 0, AnimationDuration);
	}
}
