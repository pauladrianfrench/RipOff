
namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IEntity
    {
        List<Line> Outline { get; set; }
        MatrixPoint Centre { get; set; }
        void Update();
        void Draw(DrawParams dp);
        bool Expired { get; set; }
        void Destroy();
        List<Line> GetPerimeter();
    }
}
