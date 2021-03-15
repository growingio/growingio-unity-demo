using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GrowingIO
{
#if UNITY_ANDROID

    private static string ANDROID_CLASS = "com.growingio.demo.GrowingIOHelper";

    private static AndroidJavaObject DicToMap(Dictionary<string, object> dictionary)
    {
        if (dictionary == null)
        {
            return null;
        }

        AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
        foreach (KeyValuePair<string, object> pair in dictionary)
        {
            object value;
            if (pair.Value is string)
            {
                value = pair.Value;
            }
            else if (pair.Value is bool)
            {
                value = new AndroidJavaObject("java.lang.Boolean", pair.Value);
            }
            else if (pair.Value is ValueType)
            {
                value = new AndroidJavaObject("java.lang.Double", pair.Value.ToString());
            }
            else
            {
                value = pair.Value.ToString();
            }

            map.Call<AndroidJavaClass>("put", pair.Key, value);
        }

        return map;
    }

#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void gioSetUserId(string userId);

    [DllImport("__Internal")]
    private static extern void gioClearUserId();

    [DllImport("__Internal")]
    private static extern void gioTrack(string eventId);

    [DllImport("__Internal")]
    private static extern void gioTrackWithVariable(string eventId, string[] keys, string[] stringValues, double[] numberValues, int count);

    [DllImport("__Internal")]
    private static extern void gioTrackWithVariableForItemAnditemKey(string eventId, string[] keys, string[] stringValues, double[] numberValues, int count, string itemId, string itemKey);

    [DllImport("__Internal")]
    private static extern void gioTrackPage(string pageName);

    [DllImport("__Internal")]
    private static extern void gioSetUserAttributes(string[] keys, string[] stringValues, double[] numberValues, int count);

    private class GIOIOSObject {
        public string[] keys;
        public string[] values;
        public double[] numbers;
        
    }

    private static GIOIOSObject DicToObject(Dictionary<string, object> variable) {
        GIOIOSObject gioObject = new GIOIOSObject();
        gioObject.keys = new String[variable.Count];
        gioObject.values = new String[variable.Count];
        gioObject.numbers = new double[variable.Count];
        int index = 0;
        if (variable != null && variable.Count > 0) {
            foreach (KeyValuePair<string, object> kvp in variable) {
                gioObject.keys[index] = kvp.Key;
                if (kvp.Value is string) {
                    gioObject.values[index] = (string)kvp.Value;
                }
                else if (kvp.Value is ValueType) {
                    gioObject.numbers[index] = System.Convert.ToDouble(kvp.Value);
                }
                else {
                    gioObject.values[index] = System.Convert.ToString(kvp.Value);
                }
                index++;
            }
        }
        return gioObject;
    }
#endif

    public static void SetUserId(string userId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            gioSetUserId(userId);
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("setUserId", userId);
#endif
        }
    }

    public static void ClearUserId()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            gioClearUserId();
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("clearUserId");
#endif
        }
    }

    public static void Track(string eventName)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
           gioTrack(eventName);
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("track", eventName);
#endif
        }
    }

    public static void Track(string eventName, Dictionary<string, object> var)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            if (var != null && var.Count > 0) {
                gioTrackWithVariable(eventName, DicToObject(var).keys, DicToObject(var).values, DicToObject(var).numbers, var.Count);
            } else {
                gioTrack(eventName);
            }
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("track", eventName, DicToMap(var));
#endif
        }
    }

    public static void Track(string eventName, Dictionary<string, object> var, String itemId, String itemKey)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            if (var != null && var.Count > 0) {
                gioTrackWithVariableForItemAnditemKey(eventName, DicToObject(var).keys, DicToObject(var).values, DicToObject(var).numbers, var.Count, itemId, itemKey);
            } else {
                gioTrack(eventName);
            }
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("track", eventName, DicToMap(var), itemId, itemKey);
#endif
        }
    }

    public static void TrackPage(string pageName)
    {
        if (Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
           gioTrackPage(pageName);
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("trackPage", pageName);
#endif
        }
    }

    public static void TrackPage(string pageName, Dictionary<string, object> var)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
           gioTrackPage(pageName);
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("trackPage", pageName, DicToMap(var));
#endif
        }
    }

    public static void SetUserAttributes(Dictionary<string, object> var)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
        	if (var != null && var.Count > 0) {
            	gioSetUserAttributes(DicToObject(var).keys, DicToObject(var).values, DicToObject(var).numbers, var.Count);
        	}
#endif
#if UNITY_ANDROID
            new AndroidJavaClass(ANDROID_CLASS).CallStatic("setUserAttributes", DicToMap(var));
#endif
        }
    }
}