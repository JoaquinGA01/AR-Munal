using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;

public class Voz : MonoBehaviour
{
    const string LANG_CODE = "es-ES";

    void Start(){
        Setup(LANG_CODE);
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
    }

    public void StartSpeaking(string message){
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void StopSpeaking(){
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart(){
        Debug.Log("Talking started...");
    }

    void OnSpeakStop(){
        Debug.Log("talking stopped...");
    }

    void Setup(string code){
        TextToSpeech.Instance.Setting(code,1,1);
        SpeechToText.Instance.Setting(code);
    }
}
