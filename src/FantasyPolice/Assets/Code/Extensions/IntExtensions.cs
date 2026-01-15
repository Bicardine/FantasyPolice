namespace Extensions
{
    public static class IntExtensions
    {
        private const int DeltaToIndex = -1;
        
        public static int Next(this int number) => ++number;
        public static int Previous(this int number) => --number;
    
        public static int ToIndex(this int number) => number + DeltaToIndex;
        
        public static bool IsLessThanZero(this int number) => number <= 0;
        
        public static bool IsLessOrEqualZero(this int number) => number <= 0;

        public static bool IsGreaterThanZero(this int number) => number > 0;

        public static bool IsZero(this int number) => number == 0;

        public static int Increase(this int number) => number + 1;
        
        public static int Decrease(this int number) => number - 1;
    }
}
