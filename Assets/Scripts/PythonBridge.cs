using UnityEngine;
using System.Diagnostics;

public static class PythonBridge
{
    public static string RunPythonScript(string scrpitPath, string pythonPath, string imagePath, string outFilePath)
    {
        var psi = new ProcessStartInfo();
        psi.FileName = pythonPath;

        var scrpit = scrpitPath;

        var imagePathMessage = imagePath;
        var outFilePathMessage = outFilePath;

        psi.Arguments = $"\"{scrpit}\" \"{imagePathMessage}\" \"{outFilePathMessage}\"";

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var errors ="";
        var results = "";

        using(var process = Process.Start(psi))
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
        }

        UnityEngine.Debug.Log("ERRORS:");
        UnityEngine.Debug.Log(errors);
        UnityEngine.Debug.Log("RESULT:");
        UnityEngine.Debug.Log(results);

        return results;
    }

}
