package com.growingio.demo;

import android.content.Intent;

import com.growingio.android.sdk.collection.GrowingIO;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {
    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        GrowingIO.getInstance().onNewIntent(this, intent);
    }
}