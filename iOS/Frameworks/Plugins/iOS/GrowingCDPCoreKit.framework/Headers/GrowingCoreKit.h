//
//  GrowingCoreKit.h
//  GrowingCoreKit
//
//  Created by GrowingIO on 2018/5/14.
//  Copyright © 2018年 GrowingIO. All rights reserved.
//

#import <UIKit/UIKit.h>

#ifndef __cplusplus
@import Foundation;
@import CoreTelephony;
@import SystemConfiguration;
@import Security;
@import CFNetwork;
@import CoreLocation;
@import WebKit;
#endif

typedef NS_ENUM(NSInteger, GrowingAspectMode)
{
    // 默认 类似KVO的机制进行数据采集
    // 目前已知对Aspects以及部分不确定的手写swizzling有冲突
    // 依据不同调用顺序 可能出现函数不被调用或者崩溃的问题
    // 另外如果使用 object_getClass或者swfit的dynamicType属性会得到一个KVO的子类
    // 如果用于取得XIB或者其他资源 可能会失效
    GrowingAspectModeSubClass           ,
    
    //高兼容性 性能比GrowingAspectTypeSubClass略低 但是比RAC和Aspects快8-10倍左右
    GrowingAspectModeDynamicSwizzling   ,
};

@interface Growing : NSObject

// SDK版本号
+ (NSString*)sdkVersion;

/// 初始化SDK，请在applicationDidFinishLaunching中调用此函数初始化,并且在主线程中
/// 如果需要采样 设置一个采样值   0.01即1% 0.001即1‰  最多支持小数点后5位
/// @param accountId 项目ID
/// @param dataSourceId 数据源ID
/// @param sampling 采样率
+ (void)startWithAccountId:(NSString *)accountId dataSourceId:(NSString *)dataSourceId withSampling:(CGFloat)sampling;

/// 初始化SDK，请在applicationDidFinishLaunching中调用此函数初始化,并且在主线程中，默认采样100%
/// @param accountId 项目ID
/// @param dataSourceId 数据源ID
+ (void)startWithAccountId:(NSString *)accountId dataSourceId:(NSString *)dataSourceId;

// 命令行输出调试日志
+ (void)setEnableLog:(BOOL)enableLog;
+ (BOOL)getEnableLog;

// 默认为YES
// 设置为NO可以不采集地理位置的统计信息
+ (void)setEnableLocationTrack:(BOOL)enable;
+ (BOOL)getEnableLocationTrack;

// 以下函数设置后会覆盖原有设置
// 此函数如果被定义,则为获取deviceID的第一优先级
// 请在方法 startWithAccountId 之前调用
// 使用自定义的ID 自定义ID长度不可大于64 否则会被抛弃 NSUUID的UUIDString长度为36
// Example:
//  1. setDeviceIDModeToCustomBlock:^NSString*{ return [[[UIDevice currentDevice] identifierForVendor] UUIDString]; }
+ (void)setDeviceIDModeToCustomBlock:(NSString*(^)(void))customBlock;

// 该函数请在main函数第一行调用 APP启动后 将不允许修改采集模式
+ (void)setAspectMode:(GrowingAspectMode)aspectMode;
+ (GrowingAspectMode)getAspectMode;

// 该函数请在main函数第一行调用
+ (void)setBundleId:(NSString *)bundleId;
+ (NSString *)getBundleId; // 此方法只返回您的赋值

// 该函数请在main函数第一行调用
+ (void)setUrlScheme:(NSString *)urlScheme;
+ (NSString *)getUrlScheme; // 此方法只返回您的赋值

// 全局不发送统计信息
+ (void)disable;

// 设置发送数据的时间间隔（单位为秒）
+ (void)setFlushInterval:(NSTimeInterval)interval;
+ (NSTimeInterval)getFlushInterval;

// 设置从后台进入前台重置sessionID的时间间隔 (单位为秒)
+ (void)setSessionInterval:(NSTimeInterval)interval;
+ (NSTimeInterval)getSessionInterval;

// 设置每天使用数据网络（2G、3G、4G）上传的数据量的上限（单位是 KB）
+ (void)setDailyDataLimit:(NSUInteger)numberOfKiloByte;
+ (NSUInteger)getDailyDataLimit;

// 设置数据收集平台服务器地址, 例如：http://www.growingio.com
+ (void)setTrackerHost:(NSString *)host;

// 设置zone信息
+ (void)setZone:(NSString *)zone;

// 设置 GDPR 生效
+ (void)disableDataCollect;
// 设置 GDPR 失效
+ (void)enableDataCollect;
// 获取当前设备id
+ (NSString *)getDeviceId;
// 获取当前uid
+ (NSString *)getVisitUserId;
// 获取当前访问id
+ (NSString *)getSessionId;


/// !!!: V2.0埋点相关API，请在主线程里调用.
/**
 设置登录用户ID
 
 @param userId 登陆用户ID, ID为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
 ！！！不允许传空或者nil, 如有此操作请调用clearUserId函数
 */
+ (void)setUserId:(NSString *)userId;
/**
 清除登录用户ID
 */
+ (void)clearUserId;

/**
 发送自定义事件
 
 @param eventId : 事件Id, Id为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
 */
+ (void)track:(NSString *)eventId;

/**
 发送自定义事件
 
 @param eventId 事件Id, Id为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
 @param variable : 事件变量, 变量不能为nil
 */
+ (void)track:(NSString *)eventId withVariable:(NSDictionary<NSString *, id> *)variable;


/// 发送自定义事件
/// @param eventId 事件Id, Id为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
/// @param itemId 物品id
/// @param itemKey 物品key
+ (void)track:(NSString *)eventId withItem:(NSString *)itemId itemKey:(NSString *)itemKey;

/// 发送自定义事件
/// @param eventId 事件Id, Id为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
/// @param variable 事件变量, 变量不能为nil
/// @param itemId 物品id
/// @param itemKey 物品key
+ (void)track:(NSString *)eventId withVariable:(NSDictionary<NSString *, id> *)variable forItem:(NSString *)itemId itemKey:(NSString *)itemKey;

/**
 发送自定义Page事件

 @param pageName : 页面名称, pageName为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
 */
+ (void)trackPage:(NSString *)pageName;

/**
 发送自定义Page事件

 @param pageName : 页面名称, pageName为正常英文数字组合的字符串, 长度<=1000, 请不要含有 "'|\*&$@/', 等特殊字符
 @param variable : 页面变量, 变量不能为nil
 */
+ (void)trackPage:(NSString *)pageName withVariable:(NSDictionary<NSString *, id> *)variable;

/**
 发送用户事件
 
 @param attributes : 事件变量, 变量不能为nil
 */
+ (void)setUserAttributes:(NSDictionary<NSString *, id>*)attributes;

// 如果您的AppDelegate中 实现了其中一个或者多个方法 请以在已实现的函数中 调用handleUrl
//- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(nullable NSString *)sourceApplication annotation:(id)annotation
//- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url
//- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString*, id> *)options
// 如果以上所有函数都未实现 则请实现 - (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(nullable NSString *)sourceApplication annotation:(id)annotation 方法并调用handleUrl
+ (BOOL)handleUrl:(NSURL*)url;

+ (void)bridgeForWKWebView:(WKWebView *)webView;
@end
