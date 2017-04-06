﻿using TOF.Core.Utils;
using System;

namespace TOF.Core.DependencyInjection
{
    /// <summary>
    /// 相依註冊初始標記，若需要在應用程式啟動時註冊組件與物件相依性時，必須要使用這個標記在組件內標記初始物件。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class StartupAttribute : Attribute
    {
        /// <summary>
        /// 初始物件型別。
        /// </summary>
        public Type StartupType { get; private set; }
        /// <summary>
        /// 初始物件啟動方法，若沒有給與此值，預設為 "Initialize"。
        /// </summary>
        public string StartupMethod { get; private set; }

        /// <summary>
        /// 建構式，傳入初始物件的型別。
        /// </summary>
        /// <param name="StartupType">初始物件的型別。</param>
        public StartupAttribute(Type StartupType)
        {
            ParameterChecker.NotNull(StartupType);
            this.StartupType = StartupType;
            this.StartupMethod = "Initialize";
        }

        /// <summary>
        /// 建構式，傳入初始物件的型別與初始的方法名稱。
        /// </summary>
        /// <param name="StartupType">初始物件的型別。</param>
        /// <param name="StartupMethod">初始的方法名稱。</param>
        public StartupAttribute(Type StartupType, string StartupMethod)
        {
            ParameterChecker.NotNull(StartupType);
            ParameterChecker.NotNullOrEmpty(StartupMethod);

            this.StartupType = StartupType;
            this.StartupMethod = StartupMethod;
        }
    }
}
