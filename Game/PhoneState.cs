using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class PhoneState
    {

        public static readonly string gameStateFile = "gamestate.data";
        private static bool hasrestorestate = false;
        private static bool hasstorestate = false;

        /// <summary>
        /// 读取程序状态
        /// </summary>
        public static void RestoreState()
        {
            if (hasrestorestate) return;

            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.FileExists(gameStateFile))
                return;
            using (IsolatedStorageFileStream stream = store.OpenFile(gameStateFile, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    PhoneSetting setting = PhoneSetting.GetInstance();
                    setting.HighestSocre = reader.ReadInt32();
                    setting.Cube = reader.ReadInt32();
                    setting.DisplayNumber = reader.ReadBoolean();
                }
            }
            store.DeleteFile(gameStateFile);

            hasrestorestate = true;
            hasstorestate = false;
        }

        /// <summary>
        /// 保存当前程序状态
        /// </summary>
        public static void StoreState()
        {
            if (hasstorestate) return;

            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream stream = store.CreateFile(gameStateFile))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    PhoneSetting setting = PhoneSetting.GetInstance();
                    writer.Write(setting.HighestSocre);
                    writer.Write(setting.Cube);
                    writer.Write(setting.DisplayNumber);
                }
            }

            hasstorestate = true;
            hasrestorestate = false;
        }
    }
}
