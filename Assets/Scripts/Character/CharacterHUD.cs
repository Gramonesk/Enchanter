using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    [Header("UI Options")]
    [SerializeField] private Slider Player_HP;
    [SerializeField] private Slider Player_MP;
    public Image profil;

    private float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, MaxHP);
            Player_HP.value = hp / MaxHP;
            //Debug.Log(maxhp + "out of " +  hp);
        }
    }
    private float maxhp;
    public float MaxHP
    {
        get { return maxhp; } 
        set { 
            maxhp = value;
            //Player_HP.maxValue = value;
            HP = value;
        } 
    }
    private float maxmp;
    public float MaxMP
    {
        get { return maxmp; }
        set
        {
            maxmp = value;
            //Player_MP.maxValue = value;
            MP = value;
        }
    }
    private float mp;
    public float MP
    {
        get { return mp; }
        set
        {
            mp = Mathf.Clamp(value, 0, MaxMP);
            Player_MP.value = mp / MaxMP;
        }
    }
}
