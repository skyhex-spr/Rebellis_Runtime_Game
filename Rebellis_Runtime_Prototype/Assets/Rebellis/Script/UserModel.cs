
namespace Rebellis
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Credit
    {
        public int balance;
        public int id;
    }

    [Serializable]
    public class RebellisUserModel
    {
        public string access_token;
        public string refresh_token;
        public string token_type;
        public User user;
    }

    [Serializable]
    public class User
    {
        public int id;
        public string email;
        public string first_name;
        public string last_name;
        public bool is_active;
        public Credit credit;
    }
}