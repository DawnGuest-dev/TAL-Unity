using System;

[Serializable]
public class DawnHttpResponse : HttpResponse
{
    [Serializable]
    public class DawnProfileResponse
    {
        public int id;
        public string username;
        public string created_at;
        public string refresh_token;
    }
}
