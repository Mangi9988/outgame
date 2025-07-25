using System;

public class RankingM
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }

    public RankingM(string email, string nickname, int score)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }
        

        var nickNameSpecification = new AccountNicknameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }

        if (score < 0)
        {
            throw new Exception("올바르지 못한 점수입니다.");
        }
        
        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public void SetRank(int rank)
    {
        if (rank <= 0)
        {
            throw new Exception("올바르지 못한 등수입니다.");
        }
        
        Rank = rank;
    }

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            throw new Exception("올바르지 못한 점수입니다.");   
        }
        
        Score += score;
    }
    
    public RankingDTOM ToDTO()
    {
        return new RankingDTOM(this);
    }
}
