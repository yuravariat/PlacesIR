using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR
{
    [Serializable]
    public class ValidationResponse<T>
    {
        private T obj;
        private bool isValid;
        private ValidationError errors;
        private ValidationError warnings;

        public T Obj
        {
            get { return obj; }
            set { obj = value; }
        }

        public bool IsValid
        {
            get
            {
                isValid = Errors.Count == 0;
                return isValid;
            }
        }

        public ValidationError Errors
        {
            get
            {
                if (errors == null)
                {
                    errors = new ValidationError();
                }
                return errors;
            }
            set
            {
                errors = value;
            }
        }

        public ValidationError Warnings
        {
            get
            {
                if (warnings == null)
                {
                    warnings = new ValidationError();
                }
                return warnings;
            }
            set
            {
                warnings = value;
            }
        }
    }
    [Serializable]
    public class ValidationError : Dictionary<string, string>
    {
        public void AddError(string key, string value, Exception ex = null, Level level = Level.Info)
        {
            if (!base.ContainsKey(key))
            {
                LogHandler.WriteLog(key, value, ex, level);
                base.Add(key, value);
            }
        }
        public void AddWarning(string key, string value, Exception ex = null, Level level = Level.Warn)
        {
            if (!base.ContainsKey(key))
            {
                LogHandler.WriteLog(key, value, ex, level);
                base.Add(key, value);
            }
        }
        public void AddErrors(Dictionary<string, string> errors)
        {
            foreach (var item in errors)
            {
                if (!base.ContainsKey(item.Key))
                {
                    base.Add(item.Key, item.Value);
                }
            }
        }
    }
}