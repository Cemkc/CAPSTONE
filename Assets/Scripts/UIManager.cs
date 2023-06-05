using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    string pythonPath;
    string imagePath;
    string outPath;

    public InputField pythonInputField;
    public InputField imageInputField;
    public InputField outFileInputField;

    public void GenerateSceneButton()
    {
        bool inputFieldsFilled = pythonInputField.text.Length > 0 && imageInputField.text.Length > 0 && outFileInputField.text.Length > 0;

        if (!inputFieldsFilled) return;

        string pythonScriptPath = Application.dataPath + "/Resources/ImageDataExtractor.py";
        Debug.Log(pythonScriptPath);
        string pythonExePath = pythonInputField.text;
        string imagePath = imageInputField.text;
        string outFilePath = outFileInputField.text;

        string buildingsInfo = PythonBridge.RunPythonScript(pythonScriptPath, pythonExePath, imagePath, outFilePath);

        string path = Application.persistentDataPath + "/BuildingInfo.json";
        Debug.Log("Wrote the file into: " + path);
        File.WriteAllText(path, buildingsInfo);

        SceneManager.LoadScene(1);

    }

    public void PythonBrowseButton()
    {
        pythonPath = EditorUtility.OpenFilePanel("Choose python executable", "", "exe");
        pythonInputField.text = pythonPath;
    }

    public void ImageBrowseButton()
    {
        imagePath = EditorUtility.OpenFilePanel("Choose the source image", "", "png,jpg");
        imageInputField.text = imagePath;
    }

    public void OutputFileBrowseButton()
    {
        outPath = EditorUtility.OpenFolderPanel("Choose Output File Directory", "", "");
        outFileInputField.text = outPath;
    }

}
