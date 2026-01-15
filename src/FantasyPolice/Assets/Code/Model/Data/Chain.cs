namespace Model.Data
{
    public class Chain<T> where T : IChainable<T>
    {
        private T _first;
        private T _last;
    
        public T First() => _first;
    
        public Chain<T> SetNext(T step)
        {
            if (_first == null)
            {
                _first = step;
                _last = step;
            
                return this;
            }
        
            _last.SetNext(step);
            _last = step;
        
            return this;
        }
    }
}