using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDragLink
{
    public bool Locked { get; set; }
}
public class LinkDisplay : MonoBehaviour
{
    public bool _isActive;
    #region References
    [Header("Resources")]
    [SerializeField] protected UIData _UIData;

    [Space(2)]
    [Header("Sprite Renderer")]
    //[SerializeField] protected SpriteRenderer Link_Colour;
    [SerializeField] protected Image Profile;
    [SerializeField] protected Image Banner;
    [SerializeField] protected SpriteRenderer ProfileSprite;
    [SerializeField] protected SpriteRenderer BannerSprite;
    //[SerializeField] protected SpriteRenderer Nexus;

    [SerializeField] protected ParticleSystem OuterParticle;
    [SerializeField] protected ParticleSystem InnerParticle;

    #region DEBUG_ONLY
    private Link_type Linker;
    private Link_type Nexus;
    #endregion
    protected Color _particlecolor
    {
        set
        {
            var main = OuterParticle.main;
            main.startColor = value;
        }
    }
    #endregion

    public void SetOFF()
    {
        var main = OuterParticle.main;
        var side = InnerParticle.main;
        side.loop = false;
        main.loop = false;
        _isActive = false;
    }
    public void SetON()
    {
        var main = OuterParticle.main;
        var side = InnerParticle.main;
        side.loop = true;
        main.loop = true;
        InnerParticle.Play();
        OuterParticle.Play();
        _isActive = true;
    }
    public void UpdateLink(Link linkdata)
    {
        //Nexus.sprite = _UIData.Nexus[(int)linkdata.Nexus];
        //Profile.sprite = linkdata.user.GetComponent<SpriteRenderer>().sprite;
        if (Banner != null)
        {
            //Profile.sprite = linkdata.user.GetComponent<SpriteRenderer>().sprite;
            Banner.sprite = _UIData.Banner[(int)linkdata.Linker];
            Profile.color = new Color(255, 255, 255, 1);
            Banner.color = new Color(255, 255, 255, 1);
        }
        else if(BannerSprite != null)
        {
            //ProfileSprite.sprite = linkdata.user.GetComponent<SpriteRenderer>().sprite;
            BannerSprite.sprite = _UIData.Banner[(int)linkdata.Linker];
            ProfileSprite.color = new Color(255, 255, 255, 1);
            BannerSprite.color = new Color(255, 255, 255, 1);
        }

        //Link_Colour.color = _UIData.colors[(int)linkdata.Linker];
        _particlecolor = _UIData.colors[(int)linkdata.Linker];
        var nexus = InnerParticle.main;
        nexus.startColor = _UIData.colors[(int)linkdata.Nexus];

        #region DEBUG_ONLY
        Linker = linkdata.Linker;
        Nexus = linkdata.Nexus;
        #endregion
        SetON();
    }
    public void Clear()
    {
        //Nexus.sprite = null;
        if (Banner)
        {
            Profile.color = new Color(0, 0, 0, 0);
            Banner.color = new Color(0, 0, 0, 0);
        }
        else
        {
            ProfileSprite.color = new Color(0, 0, 0, 0);
            BannerSprite.color = new Color(0, 0, 0, 0);
        }

        SetOFF();
    }
}
