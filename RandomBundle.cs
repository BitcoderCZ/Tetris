using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class RandomBundle<T>
    {
        private T[] array;
        public readonly Random rng;
        private Func<T, T> cloneFunc;
        private int index;
        private bool autoSchuffle;

        public RandomBundle(T[] _array, Func<T, T> _cloneFunc, int seed, bool _autoSchuffle = true)
        {
            array = _array;
            cloneFunc = _cloneFunc;
            rng = new Random(seed);

            index = 0;
            autoSchuffle = _autoSchuffle;

            if (autoSchuffle)
                Schuffle();
        }

        public T Next()
        {
            T item = array[index];
            index++;
            if (index >= array.Length) {
                index = 0;
                if (autoSchuffle)
                    Schuffle();
            }
            return item;
        }

        public void Schuffle()
        {
            for (int i = 0; i < array.Length * 4; i++) {
                int from = rng.Next(0, array.Length);
                int to = rng.Next(0, array.Length);

                T item = cloneFunc(array[to]);
                array[to] = array[from];
                array[from] = item;
            }
        }
    }
}
