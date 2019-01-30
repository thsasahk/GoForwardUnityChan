using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /// <summary>
    /// Playerオブジェクト
    /// </summary>
    public GameObject Player;
    /// <summary>
    /// JumpMonsterオブジェクト
    /// </summary>
    public GameObject jumpMonster;
    /// <summary>
    /// StarPrefabオブジェクト
    /// </summary>
    public GameObject star;
    /// <summary>
    /// BossBulletオブジェクト
    /// </summary>
    public GameObject bossBullet;
    /// <summary>
    /// Canvasオブジェクト
    /// </summary>
    private GameObject canvas;
    /// <summary>
    /// Canvasオブジェクトのスクリプト
    /// </summary>
    private UIController uiController;
    /// <summary>
    /// Playerとオブジェクトを結ぶベクトルの角度
    /// </summary>
    private float angle;
    /// <summary>
    /// オブジェクトのアニメーター
    /// </summary>
    private Animator animator;
    /// <summary>
    /// 時間計測用の変数
    /// </summary>
    private float time = 0;
    /// <summary>
    /// 次の行動に移るまでの準備期間
    /// </summary>
    private float coolTime = 0;
    /// <summary>
    /// オブジェクトの体力
    /// </summary>
    public int life;
    /// <summary>
    /// オブジェクト消滅時に生成されるParticleSystem
    /// </summary>
    public GameObject bossParticle;
    /// <summary>
    /// CubeGeneratorオブジェクト
    /// </summary>
    private GameObject cubeGenerator;
    /// <summary>
    /// CubeGeneratorオブジェクトのスクリプト
    /// </summary>
    private CubeGenerator cubeGeneratorScript;
    /// <summary>
    /// オブジェクトのAudioSource
    /// </summary>
    private AudioSource[] audioSource;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.canvas = GameObject.Find("Canvas");
        this.uiController = this.canvas.GetComponent<UIController>();
        this.cubeGenerator = GameObject.Find("CubeGenerator");
        this.cubeGeneratorScript = this.cubeGenerator.GetComponent<CubeGenerator>();
        this.audioSource = GetComponents<AudioSource>();
    }

    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time >= this.coolTime)
        {
            SelectMove();
        }
        //lengthが一定値を超えたら逃げ出す
        if ((this.uiController.length >= 75 && this.uiController.length <= 104)
            || this.uiController.length >= 130)
        {
            Move09();
        }
    }

    void SelectMove()
    {
        if (this.uiController.length < 75)
        {
            int move = Random.Range(1, 7);
            switch (move)
            {
                case 1:
                    Move01();
                    break;

                case 2:
                    Move02();
                    break;

                case 3:
                    Move03();
                    break;

                case 4:
                    Move04();
                    break;

                case 5:
                    Move05();
                    break;

                case 6:
                    Move06();
                    break;
            }
        }
        else
        {
                int move2 = Random.Range(1, 9);
                switch (move2)
                {
                    case 1:
                        Move01();
                        break;

                    case 2:
                        Move02();
                        break;

                    case 3:
                        Move03();
                        break;

                    case 4:
                        Move04();
                        break;

                    case 5:
                        Move05();
                        break;

                    case 6:
                        Move06();
                        break;

                    case 7:
                        Move07();
                        break;

                    case 8:
                        Move08();
                        break;
                 }
         }
     }
     
    /// <summary>
    /// (7.5,-4)に移動してランダムで1～3発の攻撃を行う
    /// </summary>
    void Move01()
    {
        this.time = 0;
        this.coolTime = 4.5f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 7.5f, "y", -4, "time", 3.0f));
        int i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                Invoke("Attack01", 3f);
                break;

            case 2:
                Invoke("Attack01", 3f);
                Invoke("Attack01", 3.5f);
                break;

            case 3:
                Invoke("Attack01", 3f);
                Invoke("Attack01", 3.5f);
                Invoke("Attack01", 4f);
                break;
        }
    }

    /// <summary>
    /// 画面右上に移動し、Playerに向かって1度だけ攻撃する
    /// </summary>
    void Move02()
    {
        this.time = 0;
        this.coolTime = 6.0f;
        //X座標は一定値からランダムで選択、Y座標は2.5で固定
        float posX = Random.Range(3f, 8f);
        iTween.MoveTo(gameObject, iTween.Hash("x", posX, "y", 2.5f, "time", 3.0f));
        //オブジェクトとPlayerを結ぶ角度angleを求める
        //オブジェクトのz角が0の時、spriteは180度の方向を向いているので注意
        float dx = this.Player.transform.position.x - posX;
        float rad = Mathf.Atan2(-6.4f, dx);
        this.angle = rad * Mathf.Rad2Deg - 180;
        iTween.RotateTo(gameObject, iTween.Hash("z", this.angle, "delay", 3.0f));
        Invoke("Attack05", 4.0f);
        //元の角度へ戻る
        iTween.RotateTo(gameObject, iTween.Hash("z", 0, "delay", 4.5f));
    }

    /// <summary>
    /// 徐々に下降しながら5回攻撃
    /// </summary>
    void Move03()
    {
        this.time = 0;
        this.coolTime = 7.5f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 7.5f, "y", 0.0f, "time", 3.0f));
        iTween.MoveTo(gameObject, iTween.Hash("y", -4.0f, "time", 4.0f, "delay", 3.0f, "easeType", "linear"));
        Invoke("Attack01", 3.0f);
        Invoke("Attack01", 4.0f);
        Invoke("Attack01", 5.0f);
        Invoke("Attack01", 6.0f);
        Invoke("Attack01", 7.0f);
    }

    /// <summary>
    /// 徐々に上昇しながら5回攻撃
    /// </summary>
    void Move04()
    {
        this.time = 0;
        this.coolTime = 7.5f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 7.5f, "y", -4.0f, "time", 3.0f));
        iTween.MoveTo(gameObject, iTween.Hash("y", 0.0f, "time", 4.0f, "delay", 3.0f, "easeType", "linear"));
        Invoke("Attack01", 3.0f);
        Invoke("Attack01", 4.0f);
        Invoke("Attack01", 5.0f);
        Invoke("Attack01", 6.0f);
        Invoke("Attack01", 7.0f);
    }

    /// <summary>
    /// (7.5,-4.0)に移動した後予備モーションをして真っすぐ体当たり
    /// </summary>
    void Move05()
    {
        this.time = 0;
        this.coolTime = 10.5f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 7.5f, "y", -4.0f, "time", 3.0f));
        iTween.PunchScale(gameObject, iTween.Hash("x", 2.0f, "y", 2.0f, "time", 3.0f,
            "delay", 3.0f, "easeType", "linear"));
        iTween.MoveTo(gameObject, iTween.Hash("x", -15.0f, "time", 4.0f, "delay", 4.0f, "easeType", "linear"));
        iTween.MoveTo(gameObject, iTween.Hash("y", 8.0f, "delay", 8.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", 8.0f, "delay", 9.0f));
    }

    /// <summary>
    /// (8.7,0.0)に移動した後予備モーションを行い、jumpMonsterオブジェクトを召喚
    /// lengthが75以上なら召喚するオブジェクトにstarオブジェクトが追加される
    /// </summary>
    void Move06()
    {
        this.time = 0;
        this.coolTime = 10.0f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 8.7f, "y", 0.0f, "time", 3.0f));
        iTween.PunchScale(gameObject, iTween.Hash("x", 2.0f, "y", 2.0f, "time", 3.0f,
            "delay", 3.0f, "easeType", "linear"));
        if (this.uiController.length < 75)
        {
            Invoke("Attack02", 5.0f);
        }
        else
        {
            int m = Random.Range(1, 3);
            switch (m)
            {
                case 1:
                    Invoke("Attack02", 5.0f);
                    break;

                case 2:
                    Invoke("Attack03", 5.0f);
                    break;
            }
        }
    }

    /// <summary>
    /// (pos,-2.0)に移動し、y=-4～0の間を移動しながら5回攻撃
    /// </summary>
    void Move07()
    {
        this.time = 0;
        this.coolTime = 7.5f;
        float pos = Random.Range(6.0f, 9.0f);
        iTween.MoveTo(gameObject, iTween.Hash("x", pos, "y", -2.0f, "time", 3.0f));
        pos = Random.Range(-4.0f, 0.0f);
        iTween.MoveTo(gameObject, iTween.Hash("y", pos, "delay", 3.0f, "time", 1.0f, "easeType", "linear"));
        pos = Random.Range(-4.0f, 0.0f);
        iTween.MoveTo(gameObject, iTween.Hash("y", pos, "delay", 4.0f, "time", 1.0f, "easeType", "linear"));
        pos = Random.Range(-4.0f, 0.0f);
        iTween.MoveTo(gameObject, iTween.Hash("y", pos, "delay", 5.0f, "time", 1.0f, "easeType", "linear"));
        pos = Random.Range(-4.0f, 0.0f);
        iTween.MoveTo(gameObject, iTween.Hash("y", pos, "delay", 6.0f, "time", 1.0f, "easeType", "linear"));
        pos = Random.Range(-4.0f, 0.0f);
        Invoke("Attack01", 3.0f);
        Invoke("Attack01", 4.0f);
        Invoke("Attack01", 5.0f);
        Invoke("Attack01", 6.0f);
        Invoke("Attack01", 7.0f);
    }

    /// <summary>
    /// (6.9,-1.0)に移動し、予備動作の後放射状に一気に攻撃する
    /// </summary>
    void Move08()
    {
        this.time = 0;
        this.coolTime = 9.0f;
        iTween.MoveTo(gameObject, iTween.Hash("x", 6.0, "y", -1.0f, "time", 3.0f));
        iTween.ShakePosition(gameObject, iTween.Hash("y", 0.2f, "delay", 3.0f, "time", 4.0f));
        Invoke("Attack04", 7.0f);
    }

    /// <summary>
    /// Boss撃退時にz軸を中心に一回転した後x方向へ逃げる
    /// </summary>
    void Move09()
    {
        this.cubeGeneratorScript.isBoss = false;
        iTween.RotateTo(gameObject, iTween.Hash("z", 720.0f));
        iTween.RotateTo(gameObject, iTween.Hash("y", 180.0f, "delay", 1.0f));
        iTween.MoveTo(gameObject, iTween.Hash("x", 13.0f, "y", 7.0f, "delay", 2.0f));
        Invoke("Destroy", 4.0f);
    }

    /// <summary>
    /// シンプルな攻撃モーション
    /// </summary>
    void Attack01()
    {
        this.animator.SetTrigger("Boss_Attack");
        Instantiate(this.bossBullet, new Vector2(transform.position.x - 2.5f, 
            transform.position.y + 1.0f), Quaternion.identity);
    }

    /// <summary>
    /// jumpMonsterオブジェクトを召喚
    /// </summary>
    void Attack02()
    {
        this.animator.SetTrigger("Boss_Attack");
        Instantiate(this.jumpMonster, new Vector2(6.0f, 0.3f), Quaternion.identity);
    }

    /// <summary>
    /// starオブジェクトを召喚
    /// </summary>
    void Attack03()
    {
        this.animator.SetTrigger("Boss_Attack");
        Instantiate(this.star, new Vector2(8.0f, 6.0f), Quaternion.identity);
    }

    /// <summary>
    /// 放射状に多数の攻撃
    /// </summary>
    void Attack04()
    {
        this.animator.SetTrigger("Boss_Attack");
        //-50から50まで25度づつBulletの角度をずらしながら生成する
        for(int wave = -50; wave <= 50; wave += 25)
        {
            GameObject bullet = Instantiate(this.bossBullet) as GameObject;
            bullet.transform.position = new Vector2(transform.position.x - 2.5f, transform.position.y + 1.0f);
            bullet.transform.Rotate(0, 0, wave);
        }
    }

    /// <summary>
    /// 斜め下に向けて一度攻撃
    /// </summary>
    void Attack05()
    {
        this.animator.SetTrigger("Boss_Attack");
        GameObject bullet = Instantiate(this.bossBullet) as GameObject;
        bullet.transform.position = new Vector2(transform.position.x - 2.0f, transform.position.y);
        bullet.transform.Rotate(0, 0, this.angle);
    }

    /// <summary>
    /// オブジェクトを破棄してbossScoreを加算する
    /// </summary>
    void Destroy()
    {
        this.uiController.bossScore += 100;
        Destroy(gameObject);
    }

    /// <summary>
    /// Bombオブジェクトに接触した際呼び出される、lengthによってlifeが1未満になった時の処理を変化させる
    /// </summary>
    /// <param name="i"></param>
    public void Damage(int i)
    {
        this.audioSource[0].Play();
        //life変数を引数分マイナスさせる
        this.life -= i;
        //life変数が1未満の場合はオブジェクトを破棄する
        if (this.life < 1)
        {
            if (this.uiController.length < 75)
            {
                Move09();
            }
            else
            {
                this.cubeGeneratorScript.isBoss = false;
                Instantiate(this.bossParticle, this.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
