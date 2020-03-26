using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Newtonsoft.Json;


public class LunchtimeScript : MonoBehaviour
{
    public KMBombInfo BombInfo;
    public KMBombModule BombModule;
	public KMAudio KMAudio;
	public KMSelectable UpArrow;
	public KMSelectable DownArrow;
	public KMSelectable Submit;
	public Material[] Pictures;
	public MeshRenderer PictureBox;
	public TextMesh Minutes;
	public TextMesh Seconds;
	public AudioClip TickingSound;
	public TextMesh dollar;
	
	protected bool BossWaitForLunch = false;
	protected bool Exploded = false;

	int food = 0;
	int mins = 0;
	int secs = 0;
	
	int clicked = 1;
	
	float cash = 0.00f;
	float cashafter = 0.00f;
	
	bool moneyChanged = true;
	
	int serialport;
	int stereoport;
	int parallelport;
	int ports;
	int indicators;
	
	int batteries;
	int dbat;
	int aabat;
	
	bool vegetables = true;
	bool dairy = true;
	bool twovowels = true;
	
	bool sandwiches = false;
	bool plates = false;
	
	bool unicorn = false;
	bool leftovers = false;
	
	bool case1 = false;
	bool case2 = false;
	bool case3 = false;
	
	int correct;
	
	//Logging
    static int moduleIdCounter = 1;
    int moduleId;
	
    private bool ContainsVowel()
    {
        return GetSerial().Any(ch => "AEIOU".Contains(ch));
    }

    string GetSerial()
    {
        List<string> response = BombInfo.QueryWidgets(KMBombInfo.QUERYKEY_GET_SERIAL_NUMBER, null);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(response[0])["serial"];
    }
	
	void Start ()
	{
		moduleId = moduleIdCounter++;
		GetComponent<KMBombModule>().OnActivate += ActivateModule;
	}
	
