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
    }
}