using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Game.Plugin
{
    public class DllPlugin
    {

        /// <summary>
        /// 輸入
        /// </summary>
        public String Input { set; get; }
        /// <summary>
        /// 輸出
        /// </summary>
        public String Ouput { set; get; }
        /// <summary>
        /// 装载dll插件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadPlugin(String file)
        {
            Assembly dll = Assembly.LoadFile(file);
            foreach (var _every in dll.GetTypes())
            {
                if (_every.GetInterface(typeof(LibraryApi.openapi).Name) != null)
                {
                    LibraryApi.openapi api = System.Activator.CreateInstance(_every) as LibraryApi.openapi;
                    api.Input = Input;
                    Ouput=api.Ouput;
                    return true;
                }
            }
            return false;
        }
    }
}
