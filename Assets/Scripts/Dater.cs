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
		DataTimeText.text = $"{System.DateTime.Now:MMMdd��}    {GetTimePeriod()}  {System.DateTime.Now:hh:mm}";
	}
	private string GetTimePeriod()
	{
		int currentHour = System.DateTime.Now.Hour;
		return currentHour switch
		{
			>= 0 and < 2 => "��ҹ",
			>= 2 and < 4 => "�賿",
			>= 4 and < 6 => "����",
			>= 6 and < 7 => "�峿",
			>= 7 and < 8 => "�糿",
			>= 8 and < 11 => "����",
			>= 11 and < 13 => "����",
			>= 13 and < 14 => "����",
			>= 14 and < 17 => "����",
			>= 17 and < 18 => "����",
			>= 18 and < 19 => "����",
			_ => "ҹ��"
		};
	}
}