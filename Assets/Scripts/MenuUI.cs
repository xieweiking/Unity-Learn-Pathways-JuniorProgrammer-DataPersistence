using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour {

    public static HighestRecord highestRecord, currentRecord;

    private static string path;

    public TextMeshProUGUI bestScoreText;
    public TMP_InputField nameInput;

    void Awake() {
        path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(path);
        if (File.Exists(path)) {
            var json = File.ReadAllText(path);
            highestRecord = JsonUtility.FromJson<HighestRecord>(json);
        }
    }

    // Start is called before the first frame update
    void Start() {
        if (highestRecord != null)
            bestScoreText.SetText($"Best Score: {highestRecord.name}: {highestRecord.score}");
    }

    public void StartNew() {
        currentRecord = new HighestRecord();
        currentRecord.name = nameInput.text;
        Debug.Log($"Welcom {currentRecord.name} !");
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    [Serializable]
    public class HighestRecord {
        public string name;
        public int score;
    }

    internal static void SaveCurrentRecord(int score) {
        if (highestRecord == null || highestRecord.score < score) {
            currentRecord.score = score;
            var json = JsonUtility.ToJson(currentRecord);
            File.WriteAllText(path, json);
        }
    }

}
