using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Models
{
    public class Player : IEquatable<Player>
    {
        public long id { get; private set; }
        public string Name { get; set; }
        public string QuestionLine1 { get; set; }
        public string QuestionLine2 { get; set; }
        public int Answer { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Player)) return false;
            return Equals((Player)obj);
        }

        public bool Equals(Player other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
