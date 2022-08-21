using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Saturn.HybClasses;
using Newtonsoft.Json;
using System.IO.Compression;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;

public class TitleState : MonoBehaviour
{
    Stopwatch time;
    public float bpm = 102;
    public long curBeat = 0;
    public GameObject logo;
    public float defaultScale;
    public float timing;
    public bool transiting = false;
    public AudioSource source;
    public GameObject downloaderObjectPopup;
    public Slider downLoad;
    public TextMeshProUGUI downLoadText;

    public void PopupYes()
    {
        StartCoroutine(Download());
        Time.timeScale = 0;
        time.Stop();
    }

    public IEnumerator Download()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get("https://www.dropbox.com/s/lr6yjwxxf7g5j6v/mods.zip?dl=1"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                string savePath = Path.Combine(StaticVariables.Root, "mods.zip");
                File.WriteAllBytes(savePath, uwr.downloadHandler.data);
                ZipFile.ExtractToDirectory(Path.Combine(StaticVariables.Root, "mods.zip"), StaticVariables.Root);
                PopupNo();
            }
        }
    }

    public void PopupNo()
    {
        downloaderObjectPopup.SetActive(false);
        PlayerPrefs.SetString("dontShowPackPopup", "true");
        Time.timeScale = 1;
        time.Start();
    }

    private void Start()
    {
#if UNITY_STANDALONE_WIN
        StaticVariables.Root = Application.dataPath + "/..";
#endif
#if UNITY_ANDROID
        StaticVariables.Root = Application.persistentDataPath;
#endif




        if (!PlayerPrefs.HasKey("dontShowPackPopup"))
            PlayerPrefs.SetString("dontShowPackPopup", "false");


        if (!File.Exists(Path.Combine(StaticVariables.Root, "mods")) && !Convert.ToBoolean(PlayerPrefs.GetString("dontShowPackPopup")))
        {
            downloaderObjectPopup.SetActive(true);
        }


        if (PlayerPrefs.HasKey("menu-volume"))
        {
            source.volume = PlayerPrefs.GetFloat("menu-volume");
        }
        else
        {
            source.volume = 0.5f;
        }

        MiscOptions miscOptions;

        if (PlayerPrefs.HasKey("misc"))
        {
            miscOptions = JsonConvert.DeserializeObject<MiscOptions>(PlayerPrefs.GetString("misc"));
        }
        else
        {
            miscOptions = new MiscOptions();
            miscOptions.fps = 120;
        }
        Application.targetFrameRate = miscOptions.fps;
        time = new Stopwatch();
        time.Start();
        defaultScale = logo.transform.localScale.x;
        LeanTween.value(-6, 6, 2f).setOnUpdate((float f) =>
        {
            logo.transform.localEulerAngles = new Vector3(0, 0, f);
        }).setEaseInOutCirc().setLoopPingPong();
    }

    private void Update()
    {
        print(Input.inputString);
        timing = time.ElapsedMilliseconds;
        if ((float)time.ElapsedMilliseconds / 1000 >= 60 / bpm && !transiting)
        {
            curBeat++;
            time.Restart();
            LeanTween.value(defaultScale, defaultScale - 0.1f, 0.3f).setOnUpdate((float f) => {
                logo.transform.localScale = new Vector3(f, f, f);
            });
        }
    }

    public void Play()
    {
        Transition.instance.Show(() =>
        {
            transiting = true;
            LeanTween.cancelAll();
            SceneManager.LoadScene("FreeplayState");
        });
    }

    public void Options()
    {
        Transition.instance.Show(() =>
        {
            transiting = true;
            LeanTween.cancelAll();
            SceneManager.LoadScene("OptionsState");
        });
    }

    public void ToModio()
    {
        Transition.instance.Show(() =>
        {
            transiting = true;
            LeanTween.cancelAll();
            SceneManager.LoadScene("Modio");
        });
    }

    public void Exit() => Application.Quit();
}
