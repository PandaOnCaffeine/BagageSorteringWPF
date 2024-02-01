using System;
using Bogus;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using BagageSorteringWPF.ViewModel;
using System.Windows.Controls;

namespace BagageSorteringWPF.Classes
{
    public class Check_In
    {
        public BufferQueue _queue;
        private ListBox _box;
        private BufferQueue _sorting;
        private int _gateAmount;
        public int GateAmount
        {
            get { return _gateAmount; }
            set { _gateAmount = value; }
        }


        private TextViewModel _viewModel;

        private Faker _nameGen = new Faker();
        private Random _random = new Random();

        private bool _open = true;

        private List<string> _lines = new List<string>();

        private Dispatcher _dispatcher;

        private int testCount = 1;
        public Check_In(BufferQueue queue, ListBox box, BufferQueue sorting, int gateAmount, Dispatcher dispatcher)
        {
            _queue = queue;
            _box = box;
            _sorting = sorting;
            _gateAmount = gateAmount;
            _dispatcher = dispatcher;

            _viewModel = new TextViewModel();

            _box.DataContext = _viewModel;
        }
        public void Run()
        {
            while (_open)
            {
                //WriteText($"Test{testCount}");
                //testCount++;


                int amount = _random.Next(1, 3);
                Person person = new Person(_nameGen, amount);

                TakeBagage(person);
                SendToSorting();
                //Thread.Sleep(_random.Next(1500, 3000));
                Thread.Sleep(300);

            }
        }
        public void Stop()
        {
            _open = false;
        }

        private void SendToSorting()
        {
            int amount = _queue.Count;
            for (int i = 0; i < amount; i++)
            {
                _queue.Split(_sorting);
            }
        }

        private void TakeBagage(Person person)
        {
            int randomNr = _random.Next(0, _gateAmount);
            for (int i = 0; i < person.BagageAmount; i++)
            {
                Bagage bagage = new Bagage(person.Name, randomNr, $"{_queue.Name}");
                _queue.Produce(bagage);
            }
            WriteText($"Bags:{person.BagageAmount}| Gate:{randomNr + 1}| {person.Name}");
        }
        private void WriteText(string text)
        {
            _dispatcher.Invoke(() =>
            {
                if (_viewModel != null && _viewModel.Lines != null)
                {
                    if (_viewModel.Lines.Count >= 5)
                    {
                        _viewModel.Lines.RemoveAt(0);
                    }
                    _viewModel.Lines.Add(text);
                }
            });
        }
    }
}
