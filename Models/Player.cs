using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Models
{
    public class Player : IEquatable<Player>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string QuestionLine1 { get; set; }
        public virtual string QuestionLine2 { get; set; }
        public virtual int Answer { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Player)) return false;
            return Equals((Player)obj);
        }

        public virtual bool Equals(Player other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
