using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMathod
{
    public class Player
    {
        private PlayerLevel level;

        public Player()
        {
            level = new BeginnerLevel();
            level.ShowLevelMessage();
        }

        public PlayerLevel GetLevel()
        {
            return level;
        }

        public void UpgradeLevel(PlayerLevel level)
        {
            this.level = level;
            level.ShowLevelMessage();
        }

        public void Play(int count)
        {
            level.Go(count);
        }
    }
    public abstract class PlayerLevel
    {
        public abstract void Run();
        public abstract void Jump();
        public abstract void Turn();
        public abstract void ShowLevelMessage();

        public void Go(int count)
        {
            Run();
            for (int i = 0; i < count; i++)
            {
                Jump();
            }
            Turn();
        }
    }

    public class BeginnerLevel : PlayerLevel
    {
        public override void Run()
        {
            Console.WriteLine("천천히 달립니다.");
        }

        public override void Jump()
        {
            Console.WriteLine("Jump 할 줄 모르지롱.");
        }

        public override void Turn()
        {
            Console.WriteLine("Turn 할 줄 모르지롱.");
        }

        public override void ShowLevelMessage()
        {
            Console.WriteLine("초보자 레벨 입니다.");
        }
    }

    public class AdvancedLevel : PlayerLevel
    {
        public override void Run()
        {
            Console.WriteLine("빨리 달립니다.");
        }

        public override void Jump()
        {
            Console.WriteLine("높이 jump 합니다.");
        }

        public override void Turn()
        {
            Console.WriteLine("Turn 할 줄 모르지롱.");
        }

        public override void ShowLevelMessage()
        {
            Console.WriteLine("중급자 레벨 입니다.");
        }
    }

    public class SuperLevel : PlayerLevel
    {
        public override void Run()
        {
            Console.WriteLine("엄청 빨리 달립니다.");
        }

        public override void Jump()
        {
            Console.WriteLine("아주 높이 jump 합니다.");
        }

        public override void Turn()
        {
            Console.WriteLine("한 바퀴 돕니다.");
        }

        public override void ShowLevelMessage()
        {
            Console.WriteLine("고급자 레벨 입니다.");
        }
    }
}
