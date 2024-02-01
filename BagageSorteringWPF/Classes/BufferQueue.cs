﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSorteringWPF.Classes
{
    public class BufferQueue
    {
        private Queue<Bagage> _queue = new Queue<Bagage>();
        private readonly object _lock = new object();
        public bool Waiting { get; private set; } = false;
        public int Count { get { return _queue.Count; } }
        public int _limit { get; }
        public string Name { get; set; }
        public BufferQueue(string bufferName, int limit)
        {
            Name = bufferName;
            _limit = limit;
        }
        public void Produce(Bagage bagage)
        {
            lock (_lock)
            {
                while (_queue.Count >= _limit)
                {
                    Monitor.Pulse(_lock);
                    Monitor.Wait(_lock);
                }
                _queue.Enqueue(bagage);
            }
        }
        public void Split(BufferQueue direction)
        {
            lock (_lock)
            {
                while (_queue.Count <= 0)
                {
                    Monitor.Wait(_lock);
                }
                lock (direction._lock)
                {
                    while (direction.Count >= direction._limit)
                    {
                        Monitor.Wait(direction._lock);
                    }
                    Bagage bagage = _queue.Dequeue();
                    direction._queue.Enqueue(bagage);
                    Monitor.Pulse(direction._lock);
                }
                Monitor.Pulse(_lock);
            }
        }
        public Bagage Next()
        {
            lock (_lock)
            {
                while (_queue.Count <= 0)
                {
                    Monitor.Pulse(_lock);
                    Monitor.Wait(_lock);
                }
                Bagage bagage;
                bagage = _queue.Peek();
                return bagage;
            }
        }
        public Bagage Consume()
        {
            lock (_lock)
            {
                while (_queue.Count <= 0)
                {
                    Monitor.Pulse(_lock);
                    Monitor.Wait(_lock);
                }
                Bagage beverage;
                beverage = _queue.Dequeue();
                return beverage;
            }
        }
    }
}
