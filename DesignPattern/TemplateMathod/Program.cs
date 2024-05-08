using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMathod
{
    class Program
    {
        static void Main(string[] args)
        {
            // Basic
            AbstractClass abstractClass = new ConcreteClass();
            abstractClass.TemplateMethod();
            Console.WriteLine("");

            // Player
            Player player = new Player();
            player.Play(1);
            AdvancedLevel aLevel = new AdvancedLevel();
            player.UpgradeLevel(aLevel);
            player.Play(2);
            SuperLevel sLevel = new SuperLevel();
            player.UpgradeLevel(sLevel);
            player.Play(3);


            Console.ReadLine();
        }
    }
}
