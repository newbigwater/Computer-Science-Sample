using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class ConcretePrototype : IPrototype
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 복제 생성 메서드
        public IPrototype Clone()
        {
            // 객체를 복제하여 새로운 인스턴스를 반환
            return new ConcretePrototype { Id = this.Id, Name = this.Name };
        }
    }
}
