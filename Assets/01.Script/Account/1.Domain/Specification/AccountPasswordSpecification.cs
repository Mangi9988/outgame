using UnityEngine;

public class AccountPasswordSpecification : MonoBehaviour
{ 
    public bool IsSatisfiedBy(string value)
    {
        // 비밀번호 검증
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = ("비밀번호는 비어있을 수 없습니다.");
        }

        if (value.Length < 6 || value.Length > 12)
        {
            ErrorMessage = ("비밀번호는 6자 이상 12자 이하이어야 합니다.");
        }
        
        return true;
    }
    
    public string ErrorMessage { get; private set; }
}