	void ActivateModule()
	{
		UpArrow.OnInteract += UpFunc;
		DownArrow.OnInteract += DownFunc;
		Submit.OnInteract += SubmitFunction;
		BossWaitForLunch = true;
		StartCoroutine(loop());
		cash = Random.Range(1.00f, 7.00f);
		cash = cash * 100;
		cash = Mathf.Round(cash);
		cash = cash / 100;
		food = Random.Range(0, 30);
		mins = Random.Range(8, 13);
		secs = Random.Range(24, 49);
		PictureBox.GetComponent<MeshRenderer>().material = Pictures[food];
		if (secs == 0) {Seconds.text = "00";}
		else if (secs == 1) {Seconds.text = "01";}
		else if (secs == 2) {Seconds.text = "02";}
		else if (secs == 3) {Seconds.text = "03";}
		else if (secs == 4) {Seconds.text = "04";}
		else if (secs == 5) {Seconds.text = "05";}
		else if (secs == 6) {Seconds.text = "06";}
		else if (secs == 7) {Seconds.text = "07";}
		else if (secs == 8) {Seconds.text = "08";}
		else if (secs == 9) {Seconds.text = "09";}
		else { Seconds.text = secs.ToString(); }
		if (mins == 0) {Minutes.text = "00";}
		else if (mins == 1) {Minutes.text = "01";}
		else if (mins == 2) {Minutes.text = "02";}
		else if (mins == 3) {Minutes.text = "03";}
		else if (mins == 4) {Minutes.text = "04";}
		else if (mins == 5) {Minutes.text = "05";}			
		else if (mins == 6) {Minutes.text = "06";}
		else if (mins == 7) {Minutes.text = "07";}					
		else if (mins == 8) {Minutes.text = "08";}
		else if (mins == 9) {Minutes.text = "09";}
		else { Minutes.text = mins.ToString(); }
		dollar.text = cash.ToString() + "$";
		if (dollar.text == "1$") {dollar.text = "1.00$";}
		if (dollar.text == "1.1$") {dollar.text = "1.10$";}
		if (dollar.text == "1.2$") {dollar.text = "1.20$";}
		if (dollar.text == "1.3$") {dollar.text = "1.30$";}
		if (dollar.text == "1.4$") {dollar.text = "1.40$";}
		if (dollar.text == "1.5$") {dollar.text = "1.50$";}
		if (dollar.text == "1.6$") {dollar.text = "1.60$";}
		if (dollar.text == "1.7$") {dollar.text = "1.70$";}
		if (dollar.text == "1.8$") {dollar.text = "1.80$";}
		if (dollar.text == "1.9$") {dollar.text = "1.90$";}
		if (dollar.text == "2$") {dollar.text = "2.00$";}
		if (dollar.text == "2.1$") {dollar.text = "2.10$";}
		if (dollar.text == "2.2$") {dollar.text = "2.20$";}
		if (dollar.text == "2.3$") {dollar.text = "2.30$";}
		if (dollar.text == "2.4$") {dollar.text = "2.40$";}
		if (dollar.text == "2.5$") {dollar.text = "2.50$";}
		if (dollar.text == "2.6$") {dollar.text = "2.60$";}
		if (dollar.text == "2.7$") {dollar.text = "2.70$";}
		if (dollar.text == "2.8$") {dollar.text = "2.80$";}
		if (dollar.text == "2.9$") {dollar.text = "2.90$";}
		if (dollar.text == "3$") {dollar.text = "3.00$";}
		if (dollar.text == "3.1$") {dollar.text = "3.10$";}
		if (dollar.text == "3.2$") {dollar.text = "3.20$";}
		if (dollar.text == "3.3$") {dollar.text = "3.30$";}
		if (dollar.text == "3.4$") {dollar.text = "3.40$";}
		if (dollar.text == "3.5$") {dollar.text = "3.50$";}
		if (dollar.text == "3.6$") {dollar.text = "3.60$";}
		if (dollar.text == "3.7$") {dollar.text = "3.70$";}
		if (dollar.text == "3.8$") {dollar.text = "3.80$";}
		if (dollar.text == "3.9$") {dollar.text = "3.90$";}
		if (dollar.text == "4$") {dollar.text = "4.00$";}
		if (dollar.text == "4.1$") {dollar.text = "4.10$";}
		if (dollar.text == "4.2$") {dollar.text = "4.20$";}
		if (dollar.text == "4.3$") {dollar.text = "4.30$";}
		if (dollar.text == "4.4$") {dollar.text = "4.40$";}
		if (dollar.text == "4.5$") {dollar.text = "4.50$";}
		if (dollar.text == "4.6$") {dollar.text = "4.60$";}
		if (dollar.text == "4.7$") {dollar.text = "4.70$";}
		if (dollar.text == "4.8$") {dollar.text = "4.80$";}
		if (dollar.text == "4.9$") {dollar.text = "4.90$";}
		if (dollar.text == "5$") {dollar.text = "5.00$";}
		if (dollar.text == "5.1$") {dollar.text = "5.10$";}
		if (dollar.text == "5.2$") {dollar.text = "5.20$";}
		if (dollar.text == "5.3$") {dollar.text = "5.30$";}
		if (dollar.text == "5.4$") {dollar.text = "5.40$";}
		if (dollar.text == "5.5$") {dollar.text = "5.50$";}
		if (dollar.text == "5.6$") {dollar.text = "5.60$";}
		if (dollar.text == "5.7$") {dollar.text = "5.70$";}
		if (dollar.text == "5.8$") {dollar.text = "5.80$";}
		if (dollar.text == "5.9$") {dollar.text = "5.90$";}
		if (dollar.text == "6$") {dollar.text = "6.00$";}
		if (dollar.text == "6.1$") {dollar.text = "6.10$";}
		if (dollar.text == "6.2$") {dollar.text = "6.20$";}
		if (dollar.text == "6.3$") {dollar.text = "6.30$";}
		if (dollar.text == "6.4$") {dollar.text = "6.40$";}
		if (dollar.text == "6.5$") {dollar.text = "6.50$";}
		if (dollar.text == "6.6$") {dollar.text = "6.60$";}
		if (dollar.text == "6.7$") {dollar.text = "6.70$";}
		if (dollar.text == "6.8$") {dollar.text = "6.80$";}
		if (dollar.text == "6.9$") {dollar.text = "6.90$";}
		if (dollar.text == "7$") {dollar.text = "7.00$";}
		
		batteries = BombInfo.GetBatteryCount();
		parallelport = BombInfo.GetPortCount(Port.Parallel);
		if (batteries > 4) {cash = cash + 0.61f;}
		else if (BombInfo.IsIndicatorOn("FRK") && parallelport == 0) {cash = cash + 0.43f;}
		else if (BombInfo.IsIndicatorPresent("FRQ") && BombInfo.IsIndicatorPresent("CAR")) {cash = cash - 1.00f;}
		else if (BombInfo.CountUniquePorts() > 2) {cash = cash + 0.18f;}
		else if (ContainsVowel()) {cash = cash + 0.37f;}
		else {moneyChanged = false;}
		
		dbat = BombInfo.GetBatteryCount(Battery.AA);
		aabat = BombInfo.GetBatteryCount(Battery.D);
		if (dbat > aabat) {sandwiches = true;}
		else if (aabat > dbat) {plates = true;}
		else if (aabat == dbat) {sandwiches = true; plates = true;}
		
		ports = BombInfo.GetPortCount();
		indicators = BombInfo.GetIndicators().Count();
		if (BombInfo.IsIndicatorOn("SND") || BombInfo.IsIndicatorOn("CLR") || BombInfo.IsIndicatorOff("MSA") || BombInfo.IsIndicatorOff("BOB")) {vegetables = false;}
		else if (ports + indicators >= 5) {dairy = false;}
		else if (cash > 6 && batteries < 2) {twovowels = false;}
		
		serialport = BombInfo.GetPortCount(Port.Serial);
		stereoport = BombInfo.GetPortCount(Port.StereoRCA);
		if (serialport == 1 && stereoport == 1 && ports == 2 && BombInfo.IsIndicatorOn("BOB")) {unicorn = true;}
		else if (cash < 1.24f) {leftovers = true;}
		else if (vegetables && !moneyChanged) {case1 = true;}
		else if (!vegetables && moneyChanged) {case2 = true;}
		else {case3 = true;}
		
		cashafter = cash;
		
		if (cash >= 6.41f && plates){correct = 2; cashafter = cash - 6.41f;}
		else if (cash >= 5.82f && twovowels){correct = 9; cashafter = cash - 5.82f;}
		else if (cash >= 5.33f && sandwiches && twovowels && dairy){correct = 16; cashafter = cash - 5.33f;}
		else if (cash >= 5.22f && plates && dairy){correct = 8; cashafter = cash - 5.22f;}
		else if (cash >= 5.18f && plates){correct = 0; cashafter = cash - 5.18f;}
		else if (cash >= 4.71f && sandwiches && twovowels){correct = 23; cashafter = cash - 4.71f;}
		else if (cash >= 4.67f && plates && twovowels){correct = 26; cashafter = cash - 4.67f;}
		else if (cash >= 4.58f && sandwiches && vegetables){correct = 1; cashafter = cash - 4.58f;}
		else if (cash >= 4.31f && plates){correct = 27; cashafter = cash - 4.31f;}
		else if (cash >= 4.04f && sandwiches && twovowels && vegetables && dairy){correct = 5; cashafter = cash - 4.04f;}
		else if (cash >= 3.76f && twovowels){correct = 21; cashafter = cash - 3.76f;}
		else if (cash >= 3.62f && sandwiches && dairy){correct = 18; cashafter = cash - 3.62f;}
		else if (cash >= 3.44f && sandwiches && twovowels && vegetables && dairy){correct = 22; cashafter = cash - 3.44f;}
		else if (cash >= 3.43f && vegetables){correct = 25; cashafter = cash - 3.43f;}
		else if (cash >= 3.41f && plates && dairy){correct = 4; cashafter = cash - 3.41f;}
		else if (cash >= 3.39f && sandwiches && dairy){correct = 12; cashafter = cash - 3.39f;}
		else if (cash >= 3.21f && plates && vegetables && dairy){correct = 20; cashafter = cash - 3.21f;}
		else if (cash >= 2.96f && sandwiches && dairy){correct = 17; cashafter = cash - 2.96f;}
		else if (cash >= 2.94f && vegetables){correct = 29; cashafter = cash - 2.94f;}
		else if (cash >= 2.82f && twovowels && dairy){correct = 15; cashafter = cash - 2.82f;}
		else if (cash >= 2.75f && twovowels && vegetables){correct = 7; cashafter = cash - 2.75f;}
		else if (cash >= 2.55f && dairy){correct = 13; cashafter = cash - 2.55f;}
		else if (cash >= 2.19f && sandwiches){correct = 19; cashafter = cash - 2.19f;}
		else if (cash >= 2.05f && sandwiches && twovowels && dairy){correct = 10; cashafter = cash - 2.05f;}
		else if (cash >= 2.04f && plates && vegetables){correct = 28; cashafter = cash - 2.04f;}
		else if (cash >= 1.91f && sandwiches && vegetables && dairy){correct = 3; cashafter = cash - 1.91f;}
		else if (cash >= 1.81f && plates){correct = 9; cashafter = cash - 1.81f;}
		else if (cash >= 1.69f && plates && vegetables){correct = 24; cashafter = cash - 1.69f;}
		else if (cash >= 1.24f){correct = 6; cashafter = cash - 1.24f;}
		else {correct = 14;}
		
		cashafter = cashafter * 100;
		cashafter = Mathf.Round(cashafter);

		Debug.LogFormat("[Lunchtime #{0}] ContainsVowel {1}", moduleId, ContainsVowel());
		Debug.LogFormat("[Lunchtime #{0}] vegetables {1}", moduleId, vegetables);
		Debug.LogFormat("[Lunchtime #{0}] dairy {1}", moduleId, dairy);
		Debug.LogFormat("[Lunchtime #{0}] twovowels {1}", moduleId, twovowels);
		Debug.LogFormat("[Lunchtime #{0}] case 1 {1}", moduleId, case1);
		Debug.LogFormat("[Lunchtime #{0}] case 2 {1}", moduleId, case2);
		Debug.LogFormat("[Lunchtime #{0}] case 3 {1}", moduleId, case3);
		Debug.LogFormat("[Lunchtime #{0}] unicorn {1}", moduleId, unicorn);
		Debug.LogFormat("[Lunchtime #{0}] leftovers {1}", moduleId, leftovers);
		Debug.LogFormat("[Lunchtime #{0}] sandwiches {1}", moduleId, sandwiches);
		Debug.LogFormat("[Lunchtime #{0}] plates {1}", moduleId, plates);
		Debug.LogFormat("[Lunchtime #{0}] cash {1}", moduleId, cash);
		Debug.LogFormat("[Lunchtime #{0}] moneyChanged {1}", moduleId, moneyChanged);
		Debug.LogFormat("[Lunchtime #{0}] correct {1}", moduleId, correct);
		Debug.LogFormat("[Lunchtime #{0}] cashafter {1}", moduleId, cashafter);
	}
	
