#import <WebKit/WebKit.h>
#import <Growing.h>

// Converts C style string to NSString
static NSString *gioCreateNSString(const char *string) {
    if (string)
        return [NSString stringWithUTF8String:string];
    else
        return [NSString stringWithUTF8String:""];
}

static NSDictionary *gioCreateDiction(const char *keys[], const char *stringValues[], double numberValues[],int count) {
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    for (int i = 0; i < count; i++) {
        if (keys[i] != NULL) {
            if (stringValues[i] != NULL) {
                [dic setObject:gioCreateNSString(stringValues[i])
                        forKey:gioCreateNSString(keys[i])];
            } else {
                [dic setObject:[NSNumber numberWithDouble:numberValues[i]] forKey:gioCreateNSString(keys[i])];
            }
        }
    }
    return dic.copy;
}


extern "C" {
    
#pragma GCC diagnostic ignored "-Wmissing-prototypes"
    
    void gioSetUserId(const char *userId) {
        [Growing setUserId:gioCreateNSString(userId)];
    }
    
    void gioClearUserId(){
        [Growing clearUserId];
    }
    
    void gioTrack(const char *eventName) {
        [Growing track:gioCreateNSString(eventName)];
    }
    
    void gioTrackWithVariable(const char *eventName, const char *keys[], const char *stringValues[], double numberValues[], int count) {
        [Growing track:gioCreateNSString(eventName) withVariable:gioCreateDiction(keys, stringValues, numberValues, count)];
    }
    
    void gioTrackWithVariableForItemAnditemKey(const char *eventName, const char *keys[], const char *stringValues[], double numberValues[], int count, const char *itemId, const char *itemKey) {
        [Growing track:gioCreateNSString(eventName) withVariable:gioCreateDiction(keys, stringValues, numberValues, count) forItem:gioCreateNSString(itemId) itemKey:gioCreateNSString(itemKey)];
    }
    
    void gioTrackPage(const char *pageName) {
        [Growing trackPage:gioCreateNSString(pageName)];
    }

    void gioSetUserAttributes(const char *keys[], const char *stringValues[], double numberValues[], int count) {
        [Growing setUserAttributes:gioCreateDiction(keys, stringValues, numberValues, count)];
    }

#pragma GCC diagnostic warning "-Wmissing-prototypes"
    
}
