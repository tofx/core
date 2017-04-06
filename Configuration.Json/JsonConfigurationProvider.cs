using TOF.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TOF.Core.Configuration.Json
{
    public class JsonConfigurationProvider : ConfigurationProvider
    {
        private IDictionary<string, string> _configurationValues = null;
        private Stack<string> _keyStack = null;
        private string _currentPath = null;
        private JsonReader _reader = null;

        public string Path { get; private set; }
        public bool Optional { get; private set; }

        public JsonConfigurationProvider(string Path) : this(Path, false)
        {
        }

        public JsonConfigurationProvider(string Path, bool Optional)
        {
            ParameterChecker.NotNullOrEmpty(Path);

            this._configurationValues = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this._keyStack = new Stack<string>();
            this.Path = Path;
            this.Optional = Optional;
        }

        public override void Load()
        {
            if (File.Exists(this.Path))
            {
                var stream = File.OpenRead(this.Path);
                this.Load(stream);
                this.ConfigurationValues = this._configurationValues;
            }
            else
            {
                if (!this.Optional)
                    throw new FileNotFoundException("ERROR_JSON_CONFIGURATION_FILE_NOT_FOUND");
            }
        }
        
        private void Load(Stream stream)
        {
            this._reader = new JsonTextReader(new StreamReader(stream));
            this._reader.DateParseHandling = DateParseHandling.None;
            var o = JObject.Load(this._reader);
            TraverseObject(o);
            this._reader.Close();
        }

        private void TraverseObject(JObject o)
        {
            foreach (var p in o.Properties())
            {
                this.EnterNextLevel(p.Name);
                this.TraverseProperty(p);
                this.LeaveLevel();
            }
        }

        private void TraverseProperty(JProperty property)
        {
            this.TraverseToken(property.Value);
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
            string key = this._currentPath;

            if (this._configurationValues.ContainsKey(key))
                throw new InvalidOperationException("ERROR_DUPLICATED_KEY_FOUND");

            this._configurationValues.Add(key, token.Value<string>());
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
