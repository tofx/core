using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace TOF.Core.Configuration.DotNet
{
    public class DotNetConfigurationProvider : ConfigurationProvider
    {
        private IDictionary<string, string> _configurationValues = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Stack<string> _keyStack = new Stack<string>();
        private string _currentPath = null;
        
        public override void Set(string Key, string Value)
        {
            // do not implement because unusable.
        }

        public override void Load()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            var appSettings = ConfigurationManager.AppSettings;

            EnterNextLevel("connectionStrings");

            for (int i = 0; i < connectionStrings.Count; i++)
            {
                EnterNextLevel(connectionStrings[i].Name);
                TraverseValue(connectionStrings[i].ConnectionString);
                LeaveLevel();
            }

            LeaveLevel();

            EnterNextLevel("appSettings");

            for (int i = 0; i < appSettings.Count; i++)
            {
                EnterNextLevel(appSettings.GetKey(i));
                TraverseValue(appSettings[appSettings.GetKey(i)]);
                LeaveLevel();
            }

            LeaveLevel();

            this.ConfigurationValues = this._configurationValues;
        }

        private void TraverseValue(string Value)
        {
            string key = this._currentPath;

            if (this._configurationValues.ContainsKey(key))
                throw new InvalidOperationException("ERROR_DUPLICATED_KEY_FOUND");

            this._configurationValues.Add(key, Value);
        }

        private void EnterNextLevel(string PropertyName)
        {
            this._keyStack.Push(PropertyName);
            this._currentPath = string.Join(Constants.Delimiter, this._keyStack.Reverse());
        }

        private void LeaveLevel()
        {
            this._keyStack.Pop();
            this._currentPath = string.Join(Constants.Delimiter, this._keyStack.Reverse());
        }
    }
}
