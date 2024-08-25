
using UndertaleModLib.Models;

public class Databases
{
    public static Dictionary<string, (EventType, uint)> EventNames = new Dictionary<string, (EventType, uint)>
    {

        //===================================================== STANDALONE EVENTS =========================================================================================
        {"Create", (EventType.Create, 0)},
        {"Create_0", (EventType.Create, 0)},
        {"Create 0", (EventType.Create, 0)},


        //===================================================== STEP EVENTS =========================================================================================
        {"Step", (EventType.Step, (int)EventSubtypeStep.Step)},
        {"Step_0", (EventType.Step, (int)EventSubtypeStep.Step)},
        {"Step 0", (EventType.Step, (int)EventSubtypeStep.Step)},

        {"BeginStep", (EventType.Step, (int)EventSubtypeStep.BeginStep)},
        {"Begin Step", (EventType.Step, (int)EventSubtypeStep.BeginStep)},
        {"Step_1", (EventType.Step, (int)EventSubtypeStep.Step)},
        {"Step 1", (EventType.Step, (int)EventSubtypeStep.Step)},

        {"EndStep", (EventType.Step, (int)EventSubtypeStep.EndStep)},
        {"End Step", (EventType.Step, (int)EventSubtypeStep.EndStep)},
        {"Step_2", (EventType.Step, (int)EventSubtypeStep.Step)},
        {"Step 2", (EventType.Step, (int)EventSubtypeStep.Step)},




        //===================================================== DRAW EVENTS =========================================================================================

        {"Draw", (EventType.Draw, (int)EventSubtypeDraw.Draw)},
        {"Draw_0", (EventType.Draw, (int)EventSubtypeDraw.Draw)},
        {"Draw 0", (EventType.Draw, (int)EventSubtypeDraw.Draw)},

        {"DrawGUI", (EventType.Draw, (int)EventSubtypeDraw.DrawGUI)},
        {"Draw GUI", (EventType.Draw, (int)EventSubtypeDraw.DrawGUI)},
        {"GUI", (EventType.Draw, (int)EventSubtypeDraw.DrawGUI)},
        {"Draw_64", (EventType.Draw, (int)EventSubtypeDraw.DrawGUI)},
        {"Draw 64", (EventType.Draw, (int)EventSubtypeDraw.DrawGUI)},

        {"DrawResize", (EventType.Draw, (int)EventSubtypeDraw.Resize)},
        {"Draw Resize", (EventType.Draw, (int)EventSubtypeDraw.Resize)},
        {"Resize", (EventType.Draw, (int)EventSubtypeDraw.Resize)},
        {"Draw_65", (EventType.Draw, (int)EventSubtypeDraw.Resize)},
        {"Draw 65", (EventType.Draw, (int)EventSubtypeDraw.Resize)},

        {"DrawBegin", (EventType.Draw, (int)EventSubtypeDraw.DrawBegin)},
        {"Draw Begin", (EventType.Draw, (int)EventSubtypeDraw.DrawBegin)},
        {"Draw_72", (EventType.Draw, (int)EventSubtypeDraw.DrawBegin)},
        {"Draw 72", (EventType.Draw, (int)EventSubtypeDraw.DrawBegin)},

        {"DrawEnd", (EventType.Draw, (int)EventSubtypeDraw.DrawEnd)},
        {"Draw End", (EventType.Draw, (int)EventSubtypeDraw.DrawEnd)},
        {"Draw_73", (EventType.Draw, (int)EventSubtypeDraw.DrawEnd)},
        {"Draw 73", (EventType.Draw, (int)EventSubtypeDraw.DrawEnd)},

        {"DrawGUIBegin", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},
        {"DrawGUI Begin", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},
        {"Draw GUI Begin", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},
        {"GUI Begin", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},
        {"Draw_74", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},
        {"Draw 74", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIBegin)},

        {"DrawGUIEnd", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},
        {"DrawGUI End", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},
        {"Draw GUI End", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},
        {"GUI End", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},
        {"Draw_75", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},
        {"Draw 75", (EventType.Draw, (int)EventSubtypeDraw.DrawGUIEnd)},

