using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordCheckInputField;
    public Button ConfirmButton;
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject CreateAccountPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;
    
    [Header("회원가입")]
    public UI_InputFields CreateAccountInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "29847920";
    
    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        LoginPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
        
        LoginInputFields.ResultText.text    = string.Empty;
        CreateAccountInputFields.ResultText.text = string.Empty;

        LoginCheck();
    }

    // 회원가입하기 버튼 클릭
    public void OnClickGoToResisterButton()
    {
        LoginPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
    }
    
    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
    }


    // 회원가입
    public void Resister()
    {
        // 1. 이메일 도메인 규칙을 확인한다.
        string email = CreateAccountInputFields.EmailInputField.text;
        if (string.IsNullOrEmpty(email))
        {
            CreateAccountInputFields.ResultText.text = "이메일를 입력해주세요.";
            return;
        }
        
        // 2. 닉네임 도메인 규칙을 확인한다.
        string nickname = CreateAccountInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            CreateAccountInputFields.ResultText.text = nicknameSpecification.ErrorMessage;
            return;
        }
        
        // 2. 1차 비밀번호 입력을 확인한다.
        string password = CreateAccountInputFields.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            CreateAccountInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 3. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        string password2 = CreateAccountInputFields.PasswordCheckInputField.text;
        if (string.IsNullOrEmpty(password2))
        {
            CreateAccountInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        if (password != password2)
        {
            CreateAccountInputFields.ResultText.text = "비밀번혹가 다릅니다.";
            return;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        if (result.IsSuccess)
        {
            OnClickGoToLoginButton();
        }
        else
        {
            CreateAccountInputFields.ResultText.text = result.Message;
        }
    }


    public void Login()
    {
        // 1. 이메일 입력을 확인한다.
        string email = LoginInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            LoginInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            LoginInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            return;
        }

        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = "이메일이 중복되었습니다.";
        }
    }
    
    
    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만
    public void LoginCheck()
    {
        string email = LoginInputFields.EmailInputField.text;
        string password = LoginInputFields.PasswordInputField.text;
        
        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
    }
}
