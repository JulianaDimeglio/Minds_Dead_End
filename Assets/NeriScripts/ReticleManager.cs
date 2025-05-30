using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
    public Image reticleImage;

    public Sprite defaultSprite;
    public Sprite interactSprite;
    public Sprite lockedDoorSprite;
    public Sprite lightSwitchSprite; 

    public void SetReticle(Sprite sprite)
    {
        if (reticleImage != null && reticleImage.sprite != sprite)
        {
            reticleImage.sprite = sprite;
        }
    }
}