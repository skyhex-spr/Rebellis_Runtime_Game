namespace Rebellis
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "RebellisSetting", menuName = "Rebellis/Setting")]
    public class RebellisSetting : ScriptableObject
    {
        public static string BASEAPI = "https://api.rebellis.ai";

        public string email;

        public string password;

        public RebellisUserModel userdata;

        public Rebelliprompts Rebelliprompts;

       // public AnimationData AnimationData;

    }
}