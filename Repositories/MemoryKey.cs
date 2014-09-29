using RoguePoleDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Repositories
{
    public class MemoryKey : IEquatable<MemoryKey>
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public Player player { get; set; }

        public override bool Equals(object other)
        {
            if (!(other is MemoryKey)) return false;
            return Equals((MemoryKey)other);
        }
        public override int GetHashCode()
        {
            string name = "";
            if (null != player) name = player.Name;
            return (name + line1 + line2).GetHashCode();
        }

        public bool Equals(MemoryKey other)
        {
            string name = "";
            if (null != player) name = player.Name;

            string otherName = "";
            if (null != other.player) otherName = other.player.Name;

            return name == otherName && line1 == other.line1 && line2 == other.line2;
        }
    }
}
