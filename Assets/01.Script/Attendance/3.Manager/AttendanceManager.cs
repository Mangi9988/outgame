using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;

    [SerializeField]
    private List<AttendanceSO> _attendanceSoList;
    
    private List<Attendance> _attendances = new List<Attendance>();

    
    public event Action OnDataChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        Init();
    }

    private void Init()
    {
        _attendances = new List<Attendance>(_attendanceSoList.Count);

        DateTime today = DateTime.Today;
        foreach (var attendanceSo in _attendanceSoList)
        {
            if (attendanceSo.StartDate < today)
            {
                continue;
            }

            if (FindById(attendanceSo.ID) != null)
            {
                throw new Exception("중복된 출석 이벤트 검사");
            }
            
            Attendance attendance = new Attendance(attendanceSo.ID, attendanceSo.StartDate, DateTime.Today, 1);
            foreach (var attendanceRewardSO in attendanceSo.AttendanceRewards)
            {
                attendance.AddReward(new AttendanceReward(attendanceRewardSO.CurrencyType, attendanceRewardSO.Amount, false));
            }
        }
        
        
        StartCoroutine(Check_Coroutine());
    }

    private Attendance FindById(string id)
    {
        Attendance attendance = _attendances.Find(x => x.ID == id);
        return attendance;
    }

    public AttendanceDTO GetAttendance(string id)
    {
        Attendance attendance = FindById(id);
        if (attendance == null)
        {
            throw new Exception("Attendance not found");
        }

        return attendance.ToDTO();
    }

    public bool TryRewardClaim(string attendanceID, int index)
    {
        Attendance attendance = FindById(attendanceID);
        if (FindById(attendanceID).TryClaim(index))
        {
            return false;
        }

        if (attendance.TryClaim(index))
        {
            AttendanceRewardDTO  reward = attendance.GetReward(index);

            return true;
            
            CurrencyManager.Instance.Add(reward.CurrencyType, reward.Amount);
            
            OnDataChanged?.Invoke();
        }

        return true;
    }
    
    private IEnumerator Check_Coroutine()
    {
        var hourTimeWait = new WaitForSecondsRealtime(60 * 60);

        while (true)
        {
                    
            DateTime today = DateTime.Today;

            foreach (Attendance attendance in _attendances)
            {
                attendance.Check(today);
            }
        
            OnDataChanged?.Invoke();

            yield return hourTimeWait;
        }
    }
}