	private IEnumerator loop()
	{
		while (BossWaitForLunch)
		{
			secs--;
			if (secs < 0) {secs = 59; mins--;}
			
			if (secs == 0) {Seconds.text = "00";}
			else if (secs == 1) {Seconds.text = "01";}
			else if (secs == 2) {Seconds.text = "02";}
			else if (secs == 3) {Seconds.text = "03";}
			else if (secs == 4) {Seconds.text = "04";}
			else if (secs == 5) {Seconds.text = "05";}
			else if (secs == 6) {Seconds.text = "06";}
			else if (secs == 7) {Seconds.text = "07";}
			else if (secs == 8) {Seconds.text = "08";}
			else if (secs == 9) {Seconds.text = "09";}
			else { Seconds.text = secs.ToString(); }
			
			if (mins == 0) {Minutes.text = "00";}
			else if (mins == 1) {Minutes.text = "01";}
			else if (mins == 2) {Minutes.text = "02";}
			else if (mins == 3) {Minutes.text = "03";}
			else if (mins == 4) {Minutes.text = "04";}
			else if (mins == 5) {Minutes.text = "05";}			
			else if (mins == 6) {Minutes.text = "06";}
			else if (mins == 7) {Minutes.text = "07";}					
			else if (mins == 8) {Minutes.text = "08";}
			else if (mins == 9) {Minutes.text = "09";}
			else { Minutes.text = mins.ToString(); }
			KMAudio.PlaySoundAtTransform(TickingSound.name, transform);
			if (mins == 0 && secs == 0) { Exploded = true; StartCoroutine(bomba()); BossWaitForLunch = false; }
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	private IEnumerator bomba()
	{
		while (Exploded)
		{
			BombModule.HandleStrike();
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	protected bool DownFunc()
	{
		KMAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, UpArrow.transform);
		UpArrow.AddInteractionPunch();
		food++;
		if (food > 29) food = 0;
		PictureBox.GetComponent<MeshRenderer>().material = Pictures[food];
		return false;
	}
	
	protected bool UpFunc()
	{
		KMAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, DownArrow.transform);
		DownArrow.AddInteractionPunch();
		food--;
		if (food < 0) food = 29;
		PictureBox.GetComponent<MeshRenderer>().material = Pictures[food];
		return false;
	}
	
	protected bool SubmitFunction()
	{
		KMAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, UpArrow.transform);
		UpArrow.AddInteractionPunch();
		
		if (unicorn)
		{
			if (clicked == 3){Solved();}
			clicked++;
		}
		else if (leftovers && food == correct)
		{
			if (secs == 0 || secs == 6 || secs == 12 || secs == 18 || secs == 24 || secs == 30 || secs == 36 || secs == 42 || secs == 48 || secs == 54)
			{
				Solved();
			}
			else
			{
				Striked();
			}
		}
		else if (food == correct)
		{
			if (case1)
			{
				int i = secs % 10;
				int j = secs / 10;
				if (i + j == 2 || i + j == 3 || i + j == 5 || i + j == 7 || i + j == 11 || i + j == 13 || i + j == 17)
				{
					Solved();
				}
				else
				{
					Striked();
				}
			}
			else if (case2)
			{
				int i = secs % 10;
				if (i == 0)
				{
					Solved();
				}
				else
				{
					Striked();
				}
			}
			else if (case3)
			{
				float f = cashafter % 10;
				int i = secs % 10;
				if (i == f)
				{
					Solved();
				}
				else
				{
					Striked();
				}
			}
		}
		else
		{
			Striked();
		}
		return false;
	}
	
	protected bool Solved()
	{
		BombModule.HandlePass();
		UpArrow.OnInteract = Nothingness;
		DownArrow.OnInteract = Nothingness;
		BossWaitForLunch = false;
		Submit.OnInteract = Nothingness;
		return false;
	}
	
	protected bool Striked()
	{
		Debug.LogFormat("[Lunchtime #{0}] mins {1}", moduleId, mins);
		Debug.LogFormat("[Lunchtime #{0}] secs {1}", moduleId, secs);
		Debug.LogFormat("[Lunchtime #{0}] chosen food {1}", moduleId, food);
		Debug.LogFormat("[Lunchtime #{0}] correct {1}", moduleId, correct);
		Debug.LogFormat("[Lunchtime #{0}] money left {1}", moduleId, cashafter);
		BombModule.HandleStrike();
		return false;
	}
	
	protected bool Nothingness()
	{
		return false;
	}
}
