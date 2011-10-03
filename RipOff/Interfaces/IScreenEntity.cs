
namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IScreenEntity
    {
        List<Line> Outline { get; set; }

        void Update();
        void Draw(DrawParams dp);
        bool Expired { get; set; }
        bool DetectCollision(IScreenEntity other);
        void Destroy();
        List<Line> GetPerimeter();
    }
}
