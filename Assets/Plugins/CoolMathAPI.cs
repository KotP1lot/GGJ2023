using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CoolMathAPI : MonoBehaviour
{
    [SerializeField]private IsGameStart data;
    public string[] domains = new string[] {
 "https://www.coolmath-games.com",
 "www.coolmath-games.com",
 "edit.coolmath-games.com",
 "www.stage.coolmath-games.com",
 "edit-stage.coolmath-games.com",
 "dev.coolmath-games.com",
 "m.coolmath-games.com",
 "https://www.coolmathgames.com",
 "www.coolmathgames.com",
 "edit.coolmathgames.com",
 "www.stage.coolmathgames.com",
 "edit-stage.coolmathgames.com",
 "dev.coolmathgames.com",
 "m.coolmathgames.com",
 "https://edit.coolmathgames.com",
 "https://www.stage.coolmathgames.com",
 "https://edit-stage.coolmathgames.com",
 "https://dev.coolmathgames.com",
 "https://m.coolmathgames.com"
 };
    [DllImport("__Internal")]
    private static extern void RedirectTo(string url);
    // Check right away if the domain is valid
    private void Start()
    {
        if (!data.isGameStart)
        {
            CheckDomains();
            StartGameEvent();
            data.isGameStart = true;
        }
    }
    private void CheckDomains()
    {
        if (!IsValidHost(domains))
        {
           RedirectTo("www.coolmathgames.com");
        }
    }
    private bool IsValidHost(string[] hosts)
    {
        foreach (string host in hosts)
            if (Application.absoluteURL.IndexOf(host) == 0)
                return true;
        return false;
    }
    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void StartGameEvent();
    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void StartLevelEvent(int level);
    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void ReplayEvent();

    public void OnApplicationQuit()
    {
        data.isGameStart = false;
    }
}
