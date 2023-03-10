using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace ULB.RPG.UISystems
{
    public interface IUI
    {
        Canvas canvas { get; }
        event Action OnShow;
        event Action OnHide;

        void Show();
        void Hide();
    }
}

/*public class A
{
    public int num;
    public B b;

    public A(A copy)
    {
        this.num = copy.num;
        // 얕은 복사
        this.b = copy.b;
        
        // 깊은 복사
        this.b = new B();
        this.b.id = copy.b.id;
    }

    public void DeepCopy(A copy)
    {
        this.num = copy.num;
        this.b = new B();
        this.b.id = copy.b.id;
    }
}

public class B
{
    public int id;
}

public  class TestA
{
    void Test()
    {
        A a = new A();
        A a2 = new A();
        a2.DeepCopy(a);
    }
}*/
