using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Plugin
{
    public static class LibraryApi
    {
        /// <summary>
        /// 插件api
        /// </summary>
        public interface openapi
        {
            /// <summary>
            /// 名稱
            /// </summary>
            String Name { get; }

            /// <summary>
            /// 作者
            /// </summary>
            String Auth { get; }

            /// <summary>
            /// 網址
            /// </summary>
            String Url { get; }

            /// <summary>
            /// 解密
            /// </summary>
            /// <param name="md5">MD5</param>
            /// <returns></returns>
            String Decryption(string md5);
            /// <summary>
            /// 輸入
            /// </summary>
            String Input { set; get; }
            /// <summary>
            /// 輸出
            /// </summary>
            String Ouput { set; get; }
        }
    }
}
