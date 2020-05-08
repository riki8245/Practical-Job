using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsCanvasAnimator : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.0f, 0.1f);

    }
    public MS_enemyMove mS_EnemyMove;
    private void OnEnable()
    {
        this.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveTo(this.transform.gameObject, iTween.Hash("z", 4.840001, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(this.transform.gameObject, iTween.Hash("x", 0.1, "y", 0.1, "z", 0.1,"easeType","easeOutSine","time",2f,"oncomplete","moveButtons"));
    }

    // Update is called once per frame
    private void moveButtons()
    {
        iTween.MoveTo(this.transform.gameObject, iTween.Hash("y", this.gameObject.transform.position.y + .8f, "time", 3f, "easeType", "linear","loopType","pingPong"));
        mS_EnemyMove.enabled = true;
    }
}
