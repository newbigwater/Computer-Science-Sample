using System;

namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // 복합 객체 생성
            var composite1 = new Composite("Composite 1");
            // 리프 객체 생성 및 복합 객체에 추가
            var leaf1 = new Leaf("Leaf 1");
            var leaf2 = new Leaf("Leaf 2");
            composite1.Add(leaf1);
            composite1.Add(leaf2);

            // 또 다른 복합 객체 생성
            var composite2 = new Composite("Composite 2");
            // 리프 객체 생성 및 복합 객체에 추가
            var leaf3 = new Leaf("Leaf 3");
            var leaf4 = new Leaf("Leaf 4");
            var leaf5 = new Leaf("Leaf 5");
            composite2.Add(leaf3);
            composite2.Add(leaf4);
            composite2.Add(leaf5);

            // 복합 객체 안에 다른 복합 객체 추가
            composite1.Add(composite2);

            // 전체 트리 구조 출력
            Console.WriteLine("** Composite Pattern Tree **");
            composite1.Operation(0);

            Console.ReadLine();
        }
    }
}
