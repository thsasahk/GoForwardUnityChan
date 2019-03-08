using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using TMPro;

public class RankingController : MonoBehaviour
{
    /// <summary>
    /// データストアのランキングクラスを検索するための変数
    /// </summary>
    NCMBQuery<NCMBObject> ranking;
    /// <summary>
    /// オブジェクトの子のTextMeshProの集まり
    /// </summary>
    [SerializeField] private TextMeshProUGUI[] tMPro;
    /// <summary>
    /// 削除するオブジェクトを入れる
    /// </summary>
    private NCMBObject deleteObj;
    /// <summary>
    /// スコアの順位
    /// </summary>
    private int rank;
    /// <summary>
    /// 記録を残したプレイヤーの名前
    /// </summary>
    private string playerName;
    /// <summary>
    /// 記録されているスコア
    /// </summary>
    private int score;

    void Start()
    {
        //検索対象にRankingオブジェクトを指定する
        this.ranking = new NCMBQuery<NCMBObject>("Ranking");
        //scoreクラスの降順に並び替える
        this.ranking.OrderByDescending("score");
        this.deleteObj = new NCMBObject("Ranking");

        //並び替えられたRankingオブジェクトを順にrankListに格納する
        this.ranking.FindAsync((List<NCMBObject> rankList, NCMBException e) =>
        {
            //エラーが出たらreturn
            if(e != null)
            {
                return;
            }

            for(int i = 0; i < rankList.Count; i++)
            {
                //21番目以降の記録はNCMBから削除する
                if (i >= 20)
                {
                    this.deleteObj.ObjectId = rankList[i].ObjectId;
                    deleteObj.DeleteAsync();
                    /*
                    deleteObj.DeleteAsync((NCMBException e2) =>
                    {               
                        if (e2 != null)
                        {
                            //エラー処理
                            return;
                        }
                        
                    });
                    */
                }
                else
                {
                    //TextMeshProに表示する値をrankListを参照して指定
                    this.rank = i + 1;
                    this.playerName = System.Convert.ToString(rankList[i]["name"]);
                    this.score = System.Convert.ToInt32(rankList[i]["score"]);
                    //TextMeshProに記載
                    this.tMPro[i + 2].GetComponent<TextMeshProUGUI>().text = this.rank.ToString() + "位:" +
                        this.playerName + " " + this.score.ToString() + "pts";
                }
            }
        });
    }
}
