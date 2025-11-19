using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimator : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] sprites;
    public float frameRate = 0.1f;
    public bool loop = true;
    
    private int currentFrame = 0;
    public float timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Length == 0 || targetImage == null)
            return;
        
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame++;

            if (currentFrame >= sprites.Length)
            {
                if (loop)
                    currentFrame = 0;
                else
                {
                    currentFrame = sprites.Length - 1;
                    enabled  = false;
                }
            }
            
            targetImage.sprite = sprites[currentFrame];
        }
    }
}
