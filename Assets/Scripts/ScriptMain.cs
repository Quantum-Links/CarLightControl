using System.IO;

using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptMain : MonoBehaviour
{
	public static ScriptMain Instance { get; private set; }
	LightView LigheToggle;
	Transform HeadTransform;
	Transform TrailTransform;
	CanvasGroup SettingGroup;
	LightList LightList;
	Transform Car02_Lights_Emissive;
	const float AnimationDuration = 0.25f;

	CanvasGroup MainGroup;


	private void Awake()
	{
		Instance = this;
		LigheToggle = Resources.Load<LightView>("Tog_Light");
		Car02_Lights_Emissive = GameObject.Find("Car02_Lights_Emissive").transform;
		HeadTransform = GameObject.Find("Pnl_Head").transform;
		TrailTransform = GameObject.Find("Pnl_Trail").transform;
		SettingGroup = GameObject.Find("Pnl_Setting").GetComponent<CanvasGroup>();
		MainGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();

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
	private void InstantiateAndInit(LightView lightPrefab, LightModel lightModel, Transform parentTransform)
	{
		var iLight = Instantiate(lightPrefab, parentTransform);
		iLight.Init(lightModel);
	}
	public void ShowSetting()
	{
		UpdateSettingGroup(SettingGroup.blocksRaycasts == true ? false : true);
	}
	private void UpdateSettingGroup(bool show)
	{
		SettingGroup.blocksRaycasts = show;
		SettingGroup.alpha = show ? 1 : 0;
	}
	private void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))
		{
			MainGroup.alpha -= Time.deltaTime * 5;
			MainGroup.blocksRaycasts = false;
		}
		else if (MainGroup.alpha != 1)
		{
			MainGroup.alpha += Time.deltaTime * 5;
			MainGroup.blocksRaycasts = true;
		}
	}
}