using System.Drawing;
using System.Windows.Media;

namespace Spotlight.Models
{
    public class Item
    {
        public Item()
        {

        }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Path { get; set; }

        public ImageSource Icon { get; set; }
    }
}
