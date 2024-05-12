using System.Collections.Generic;
using System.Diagnostics;

namespace HLTVScrapperAPI.Models
{
    public class NestedDictionary : Dictionary<string, object>
    {
        public void Add(string key, string value)
        {
            base.Add(key, value);
        }

        public void Add(string key, Dictionary<string, string> value)
        {
            base.Add(key, value);
        }

        public void Print()
        {
            PrintNestedDictionary(this);
        }

        private void PrintNestedDictionary(Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
            {
                if (pair.Value is string)
                {
                    Debug.WriteLine($"{pair.Key}: {pair.Value}");
                }
                else if (pair.Value is Dictionary<string, object>)
                {
                    PrintNestedDictionary((Dictionary<string, object>)pair.Value);
                }
                else if (pair.Value is Dictionary<string, string>)
                {
                    foreach (var innerPair in (Dictionary<string, string>)pair.Value)
                    {
                        Debug.WriteLine($"{innerPair.Key}: {innerPair.Value}");
                    }
                }
            }
        }
    }
}
