using System.Collections;

using UnityEngine;
using UnityEngine.UI;



public class MesPanel : MonoBehaviour
{
	Toggle[] colorButtons;
	Material bodyMaterial;
	Coroutine coroutine;
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
				if (coroutine != null)
					StopCoroutine(coroutine);
				coroutine = StartCoroutine(StartAnimation(color));
				var colorStr = ColorUtility.ToHtmlStringRGBA(bodyMaterial.color);
				PlayerPrefs.SetString("bodyColor", $"#{colorStr}");
			});
		}
	}
	private IEnumerator StartAnimation(Color color)
	{
		bodyMaterial.SetColor("_Color1", color);
		bodyMaterial.SetColor("_EdgeColor", color * 618);
		float fValue = -3;
		while (fValue <= 3)
		{
			fValue += Time.deltaTime * 6.2f;
			bodyMaterial.SetFloat("_Float", fValue);
			yield return null;
		}
		bodyMaterial.SetColor("_Color2", color);
		bodyMaterial.SetFloat("_Float", -3f);
	}
}