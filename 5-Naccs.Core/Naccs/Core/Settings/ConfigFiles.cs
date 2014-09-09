namespace Naccs.Core.Settings
{
    using System;

    public class ConfigFiles
    {
        public static FileSave fileSave;
        public static GStampSettings gStamp;
        public static HelpSettings helpSettings;
        public static MessageClassify messageClassify;
        public static OptionCertification optionCertification;
        public static PathInfo pathInfo;
        public static PrintSetups printerSettings;
        public static ReceiveNotice receiveNotice;
        public static SystemEnvironment systemEnvironment;
        public static TermMessage termMessage;
        public static User user;
        public static UserEnvironment userEnvironment;
        public static UserKey userKey;

        public static void AllInitialize()
        {
            AllLoad();
            user.Initialize();
            pathInfo.SettingDiv = user.Application.SettingsDiv;
            userEnvironment.Initialize();
            optionCertification.Initialize();
            printerSettings.Initialize();
            messageClassify.Initialize();
            helpSettings.Initialize();
            fileSave.Initialize();
            receiveNotice.Initialize();
            userKey.Initialize();
            gStamp.Initialize();
        }

        public static void AllLoad()
        {
            pathInfo = PathInfo.CreateInstance();
            systemEnvironment = SystemEnvironment.CreateInstance();
            user = User.CreateInstance();
            userEnvironment = UserEnvironment.CreateInstance();
            optionCertification = OptionCertification.CreateInstance();
            printerSettings = PrintSetups.CreateInstance();
            messageClassify = MessageClassify.CreateInstance();
            helpSettings = HelpSettings.CreateInstance();
            fileSave = FileSave.CreateInstance();
            receiveNotice = ReceiveNotice.CreateInstance();
            userKey = UserKey.CreateInstance();
            termMessage = TermMessage.CreateInstance();
            gStamp = GStampSettings.CreateInstance();
        }

        public static void AllReset()
        {
            pathInfo = PathInfo.ResetInstance();
            systemEnvironment = SystemEnvironment.ResetInstance();
            user = User.ResetInstance();
            userEnvironment = UserEnvironment.ResetInstance();
            optionCertification = OptionCertification.ResetInstance();
            printerSettings = PrintSetups.ResetInstance();
            messageClassify = MessageClassify.ResetInstance();
            helpSettings = HelpSettings.ResetInstance();
            fileSave = FileSave.ResetInstance();
            receiveNotice = ReceiveNotice.ResetInstance();
            userKey = UserKey.ResetInstance();
            termMessage = TermMessage.ResetInstance();
            gStamp = GStampSettings.ResetInstance();
        }

        public static bool AllSave()
        {
            if (AllSave(true))
            {
                return false;
            }
            return true;
        }

        public static bool AllSave(bool updateCheck)
        {
            AllLoad();
            if ((!updateCheck || printerSettings.UpdateFlg) && !printerSettings.Save())
            {
                return false;
            }
            if ((!updateCheck || messageClassify.UpdateFlg) && !messageClassify.Save())
            {
                return false;
            }
            if ((!updateCheck || fileSave.UpdateFlg) && !fileSave.Save())
            {
                return false;
            }
            if ((!updateCheck || receiveNotice.UpdateFlg) && !receiveNotice.Save())
            {
                return false;
            }
            if ((!updateCheck || userKey.UpdateFlg) && !userKey.Save())
            {
                return false;
            }
            if ((!updateCheck || user.UpdateFlg) && !user.Save())
            {
                return false;
            }
            if ((!updateCheck || userEnvironment.UpdateFlg) && !userEnvironment.Save())
            {
                return false;
            }
            if ((!updateCheck || optionCertification.UpdateFlg) && !optionCertification.Save())
            {
                return false;
            }
            if ((!updateCheck || helpSettings.UpdateFlg) && !helpSettings.Save())
            {
                return false;
            }
            if ((!updateCheck || gStamp.UpdateFlg) && !gStamp.Save())
            {
                return false;
            }
            return true;
        }
    }
}

