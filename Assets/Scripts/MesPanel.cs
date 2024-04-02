using UnityEngine;
using UnityEngine.UI;

public class MesPanel : MonoBehaviour
{
	InputField speedInputField;
	Button[] colorButtons;
	Button saveButton;
	Material bodyMaterial;
	private void Awake()
	{
		speedInputField = transform.Find("InputField_Speed").GetComponent<InputField>();
		CameraControl.Instance.mouseRotSpeed = PlayerPrefs.GetFloat("rotateSpeed", 1);
		speedInputField.text = CameraControl.Instance.mouseRotSpeed.ToString();

		bodyMaterial = GameObject.Find("Car02_Body_LOD1").GetComponent<MeshRenderer>().sharedMaterials[0];
		string colorString = PlayerPrefs.GetString("bodyColor", "#074FA0");
		Color bodyColor;
		ColorUtility.TryParseHtmlString(colorString, out bodyColor);
		bodyMaterial.color = bodyColor;
		colorButtons = transform.Find("LayOut").GetComponentsInChildren<Button>();
		for (int i = 0; i < colorButtons.Length; i++)
		{
			var tmpBtn = colorButtons[i];
			var color = tmpBtn.GetComponent<Image>().color;
			tmpBtn.onClick.AddListener(() => bodyMaterial.color = color);
		}
		saveButton = transform.Find("Btn_Save").GetComponent<Button>();
		saveButton.onClick.AddListener(SaveParam);
	}
	public void SaveParam()
	{
		var colorStr = ColorUtility.ToHtmlStringRGBA(bodyMaterial.color);
		Debug.Log(colorStr);
		PlayerPrefs.SetString("bodyColor", $"#{colorStr}");
		PlayerPrefs.SetFloat("rotateSpeed", float.Parse(speedInputField.text));
		CameraControl.Instance.mouseRotSpeed = PlayerPrefs.GetFloat("rotateSpeed", 1);
	}
}