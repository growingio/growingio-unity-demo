using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    void Start()
    {
    }

    public void click_Set_User_Id()
    {
        GrowingIO.SetUserId("userId");
    }

    public void click_Clear_User_Id()
    {
        GrowingIO.ClearUserId();
    }

    public void click_Track_String()
    {
        GrowingIO.Track("TrackString");
    }

    public void click_Track_String_Dic()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object> { { "key1", "value1" }, { "key2", 111 }, { "key3", false } };
        GrowingIO.Track("TrackStringDic", dictionary);
    }

    public void click_Track_String_Dic_String_String()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object> { { "key1", "value1" }, { "key2", 111 }, { "key3", false } };
        GrowingIO.Track("TrackStringDicStringString", dictionary, "id", "key");
    }

    public void click_Track_Page_String()
    {
        GrowingIO.TrackPage("TrackPage");
    }

    public void click_Track_Page_String_Dic()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object> { { "key1", "value1" }, { "key2", 111 }, { "key3", false } };
        GrowingIO.TrackPage("TrackPageStringDic", dictionary);
    }

    public void click_Set_User_Attributes()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object> { { "key1", "value1" }, { "key2", 111 }, { "key3", false } };
        GrowingIO.SetUserAttributes(dictionary);
    }
}