        {"PreDraw", (EventType.Draw, (int)EventSubtypeDraw.PreDraw)},
        {"Draw_76", (EventType.Draw, (int)EventSubtypeDraw.PreDraw)},
        {"Draw 76", (EventType.Draw, (int)EventSubtypeDraw.PreDraw)},

        {"PostDraw", (EventType.Draw, (int)EventSubtypeDraw.PostDraw)},
        {"Draw_77", (EventType.Draw, (int)EventSubtypeDraw.PostDraw)},
        {"Draw 77", (EventType.Draw, (int)EventSubtypeDraw.PostDraw)},

        //===================================================== OTHER EVENTS =========================================================================================

        {"OutsideRoom", (EventType.Draw, (int)EventSubtypeOther.OutsideRoom)},
        {"Outside Room", (EventType.Draw, (int)EventSubtypeOther.OutsideRoom)},
        {"Other_0", (EventType.Draw, (int)EventSubtypeOther.OutsideRoom)},
        {"Other 0", (EventType.Draw, (int)EventSubtypeOther.OutsideRoom)},

        {"IntersectBoundary", (EventType.Draw, (int)EventSubtypeOther.IntersectBoundary)},
        {"Intersect Boundary", (EventType.Draw, (int)EventSubtypeOther.IntersectBoundary)},
        {"Other_1", (EventType.Draw, (int)EventSubtypeOther.IntersectBoundary)},
        {"Other 1", (EventType.Draw, (int)EventSubtypeOther.IntersectBoundary)},

        {"GameStart", (EventType.Draw, (int)EventSubtypeOther.GameStart)},
        {"Game Start", (EventType.Draw, (int)EventSubtypeOther.GameStart)},
        {"Other_2", (EventType.Draw, (int)EventSubtypeOther.GameStart)},
        {"Other 2", (EventType.Draw, (int)EventSubtypeOther.GameStart)},

        {"GameEnd", (EventType.Draw, (int)EventSubtypeOther.GameEnd)},
        {"Game End", (EventType.Draw, (int)EventSubtypeOther.GameEnd)},
        {"Other_3", (EventType.Draw, (int)EventSubtypeOther.GameEnd)},
        {"Other 3", (EventType.Draw, (int)EventSubtypeOther.GameEnd)},

        {"RoomStart", (EventType.Draw, (int)EventSubtypeOther.RoomStart)},
        {"Room Start", (EventType.Draw, (int)EventSubtypeOther.RoomStart)},
        {"Other_4", (EventType.Draw, (int)EventSubtypeOther.RoomStart)},
        {"Other 4", (EventType.Draw, (int)EventSubtypeOther.RoomStart)},

        {"RoomEnd", (EventType.Draw, (int)EventSubtypeOther.RoomEnd)},
        {"Room End", (EventType.Draw, (int)EventSubtypeOther.RoomEnd)},
        {"Other_5", (EventType.Draw, (int)EventSubtypeOther.RoomEnd)},
        {"Other 5", (EventType.Draw, (int)EventSubtypeOther.RoomEnd)},

        {"NoMoreLives", (EventType.Draw, (int)EventSubtypeOther.NoMoreLives)},
        {"No More Lives", (EventType.Draw, (int)EventSubtypeOther.NoMoreLives)},
        {"Other_6", (EventType.Draw, (int)EventSubtypeOther.NoMoreLives)},
        {"Other 6", (EventType.Draw, (int)EventSubtypeOther.NoMoreLives)},

        {"AnimationEnd", (EventType.Draw, (int)EventSubtypeOther.AnimationEnd)},
        {"Animation End", (EventType.Draw, (int)EventSubtypeOther.AnimationEnd)},
        {"Other_7", (EventType.Draw, (int)EventSubtypeOther.AnimationEnd)},
        {"Other 7", (EventType.Draw, (int)EventSubtypeOther.AnimationEnd)},

        {"EndOfPath", (EventType.Draw, (int)EventSubtypeOther.EndOfPath)},
        {"End Of Path", (EventType.Draw, (int)EventSubtypeOther.EndOfPath)},
        {"Other_8", (EventType.Draw, (int)EventSubtypeOther.EndOfPath)},
        {"Other 8", (EventType.Draw, (int)EventSubtypeOther.EndOfPath)},

