using UnityEngine;
using UnityEngine.UI;

public class MesPanel : MonoBehaviour
{
	Toggle[] colorButtons;
	Material bodyMaterial;
	private void Awake()
	{
		bodyMaterial = GameObject.Find("Car02_Body_LOD0").GetComponent<MeshRenderer>().sharedMaterials[0];
		string colorString = PlayerPrefs.GetString("bodyColor", "#074FA0");
		Color bodyColor;
		ColorUtility.TryParseHtmlString(colorString, out bodyColor);
		bodyMaterial.color = bodyColor;
		colorButtons = gameObject.GetComponentsInChildren<Toggle>();
		for (int i = 0; i < colorButtons.Length; i++)
		{
			var tmpBtn = colorButtons[i];
			var color = tmpBtn.GetComponent<Image>().color;
			tmpBtn.onValueChanged.AddListener(x =>
			{
				bodyMaterial.color = color;
				var colorStr = ColorUtility.ToHtmlStringRGBA(bodyMaterial.color);
				PlayerPrefs.SetString("bodyColor", $"#{colorStr}");
			});
		}
	}
	//public void SaveParam()
	//{
	//	var colorStr = ColorUtility.ToHtmlStringRGBA(bodyMaterial.color);
	//	Debug.Log(colorStr);
	//	PlayerPrefs.SetString("bodyColor", $"#{colorStr}");
	//	PlayerPrefs.SetFloat("rotateSpeed", float.Parse(speedInputField.text));
	//	CameraControl.Instance.mouseRotSpeed = PlayerPrefs.GetFloat("rotateSpeed", 1);
	//}
}