using System.Collections.Generic;

public class RankingRepositoryM
{
    public List<RankingSaveDataM> Load()
    {
        // 데이터가 아직 없지만 ..
        // 개발 단계에서 데이터가 필요하다면.. Mocking 기법을 쓴다.
        // PlayerPrefs대신 '가짜 데이터 반환'
        // 이준엽 : "GTP가 모킹데이터 잘 만들어준다! 100개 만들어보자"
        return new List<RankingSaveDataM>()
        {
            new RankingSaveDataM(1813, "test1@test.com", "냉철한토끼65"),
            new RankingSaveDataM(2721, "test2@test.com", "빛나는호랑이922"),
            new RankingSaveDataM(2960, "test3@test.com", "달콤한햄스터489"),
            new RankingSaveDataM(2263, "test4@test.com", "따뜻한토끼754"),
            new RankingSaveDataM(1552, "test5@test.com", "귀여운여우451"),
            new RankingSaveDataM(2086, "test6@test.com", "행복한고양이621"),
            new RankingSaveDataM(2065, "test7@test.com", "따뜻한햄스터558"),
            new RankingSaveDataM(1211, "test8@test.com", "우주고양이980"),
            new RankingSaveDataM(2139, "test9@test.com", "무서운사자570"),
            new RankingSaveDataM(1469, "test10@test.com", "우주토끼732"),
            new RankingSaveDataM(2786, "test11@test.com", "행복한여우85"),
            new RankingSaveDataM(2850, "test12@test.com", "귀여운토끼586"),
            new RankingSaveDataM(3100, "test13@test.com", "냉철한늑대739"),
            new RankingSaveDataM(3034, "test14@test.com", "냉철한여우560"),
            new RankingSaveDataM(2446, "test15@test.com", "빛나는고양이474"),
            new RankingSaveDataM(3175, "test16@test.com", "따뜻한여우884"),
            new RankingSaveDataM(1056, "test17@test.com", "귀여운곰674"),
            new RankingSaveDataM(2492, "test18@test.com", "행복한햄스터538"),
            new RankingSaveDataM(1527, "test19@test.com", "미친여우342"),
            new RankingSaveDataM(2710, "test20@test.com", "달콤한토끼618"),
            new RankingSaveDataM(2869, "test21@test.com", "행복한호랑이271"),
            new RankingSaveDataM(2825, "test22@test.com", "행복한곰35"),
            new RankingSaveDataM(1045, "test23@test.com", "고요한고양이417"),
            new RankingSaveDataM(2503, "test24@test.com", "행복한고양이796"),
            new RankingSaveDataM(2992, "test25@test.com", "무서운곰112"),
            new RankingSaveDataM(2265, "test26@test.com", "달콤한고양이669"),
            new RankingSaveDataM(2060, "test27@test.com", "우주햄스터18"),
            new RankingSaveDataM(1108, "test28@test.com", "따뜻한사자117"),
            new RankingSaveDataM(1762, "test29@test.com", "미친판다565"),
            new RankingSaveDataM(1589, "test30@test.com", "우주고양이491"),
            new RankingSaveDataM(2546, "test31@test.com", "귀여운늑대579"),
            new RankingSaveDataM(2895, "test32@test.com", "행복한여우38"),
            new RankingSaveDataM(3134, "test33@test.com", "미친햄스터637"),
            new RankingSaveDataM(1165, "test34@test.com", "미친햄스터526"),
            new RankingSaveDataM(3341, "test35@test.com", "따뜻한사자758"),
            new RankingSaveDataM(3130, "test36@test.com", "무서운늑대159"),
            new RankingSaveDataM(2464, "test37@test.com", "냉철한호랑이995"),
            new RankingSaveDataM(1229, "test38@test.com", "달콤한사자524"),
            new RankingSaveDataM(1641, "test39@test.com", "귀여운판다495"),
            new RankingSaveDataM(2241, "test40@test.com", "고요한여우971"),
            new RankingSaveDataM(1936, "test41@test.com", "우주곰490"),
            new RankingSaveDataM(1521, "test42@test.com", "무서운여우785"),
            new RankingSaveDataM(1584, "test43@test.com", "따뜻한늑대857"),
            new RankingSaveDataM(1996, "test44@test.com", "귀여운곰189"),
            new RankingSaveDataM(2161, "test45@test.com", "우주독수리75"),
            new RankingSaveDataM(1729, "test46@test.com", "귀여운곰394"),
            new RankingSaveDataM(2398, "test47@test.com", "미친호랑이739"),
            new RankingSaveDataM(2360, "test48@test.com", "따뜻한곰721"),
            new RankingSaveDataM(1865, "test49@test.com", "빛나는햄스터343"),
            new RankingSaveDataM(3020, "test50@test.com", "냉철한호랑이494"),
            new RankingSaveDataM(1697, "test100@test.com", "행복한토끼588"),
        };
    }
}


public class RankingSaveDataM
{
    public int Score;
    public string Email;
    public string NickName;

    public RankingSaveDataM(int score, string email, string nickName)
    {
        Score = score;
        Email = email;
        NickName = nickName;
    }
}