        {"NoMoreHealth", (EventType.Draw, (int)EventSubtypeOther.NoMoreHealth)},
        {"No More Health", (EventType.Draw, (int)EventSubtypeOther.NoMoreHealth)},
        {"Other_9", (EventType.Draw, (int)EventSubtypeOther.NoMoreHealth)},
        {"Other 9", (EventType.Draw, (int)EventSubtypeOther.NoMoreHealth)},


        //===================================================== USER EVENTS =========================================================================================

        {"User0", (EventType.Draw, (int)EventSubtypeOther.User0)},
        {"User1", (EventType.Draw, (int)EventSubtypeOther.User1)},
        {"User2", (EventType.Draw, (int)EventSubtypeOther.User2)},
        {"User3", (EventType.Draw, (int)EventSubtypeOther.User3)},
        {"User4", (EventType.Draw, (int)EventSubtypeOther.User4)},
        {"User5", (EventType.Draw, (int)EventSubtypeOther.User5)},
        {"User6", (EventType.Draw, (int)EventSubtypeOther.User6)},
        {"User7", (EventType.Draw, (int)EventSubtypeOther.User7)},
        {"User8", (EventType.Draw, (int)EventSubtypeOther.User8)},
        {"User9", (EventType.Draw, (int)EventSubtypeOther.User9)},
        {"User10", (EventType.Draw, (int)EventSubtypeOther.User10)},
        {"User11", (EventType.Draw, (int)EventSubtypeOther.User11)},
        {"User12", (EventType.Draw, (int)EventSubtypeOther.User12)},
        {"User13", (EventType.Draw, (int)EventSubtypeOther.User13)},
        {"User14", (EventType.Draw, (int)EventSubtypeOther.User14)},
        {"User15", (EventType.Draw, (int)EventSubtypeOther.User15)},
        {"User16", (EventType.Draw, (int)EventSubtypeOther.User16)},

        {"User 0", (EventType.Draw, (int)EventSubtypeOther.User0)},
        {"User 1", (EventType.Draw, (int)EventSubtypeOther.User1)},
        {"User 2", (EventType.Draw, (int)EventSubtypeOther.User2)},
        {"User 3", (EventType.Draw, (int)EventSubtypeOther.User3)},
        {"User 4", (EventType.Draw, (int)EventSubtypeOther.User4)},
        {"User 5", (EventType.Draw, (int)EventSubtypeOther.User5)},
        {"User 6", (EventType.Draw, (int)EventSubtypeOther.User6)},
        {"User 7", (EventType.Draw, (int)EventSubtypeOther.User7)},
        {"User 8", (EventType.Draw, (int)EventSubtypeOther.User8)},
        {"User 9", (EventType.Draw, (int)EventSubtypeOther.User9)},
        {"User 10", (EventType.Draw, (int)EventSubtypeOther.User10)},
        {"User 11", (EventType.Draw, (int)EventSubtypeOther.User11)},
        {"User 12", (EventType.Draw, (int)EventSubtypeOther.User12)},
        {"User 13", (EventType.Draw, (int)EventSubtypeOther.User13)},
        {"User 14", (EventType.Draw, (int)EventSubtypeOther.User14)},
        {"User 15", (EventType.Draw, (int)EventSubtypeOther.User15)},
        {"User 16", (EventType.Draw, (int)EventSubtypeOther.User16)},

