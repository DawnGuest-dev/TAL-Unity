using System;

[Serializable]
public class UserData
{
    public string UserId { get; set; } // 유저 ID
    public string UserName { get; set; } // 유저 이름
    public int Level { get; set; } // 유저 레벨
    public int Experience { get; set; } // 경험치

    public UserData(string userId, string userName, int level, int experience)
    {
        UserId = userId;
        UserName = userName;
        Level = level;
        Experience = experience;
    }

    public override string ToString()
    {
        return $"UserId: {UserId}, UserName: {UserName}, Level: {Level}";
    }
}
