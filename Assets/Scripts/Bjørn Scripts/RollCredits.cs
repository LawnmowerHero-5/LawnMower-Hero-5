using UnityEngine;

public class RollCredits : MonoBehaviour
{
    [SerializeField] private float scrollSpd = 60f;
    [SerializeField] private float endOfCredits;
    [SerializeField] private float returnTime = 4f;
    private float timer;

    private SceneController _Scene;

    private void Start()
    {
        timer = returnTime;

        _Scene = GetComponent<SceneController>();
    }
    
    private void Update()
    {
        if (transform.localPosition.y <= endOfCredits) transform.localPosition += new Vector3(0f, scrollSpd * Time.deltaTime, 0f);
        else if (timer > 0) timer -= Time.deltaTime;
        else _Scene.LoadScene("MainMenu");
    }
}