        {"Other_10", (EventType.Draw, (int)EventSubtypeOther.User0)},
        {"Other_11", (EventType.Draw, (int)EventSubtypeOther.User1)},
        {"Other_12", (EventType.Draw, (int)EventSubtypeOther.User2)},
        {"Other_13", (EventType.Draw, (int)EventSubtypeOther.User3)},
        {"Other_14", (EventType.Draw, (int)EventSubtypeOther.User4)},
        {"Other_15", (EventType.Draw, (int)EventSubtypeOther.User5)},
        {"Other_16", (EventType.Draw, (int)EventSubtypeOther.User6)},
        {"Other_17", (EventType.Draw, (int)EventSubtypeOther.User7)},
        {"Other_18", (EventType.Draw, (int)EventSubtypeOther.User8)},
        {"Other_19", (EventType.Draw, (int)EventSubtypeOther.User9)},
        {"Other_20", (EventType.Draw, (int)EventSubtypeOther.User10)},
        {"Other_21", (EventType.Draw, (int)EventSubtypeOther.User11)},
        {"Other_22", (EventType.Draw, (int)EventSubtypeOther.User12)},
        {"Other_23", (EventType.Draw, (int)EventSubtypeOther.User13)},
        {"Other_24", (EventType.Draw, (int)EventSubtypeOther.User14)},
        {"Other_25", (EventType.Draw, (int)EventSubtypeOther.User15)},
        {"Other_26", (EventType.Draw, (int)EventSubtypeOther.User16)},

        {"Other 10", (EventType.Draw, (int)EventSubtypeOther.User0)},
        {"Other 11", (EventType.Draw, (int)EventSubtypeOther.User1)},
        {"Other 12", (EventType.Draw, (int)EventSubtypeOther.User2)},
        {"Other 13", (EventType.Draw, (int)EventSubtypeOther.User3)},
        {"Other 14", (EventType.Draw, (int)EventSubtypeOther.User4)},
        {"Other 15", (EventType.Draw, (int)EventSubtypeOther.User5)},
        {"Other 16", (EventType.Draw, (int)EventSubtypeOther.User6)},
        {"Other 17", (EventType.Draw, (int)EventSubtypeOther.User7)},
        {"Other 18", (EventType.Draw, (int)EventSubtypeOther.User8)},
        {"Other 19", (EventType.Draw, (int)EventSubtypeOther.User9)},
        {"Other 20", (EventType.Draw, (int)EventSubtypeOther.User10)},
        {"Other 21", (EventType.Draw, (int)EventSubtypeOther.User11)},
        {"Other 22", (EventType.Draw, (int)EventSubtypeOther.User12)},
        {"Other 23", (EventType.Draw, (int)EventSubtypeOther.User13)},
        {"Other 24", (EventType.Draw, (int)EventSubtypeOther.User14)},
        {"Other 25", (EventType.Draw, (int)EventSubtypeOther.User15)},
        {"Other 26", (EventType.Draw, (int)EventSubtypeOther.User16)},



        //===================================================== ASYNC EVENTS =========================================================================================
       
