using UnityEngine;
using UnityEngine.UI;

public class Dater : MonoBehaviour
{
	Text DataTimeText;
	private void Awake()
	{
		DataTimeText = GetComponent<Text>();
		RefreshTime();
	}
	private void Update()
	{
		if (Time.frameCount % 60 == 0)
		{
			RefreshTime();
		}
	}
	private void RefreshTime()
	{
		DataTimeText.text = $"{System.DateTime.Now:MMMddÈÕ}    {GetTimePeriod()}  {System.DateTime.Now:hh:mm}";
	}
	private string GetTimePeriod()
	{
		int currentHour = System.DateTime.Now.Hour;
		return currentHour switch
		{
			>= 0 and < 2 => "ÎçÒ¹",
			>= 2 and < 4 => "Áè³¿",
			>= 4 and < 6 => "·÷Ïþ",
			>= 6 and < 7 => "Çå³¿",
			>= 7 and < 8 => "Ôç³¿",
			>= 8 and < 11 => "ÉÏÎç",
			>= 11 and < 13 => "ÖÐÎç",
			>= 13 and < 14 => "ÏÂÎç",
			>= 14 and < 17 => "ÏÂÎç",
			>= 17 and < 18 => "°øÍí",
			>= 18 and < 19 => "ÍíÉÏ",
			_ => "Ò¹¼ä"
		};
	}
}