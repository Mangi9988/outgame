using System;
using System.Text.RegularExpressions;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;



    // 닉네임: 한글 또는 영어로 구성, 2~12자
    private static readonly Regex NicknameRegex = new Regex(@"^[가-힣a-zA-Z]{2,12}$", RegexOptions.Compiled);

    // 금지된 닉네임 (비속어 등)
    private static readonly string[] ForbiddenNicknames = { "바보", "멍청이", "운영자", "김홍일" };

    public Account(string email, string nickname, string password)
    {
        // 규칙을 객체로 캡슐화해서 분리한다
        // 그래서 도메인과 UI는 모두 "이 규칙을 만족하니?"를 물으면 된다.
        // 캡슐화한 규칙 : 명세

        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }
        

        // 닉네임 검증
        var nickNameSpecification = new AccountNicknameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }

        // 비밀번호 검증
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("비밀번호는 비어있을 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }
    
    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}