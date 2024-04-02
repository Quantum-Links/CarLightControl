using UnityEngine;
using UnityEngine.UI;

public class Dater : MonoBehaviour
{
	Text DataTimeText;
	private void Awake()
	{
		DataTimeText = GetComponent<Text>();
	}
	private void Update()
	{
		if (Time.frameCount % 60 == 0)
		{
			DataTimeText.text = $"{System.DateTime.Now: tt hh:mm  MMMdd»’}";
		}
	}
}