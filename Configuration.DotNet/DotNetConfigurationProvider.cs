using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace tofx.Core.Configuration.DotNet
{
    public class DotNetConfigurationProvider : ConfigurationProvider
    {
        private IDictionary<string, string> _configurationValues = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Stack<string> _keyStack = new Stack<string>();
        private string _currentPath = null;
        
        public override void Set(string key, string value)
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

            ConfigurationValues = _configurationValues;
        }

        private void TraverseValue(string value)
        {
            string key = _currentPath;

            if (_configurationValues.ContainsKey(key))
                throw new InvalidOperationException("ERROR_DUPLICATED_KEY_FOUND");

            _configurationValues.Add(key, value);
        }

        private void EnterNextLevel(string propertyName)
        {
            _keyStack.Push(propertyName);
            _currentPath = string.Join(Constants.Delimiter, _keyStack.Reverse());
        }

        private void LeaveLevel()
        {
            _keyStack.Pop();
            _currentPath = string.Join(Constants.Delimiter, _keyStack.Reverse());
        }
    }
}
