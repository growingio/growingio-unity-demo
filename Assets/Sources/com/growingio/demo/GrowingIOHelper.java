package com.growingio.demo;

import com.growingio.android.sdk.collection.GrowingIO;
import com.growingio.android.sdk.utils.LogUtil;
import com.growingio.android.sdk.utils.ThreadUtils;

import org.json.JSONObject;

import java.util.HashMap;

public class GrowingIOHelper {
    private static final String TAG = "GIO.GrowingIOHelper";

    public static void setUserId(final String userId) {
        LogUtil.d(TAG, "setUserId: userId = " + userId);
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().setUserId(userId);
            }
        });
    }

    public static void clearUserId() {
        LogUtil.d(TAG, "clearUserId");
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().clearUserId();
            }
        });
    }

    public static void track(final String eventName) {
        LogUtil.d(TAG, "track: eventName = " + eventName);
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().track(eventName);
            }
        });
    }

    public static void track(final String eventName, final HashMap var) {
        LogUtil.d(TAG, "track: eventName = " + eventName + ", var = " + var);
        if (var == null || var.isEmpty()) {
            return;
        }
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().track(eventName, new JSONObject(var));
            }
        });
    }

    public static void track(final String eventName, final HashMap var, String itemId, String itemKey) {
        LogUtil.d(TAG, "track: eventName = " + eventName + ", var = " + var + ", itemId = " + itemId + ", itemKey = " + itemKey);
        if (var == null || var.isEmpty()) {
            return;
        }
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().track(eventName, new JSONObject(var), itemId, itemKey);
            }
        });
    }

    public static void trackPage(final String pageName) {
        LogUtil.d(TAG, "track: pageName = " + pageName);
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().trackPage(pageName);
            }
        });
    }

    public static void trackPage(final String pageName, final HashMap var) {
        LogUtil.d(TAG, "track: pageName = " + pageName + ", var = " + var);
        if (var == null || var.isEmpty()) {
            return;
        }
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().trackPage(pageName, new JSONObject(var));
            }
        });
    }

    public static void setUserAttributes(final HashMap var) {
        LogUtil.d(TAG, "setUserAttributes: var = " + var);
        if (var == null || var.isEmpty()) {
            return;
        }
        ThreadUtils.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                GrowingIO.getInstance().setUserAttributes(new JSONObject(var));
            }
        });
    }
}