using tofx.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tofx.Core.Configuration.Json
{
    public class JsonConfigurationProvider : ConfigurationProvider
    {
        private IDictionary<string, string> _configurationValues = null;
        private Stack<string> _keyStack = null;
        private string _currentPath = null;
        private JsonReader _reader = null;

        public string Path { get; private set; }
        public bool Optional { get; private set; }

        public JsonConfigurationProvider(string path) : this(path, false)
        {
        }

        public JsonConfigurationProvider(string path, bool optional)
        {
            ParameterChecker.NotNullOrEmpty(path);

            _configurationValues = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _keyStack = new Stack<string>();
            Path = path;
            Optional = optional;
        }

        public override void Load()
        {
            if (File.Exists(Path))
            {
                var stream = File.OpenRead(Path);
                Load(stream);
                ConfigurationValues = _configurationValues;
            }
            else
            {
                if (!Optional)
                    throw new FileNotFoundException("ERROR_JSON_CONFIGURATION_FILE_NOT_FOUND");
            }
        }
        
        private void Load(Stream stream)
        {
            _reader = new JsonTextReader(new StreamReader(stream));
            _reader.DateParseHandling = DateParseHandling.None;
            var o = JObject.Load(_reader);
            TraverseObject(o);
            _reader.Close();
        }

        private void TraverseObject(JObject o)
        {
            foreach (var p in o.Properties())
            {
                EnterNextLevel(p.Name);
                TraverseProperty(p);
                LeaveLevel();
            }
        }

        private void TraverseProperty(JProperty property)
        {
            TraverseToken(property.Value);
        }

        private void TraverseToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    TraverseObject(token.Value<JObject>());
                    break;

                case JTokenType.Array:
                    TraverseArray(token.Value<JArray>());
                    break;

                case JTokenType.Boolean:
                case JTokenType.Bytes:
                case JTokenType.Float:
                case JTokenType.Integer:
                case JTokenType.Raw:
                case JTokenType.Null:
                case JTokenType.String:
                    TraverseValue(token);
                    break;

                default:
                    throw new FormatException("ERROR_UNSUPPORTED_TYPE_FOUND");
            }
        }

        private void TraverseArray(JArray array)
        {
            for (int i=0; i<array.Count; i++)
            {
                EnterNextLevel(i.ToString());
                TraverseToken(array[i]);
                LeaveLevel();
            }
        }

        private void TraverseValue(JToken token)
        {
            string key = _currentPath;

            if (_configurationValues.ContainsKey(key))
                throw new InvalidOperationException("ERROR_DUPLICATED_KEY_FOUND");

            _configurationValues.Add(key, token.Value<string>());
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
