package com.growingio.demo;

import android.app.Application;

import com.growingio.android.sdk.collection.Configuration;
import com.growingio.android.sdk.collection.GrowingIO;

public class App extends Application {

    @Override
    public void onCreate() {
        super.onCreate();

        GrowingIO.startWithConfiguration(this, new Configuration()
                .setProjectId("91eaf9b283361032")
                .setDataSourceId("91f3e68c8377f983")
                .setURLScheme("growing.eaa8b80f322e35e4")
                .setTestMode(true)
                .setDebugMode(true)
                .setTrackerHost("http://uat-api.growingio.com/")
                .setChannel("GIO应用商店")
        );
    }
}