        {"AsyncImageLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"Async Image Loaded", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"Async ImageLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"ImageLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"Image Loaded", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"Other_60", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},
        {"Other 60", (EventType.Draw, (int)EventSubtypeOther.AsyncImageLoaded)},

        {"AsyncSoundLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"Async Sound Loaded", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"Async SoundLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"SoundLoaded", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"Sound Loaded", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"Other_61", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},
        {"Other 61", (EventType.Draw, (int)EventSubtypeOther.AsyncSoundLoaded)},

        {"AsyncHTTP", (EventType.Draw, (int)EventSubtypeOther.AsyncHTTP)},
        {"Async HTTP", (EventType.Draw, (int)EventSubtypeOther.AsyncHTTP)},
        {"HTTP", (EventType.Draw, (int)EventSubtypeOther.AsyncHTTP)},
        {"Other_62", (EventType.Draw, (int)EventSubtypeOther.AsyncHTTP)},
        {"Other 62", (EventType.Draw, (int)EventSubtypeOther.AsyncHTTP)},

        {"AsyncDialog", (EventType.Draw, (int)EventSubtypeOther.AsyncDialog)},
        {"Async Dialog", (EventType.Draw, (int)EventSubtypeOther.AsyncDialog)},
        {"Dialog", (EventType.Draw, (int)EventSubtypeOther.AsyncDialog)},
        {"Other_63", (EventType.Draw, (int)EventSubtypeOther.AsyncDialog)},
        {"Other 63", (EventType.Draw, (int)EventSubtypeOther.AsyncDialog)},

        {"AsyncIAP", (EventType.Draw, (int)EventSubtypeOther.AsyncIAP)},
        {"Async IAP", (EventType.Draw, (int)EventSubtypeOther.AsyncIAP)},
        {"IAP", (EventType.Draw, (int)EventSubtypeOther.AsyncIAP)},
        {"Other_66", (EventType.Draw, (int)EventSubtypeOther.AsyncIAP)},
        {"Other 66", (EventType.Draw, (int)EventSubtypeOther.AsyncIAP)},

        {"AsyncCloud", (EventType.Draw, (int)EventSubtypeOther.AsyncCloud)},
        {"Async Cloud", (EventType.Draw, (int)EventSubtypeOther.AsyncCloud)},
        {"Cloud", (EventType.Draw, (int)EventSubtypeOther.AsyncCloud)},
        {"Other_67", (EventType.Draw, (int)EventSubtypeOther.AsyncCloud)},
        {"Other 67", (EventType.Draw, (int)EventSubtypeOther.AsyncCloud)},

        {"AsyncNetworking", (EventType.Draw, (int)EventSubtypeOther.AsyncNetworking)},
        {"Async Networking", (EventType.Draw, (int)EventSubtypeOther.AsyncNetworking)},
        {"Networking", (EventType.Draw, (int)EventSubtypeOther.AsyncNetworking)},
        {"Other_68", (EventType.Draw, (int)EventSubtypeOther.AsyncNetworking)},
        {"Other 68", (EventType.Draw, (int)EventSubtypeOther.AsyncNetworking)},

        {"AsyncSteam", (EventType.Draw, (int)EventSubtypeOther.AsyncSteam)},
        {"Async Steam", (EventType.Draw, (int)EventSubtypeOther.AsyncSteam)},
        {"Steam", (EventType.Draw, (int)EventSubtypeOther.AsyncSteam)},
        {"Other_69", (EventType.Draw, (int)EventSubtypeOther.AsyncSteam)},
        {"Other 69", (EventType.Draw, (int)EventSubtypeOther.AsyncSteam)},

        {"AsyncSocial", (EventType.Draw, (int)EventSubtypeOther.AsyncSocial)},
        {"Async Social", (EventType.Draw, (int)EventSubtypeOther.AsyncSocial)},
        {"Social", (EventType.Draw, (int)EventSubtypeOther.AsyncSocial)},
        {"Other_70", (EventType.Draw, (int)EventSubtypeOther.AsyncSocial)},
        {"Other 70", (EventType.Draw, (int)EventSubtypeOther.AsyncSocial)},

        {"AsyncPushNotification", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"Async Push Notification", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"Async PushNotification", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"PushNotification", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"Push Notification", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"Other_71", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},
        {"Other 71", (EventType.Draw, (int)EventSubtypeOther.AsyncPushNotification)},

        {"AsyncSaveAndLoad", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Async SaveAndLoad", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Async Save And Load", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Async Save Load", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"AsyncSaveLoad", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Async SaveLoad", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"SaveAndLoad", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Save And Load", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Save Load", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Other_72", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},
        {"Other 72", (EventType.Draw, (int)EventSubtypeOther.AsyncSaveAndLoad)},

        {"AsyncAudioRecording", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"Async Audio Recording", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"Async AudioRecording", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"AudioRecording", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"Audio Recording", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"Other_73", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},
        {"Other 73", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioRecording)},

        {"AsyncAudioPlayback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Async Audio Playback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Async AudioPlayback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Async Playback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"AsyncPlayback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"AudioPlayback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Audio Playback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Playback", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Other_74", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},
        {"Other 74", (EventType.Draw, (int)EventSubtypeOther.AsyncAudioPlayback)},

        {"AsyncSystem", (EventType.Draw, (int)EventSubtypeOther.AsyncSystem)},
        {"Async System", (EventType.Draw, (int)EventSubtypeOther.AsyncSystem)},
        {"System", (EventType.Draw, (int)EventSubtypeOther.AsyncSystem)},
        {"Other_75", (EventType.Draw, (int)EventSubtypeOther.AsyncSystem)},
        {"Other 75", (EventType.Draw, (int)EventSubtypeOther.AsyncSystem)},
    };




}