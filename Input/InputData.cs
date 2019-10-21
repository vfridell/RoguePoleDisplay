namespace RoguePoleDisplay.InputListeners
{
    public class InputData
    {
        public int LastNumEntered { get; set; }

        public override bool Equals(object obj)
        {
            InputData leapData = obj as InputData;
            if (null == leapData) return false;
            return Equals(leapData);
        }

        public bool Equals(InputData other) => LastNumEntered == other.LastNumEntered;

        public override int GetHashCode() => LastNumEntered.GetHashCode();
    }
}