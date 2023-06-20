// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

public interface IFactoryAndInterfaceCheck
{
    //インターフェースのメソッドやプロパティ定義
}
public class FactoryAndInterfaceCheck : IFactoryAndInterfaceCheck
{

    private FactoryAndInterfaceCheck()
    {
        //プライベートにコンストラクタを実装　外部からはこのクラスをコンストラクタを使用してインスタンス生成できない
    }


    /// <summary>
    /// ファクトリメソッド
    /// </summary>
    /// <returns>IMyInterface</returns>
    public static IFactoryAndInterfaceCheck CreateInstance()
    {
        //インターフェース型にインスタンスしたクラスを代入
        IFactoryAndInterfaceCheck myInterface = new FactoryAndInterfaceCheck();
        //インターフェースを返す
        return myInterface;
    }
}


public class FactoryMainProgram : MonoBehaviour
{
    //インターフェース型の変数
    IFactoryAndInterfaceCheck myInterface;

    //クラス型の変数
    FactoryAndInterfaceCheck factoryAndInterfaceCheck;
    private void Start()
    {
        //ファクトリメソッドを利用してインスタンスする
        myInterface = FactoryAndInterfaceCheck.CreateInstance();

        //通常のインスタンスはできない　コンストラクタがPrivateのため
        //myInterface = new FactoryAndInterfaceCheck();


        //またクラス型の変数には代入できない
        //factoryAndInterfaceCheck = new FactoryAndInterfaceCheck();

        //factoryAndInterfaceCheck = FactoryAndInterfaceCheck.CreateInstance();
        

        /*ファクトリメソッド　ファクトリクラスとは
         * オブジェクトの生成を専門に行うメソッド
         * 通常はクラス内に静的メソッドとして実装される
         * クラスのコンストラクタとは異なり、より柔軟なオブジェクト生成方法を提供することができる。
         * 利点
         * ①インスタンスのロジックを隠蔽すること。
         *  ファクトリメソッドを使用することで、オブジェクトの生成方法や依存関係の詳細を隠蔽することができる
         *  クライアントは単に、ファクトリメソッドを呼び出すだけで、内部の生成ロジックを意識する必要はない。
         *  
         * ②インスタンスのカスタマイズ
         * ファクトリメソッドは生成されるオブジェクトをカスタマイズするための柔軟性を提供する。
         * パラメータを受け取り、生成されるオブジェクトの初期化や設定を行うことができる。
         * 
         * ③インスタンスの再利用
         * ファクトリメソッドは同じパラメータを使用して複数回呼び出すことが出来る。
         * これにより、オブジェクトの再利用を容易にすることができる。
         * 
         * ④インターフェースによる抽象化
         * ファクトリメソッドは、抽象インターフェースを介してオブジェクトの生成を行うため、
         * クライアントコードが具体的なクラスに依存しないようにすることができる。
         * そのため、コードの柔軟性、保守性が向上する
         * 
         * カプセル化
         * 
         */
    }

}
