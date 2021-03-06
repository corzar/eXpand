using System;
using System.Collections.Generic;
using DevExpress.Xpo.Metadata;

namespace eXpand.Xpo.Converters.ValueConverters
{
    /// <summary>
    /// Summary description for DictionaryValueConverter.
    /// </summary>
    public class DictionaryValueConverter : ValueConverter
    {
        private readonly string keyDelimeter = "ﮙ";
        private readonly string delimeter = "";
        

        public override Type StorageType
        {
            get { return typeof (string); }
        }

        public override object ConvertToStorageType(object value)
        {
            if (value == null) return null;
            string s = null;
            foreach (KeyValuePair<string, string> o in ((Dictionary<string, string>) value))
            {
                    s += o.Key+keyDelimeter+o.Value + delimeter;
            }
            
            if (s != null) return s.TrimEnd(delimeter.ToCharArray());
            return null;
        }

        public override object ConvertFromStorageType(object value)
        {
            if (value == null) return null;
            string[] split = value.ToString().Split(delimeter.ToCharArray());
            if (value.ToString().IndexOf(keyDelimeter) > -1)
            {
                var hashtable = new Dictionary<string, string>();
                foreach (string s in split)
                {
                    string[] strings = s.Split(keyDelimeter.ToCharArray());
                    hashtable.Add(strings[0].TrimStart('['), strings.Length == 1 ? null : strings[1].Trim().TrimEnd(']'));
                }
                return hashtable;
            }
            return new Dictionary<string, string>();
        }
    }
}