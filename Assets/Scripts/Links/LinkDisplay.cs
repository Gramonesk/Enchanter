using System.Collections;
using UnityEngine;
public interface IDragLink
{
    public bool Locked { get; set; }
}
public class LinkDisplay : MonoBehaviour
{
    #region Variables
    [Header("Card")]
    public bool _isActive;
    [SerializeField] protected LinkSO _linkdata;

    [Header("Resources")]
    [SerializeField] protected UIData _UIData;

    [Space(2)]
    [Header("Sprite Renderer")]
    [SerializeField] protected SpriteRenderer Link_Colour;
    [SerializeField] protected SpriteRenderer Profile;
    [SerializeField] protected SpriteRenderer Banner;
    //[SerializeField] protected SpriteRenderer Nexus;

    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected ParticleSystem Nexus_particle;
    protected Color _particlecolor
    {
        set
        {
            var main = particle.main;
            main.startColor = value;
        }
    }
    #endregion

    public void SetOFF()
    {
        var main = particle.main;
        var side = Nexus_particle.main;
        side.loop = false;
        main.loop = false;
        _isActive = false;
    }
    public void SetON()
    {
        var main = particle.main;
        var side = Nexus_particle.main;
        side.loop = true;
        main.loop = true;
        Nexus_particle.Play();
        particle.Play();
        _isActive = true;
    }
    public void UpdateLink(LinkSO linkdata)
    {
        _linkdata = linkdata;

        //Profile.sprite = linkdata.user.GetComponent<SpriteRenderer>().sprite;
        Banner.sprite = _UIData.Banner[(int)linkdata.Linker];
        Link_Colour.color = _UIData.colors[(int)linkdata.Linker];
        //Nexus.sprite = _UIData.Nexus[(int)linkdata.Nexus];
        _particlecolor = _UIData.colors[(int)linkdata.Linker];
        var nexus = Nexus_particle.main;
        nexus.startColor = _UIData.colors[(int)linkdata.Nexus];
        SetON();
    }
    public void Clear()
    {
        //Nexus.sprite = null;
        Profile.sprite = null;
        Banner.sprite = null;
        SetOFF();
    }
}
