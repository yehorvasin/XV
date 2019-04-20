using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XVAnimationPanel : MonoBehaviour
{
    public XVAnimation anim;
    public Text namePlacholder;
    public Text descriptionPlaceholder;
    public Text nameText;
    public Text descriptionText;
    public Slider slider;
    
    
    private XVAnimationController _animationController;

    private void Start()
    {
        _animationController = GameController.Instance.AnimController;
    }

    public void LinkData(XVAnimation _anim)
    {
        anim = _anim;
        namePlacholder.text = anim.name;
        descriptionPlaceholder.text = anim.description;
        nameText.text = "";
        descriptionText.text = "";
        slider.minValue = -3;
        slider.maxValue = 3;
        slider.value = anim.speed;
    }

    public void OnNameChanged(string newNAme)
    {
        anim.name = newNAme;
    }

    public void OnDescriptionChanged(string newDescritpion)
    {
        anim.description = newDescritpion;
    }

    public void OnSliderValueChaged(Slider slider)
    {
        anim.speed = slider.value;
    }

    public void RemoveAnim()
    {
        _animationController.RemoveAnim(anim);
    }

    public void MoveUp()
    {
        _animationController.MoveUp(anim);
    }
    
    public void MoveDown()
    {
        _animationController.MoveDown(anim);
    }
    
}
