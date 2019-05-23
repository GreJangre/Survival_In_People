using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField] private int hp;
    private int currentHp;

    // 스태미나
    [SerializeField] private int sp;
    private int currentSp;

    // 스태미나 증가량
    [SerializeField] private int spIncreaseSpeed;
    
    // 스태미나 재회복 딜레이
    [SerializeField] private int spRechargeTime;
    private int currentSpRechargeTime;
    
    // 스태미나 감소 여부
    private bool spUsed;
    
    // 방어력
    [SerializeField] private int dp;
    private int currentDp;
    
    // 필요한 이미지
    [SerializeField] private Image[] images_Gauge;

    private const int HP = 0, DP = 1, SP = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
    }

    // Update is called once per frame
    void Update()
    {
        SpRechargeTime();
        SpRecover();
        GaugeUpdate();
    }

    private void SpRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }

    private void SpRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float) currentHp / hp;
        images_Gauge[SP].fillAmount = (float) currentSp / sp;
        images_Gauge[DP].fillAmount = (float) currentDp / dp;
    }

    public void IncreaseHp(int count)
    {
        if (currentHp + count < hp)
            currentHp += count;
        else
            currentHp = hp;
    }

    public void DecreaseHp(int count)
    {
        if (currentDp > 0)
        {
            DecreaseDp(count);
            return;
        }
        currentHp -= count;
        
        if (currentHp <= 0)
            Debug.Log("체력이 없어요.");
    }
    
    public void IncreaseDp(int count)
    {
        if (currentDp + count < dp)
            currentDp += count;
        else
            currentDp = dp;
    }

    public void DecreaseDp(int count)
    {
        currentDp -= count;
        
        if (currentDp <= 0)
            Debug.Log("방어력이 없어요.");
    }
    
    public void DecreaseStamina(int count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - count > 0)
            currentSp -= count;
        else
            currentSp = 0;
    }

    public int GetCurrentSp()
    {
        return currentSp;
    }
}