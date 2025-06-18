using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;
    void Start()
    {
        Init();
    }

    // 파이어베이스 내 프로젝트에 연결
    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공");
                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;
                
                Login();
            }
            else
            {
                UnityEngine.Debug.LogError($"파이어 베이스 연결에 실패했습니다. ${dependencyStatus}");
            }
        });
    }

    private void TryRegister()
    {
        string email = "bibijh9988@naver.com";
        string password = "123456";
        
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"회원 가입에 실패했습니다: ${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원 가입에 성공했습니다!: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            return;
        });
    }

    private void Login()
    {
        string email = "bibijh9988@naver.com";
        string password = "123456";

        
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"로그인에 실패했습니다 : {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그인에 성공했습니다!: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            
            NicknameChange();
            AddMyRanking();
            GetMyRanking();
        });
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;

        if (user == null)
        {
            return;
        }

        var profile = new UserProfile
        {
            DisplayName = "Teemo"
        };

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted) {
                Debug.LogError($"닉네임 변경에 실패했습니다 : {task.Exception.Message}");
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        FirebaseUser user = _auth.CurrentUser;
        if (user != null) return;
        
        string nickname = user.DisplayName;
        string email = user.Email;
        
        Account account = new Account(email, nickname, "fireBase");
    }

    private void AddMyRanking()
    {
        RankingM ranking = new RankingM("hu@gmail.com", "huhuhu", 5400);

        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.Score },
        };
        
        _db.Collection("rankings").AddAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"데이터 추가가 실패했습니다.: {task.Exception.Message}");
                return;
            }
        });
        
        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            Debug.Log(String.Format("Added or Updated document with ID: {0}.", task.Id));
        });
    }

    private void GetMyRanking()
    {
        var email = "hu@gmail.com"; // ID 역할

        var docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;
            
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                var rankingDict = snapshot.ToDictionary();
                foreach (var pair in rankingDict)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
}