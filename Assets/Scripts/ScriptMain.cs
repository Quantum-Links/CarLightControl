using System.Collections;
using System.IO;
using UnityEngine.UI;
using SFB;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Rendering.Universal;
using System.Runtime.InteropServices;
public class ScriptMain : MonoBehaviour
{
	public static ScriptMain Instance { get; private set; }
	LightView LigheToggle;
	Transform HeadTransform;
	Transform TrailTransform;
	CanvasGroup SettingGroup;
	LightList LightList;
	Transform Car02_Lights_Emissive;


	CanvasGroup MainGroup;
	Button TongButton;
	Button SettingButton;

	GameObject[] TTs = new GameObject[2];

	private void Awake()
	{
		Instance = this;
		LigheToggle = Resources.Load<LightView>("Tog_Light");
		MainGroup = GetComponent<CanvasGroup>();
		Car02_Lights_Emissive = GameObject.Find("Car02_Lights_Emissive").transform;
		HeadTransform = transform.Find("Pnl_Head").transform;
		TrailTransform = transform.Find("Pnl_Trail").transform;
		SettingGroup = transform.Find("Pnl_Setting").GetComponent<CanvasGroup>();
		TongButton = transform.Find("Btn_Tong").GetComponent<Button>();
		SettingButton = transform.Find("Btn_Setting").GetComponent<Button>();

		var filePath = PlayerPrefs.GetString("jsonPath", $"{Application.persistentDataPath}/Light.json");
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, JsonUtility.ToJson(new LightList()));
		}
		LoadJson(filePath);
		TTs[0] = GameObject.Find("TT1");
		TTs[1] = GameObject.Find("TT2");
		TongButton.onClick.AddListener(LoadFile);
		SettingButton.onClick.AddListener(ShowSetting);
	}
#if !UNITY_EDITOR && UNITY_WEBGL
	[DllImport("__Internal")]
	private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
	public void LoadFile()
	{
		UploadFile(gameObject.name, "OnFileUpload", ".png", false);
	}
	public void OnFileUpload(string url)
	{
		StartCoroutine(OutputRoutine(url));
	}
#else
	private void LoadFile()
	{
		var paths = StandaloneFileBrowser.OpenFilePanel("¼ÓÔØÍ´³µÌù", "", "png", false);
		if (paths.Length > 0)
		{
			Debug.Log(paths[0]);
			StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
		}
	}
#endif
	private IEnumerator OutputRoutine(string url)
	{
		var uwr = UnityWebRequestTexture.GetTexture(url);
		yield return uwr.SendWebRequest();
		var tex = DownloadHandlerTexture.GetContent(uwr);
		foreach (var f in TTs)
		{
			var decal = f.GetComponent<DecalProjector>();
			decal.material.SetTexture("Base_Map", tex);
			decal.enabled = true;
		}
	}
	private void LoadJson(string path)
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
	private void ShowSetting()
	{
		SettingGroup.blocksRaycasts = !SettingGroup.blocksRaycasts;
		SettingGroup.alpha = SettingGroup.blocksRaycasts ? 1 : 0;
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