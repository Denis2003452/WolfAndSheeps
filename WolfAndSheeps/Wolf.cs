using System.Xml.Linq;

namespace WolfAndSheeps;
internal class Wolf : Animal
{
    public Wolf(int x, int y) : base(x, y)
    {
        Name = "Wolf";
    }
}
