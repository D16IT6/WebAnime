using System;

namespace WebAnime.MVC.Helpers.Session
{
    [Serializable]
    public class UserSession
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}