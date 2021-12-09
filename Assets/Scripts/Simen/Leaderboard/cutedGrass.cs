using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class cutedGrass : MonoBehaviour
{
        [FormerlySerializedAs("tex")] public RenderTexture renderTexture;
        [FormerlySerializedAs("myTexture")] public Texture2D newTexture2D;
    
        public float grassScore;
        public transformVariable trans;
        private bool IcantBelieveitsNotTrue;
        private Timer _timer;
        private bool _canScore;
        private scoreManager _scoreManager;
        
        
        private void Awake()
        {
            newTexture2D = ToTexture2D(renderTexture);
            _timer = GetComponent<Timer>();
            _scoreManager = GetComponent<scoreManager>();
            _canScore = true;
        }
    
        private void Start()
        {
            newTexture2D = ToTexture2D(renderTexture);
        }
        
        Texture2D ToTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }
    
        float ReadTexture2DPixels(Texture2D tex)
        {
            var totalPixels = tex.width * tex.height;
            var colouredPixels = 0f;
            
            for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                if (tex.GetPixel(x, y).Equals(new Color(70, 70, 70, 1)))
                    colouredPixels++;
    
            // Return new Vector2(colored, totoal);
            return colouredPixels / totalPixels * 100f;
        }
    
        private void Update()
        {
            if (!IcantBelieveitsNotTrue)
            {
                newTexture2D = ToTexture2D(renderTexture);
                IcantBelieveitsNotTrue = true;
            }
            
            print(ReadTexture2DPixels(newTexture2D));

            if (!_timer.timerIsRunning && _canScore)
            {
                print("I happened");
                newTexture2D = ToTexture2D(renderTexture);
                grassScore = ReadTexture2DPixels(newTexture2D);
                trans.score2 += grassScore * _scoreManager.grassPoints;
                _canScore = false;
            }
        }
}