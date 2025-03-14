namespace Rebellis
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "RebellisSetting", menuName = "Rebellis/Setting")]
    public class RebellisSetting : ScriptableObject
    {
        public static string BASEAPI = "https://api.rebellis.ai";

        public string username;

        public string password;

        private string token;

        public RebellisUserModel userdata;

        public Rebelliprompts Rebelliprompts;

        public AnimationData AnimationData;

        public string GetUsername()
        {
            return username;
        }

        public string GetPassword()
        {
            return password;
        }

        public string GetToken()
        {
            return token;
        }

        public void SetToken(string token)
        {
            this.token = token;
        }
    }
}