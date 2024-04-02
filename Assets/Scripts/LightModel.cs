using System;

using UnityEngine;

[System.Serializable]
public class LightModel
{
	public string LightName;
	public string LightOnProtocol;
	public string LightOffProtocol;
	public string LightObjectName;
	public bool IsBlinking;
	[NonSerialized]
	public GameObject LightObject;
}
[System.Serializable]
public class LightList
{
	public LightModel[] HeadItems;
	public LightModel[] TrailItems;
	public LightList()
	{
		HeadItems = new[]
		{
			new LightModel { LightName = "Զ��/ADB", LightOnProtocol = "AT+SWITCH1=1", LightOffProtocol = "AT+SWITCH1=0", LightObjectName = "Car02_Lights_HeadLight", IsBlinking = false },
			new LightModel { LightName = "����", LightOnProtocol = "AT+SWITCH2=1", LightOffProtocol = "AT+SWITCH2=0", LightObjectName = "Car02_Lights_HighBeam", IsBlinking = false },
			new LightModel { LightName = "���е�", LightOnProtocol = "AT+SWITCH3=1", LightOffProtocol = "AT+SWITCH3=0", LightObjectName = "Car02_Ligths_DayLight", IsBlinking = false },
			new LightModel { LightName = "λ�õ�", LightOnProtocol = "AT+SWITCH4=1", LightOffProtocol = "AT+SWITCH4=0", LightObjectName = "Car02_Lights_Local", IsBlinking = false },
			new LightModel { LightName = "��ת��", LightOnProtocol = "AT+SWITCH5=1", LightOffProtocol = "AT+SWITCH5=0", LightObjectName = "Car02_Lights_TurnLeft", IsBlinking = false },
			new LightModel { LightName = "��ת��", LightOnProtocol = "AT+SWITCH6=1", LightOffProtocol = "AT+SWITCH6=0", LightObjectName = "Car02_Lights_TurnRight", IsBlinking = false }
		};
		TrailItems = new[]
		{
			new LightModel { LightName = "�ƶ���", LightOnProtocol = "AT+SWITCH7=1", LightOffProtocol = "AT+SWITCH7=0", LightObjectName = "Car02_Lights_Brake", IsBlinking = false },
			new LightModel { LightName = "������", LightOnProtocol = "AT+SWITCH8=1", LightOffProtocol = "AT+SWITCH8=0", LightObjectName = "Car02_Lights_Reverse", IsBlinking = false }
		};
	}
}