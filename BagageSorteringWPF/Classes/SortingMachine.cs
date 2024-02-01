using BagageSorteringWPF.ViewModel;
using System;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Controls;


namespace BagageSorteringWPF.Classes
{
    public class SortingMachine
    {
        private BufferQueue[] _gates;
        public BufferQueue[] Gates
        {
            get { return _gates; }
            set { _gates = value; }
        }
        private BufferQueue _queue;
        private ListBox _box;
        private Dispatcher _dispatcher;
        private TextViewModel _viewModel;

        private bool _open = true;

        public SortingMachine(BufferQueue queue, BufferQueue[] gates, ListBox box, Dispatcher dispatcher)
        {
            _queue = queue;
            _gates = gates;
            _box = box;
            _dispatcher = dispatcher;

            _viewModel = new TextViewModel();

            _box.DataContext = _viewModel;
        }
        public void Run()
        {
            while (_open)
            {
                Bagage bagage = _queue.Next();
                WriteText($"{bagage.Key} | Gate: {bagage.Gate + 1} | {bagage.Name}");
                _queue.Split(_gates[bagage.Gate]);
                Thread.Sleep(100);
            }
        }
        public void Stop()
        {
            _open = false;
        }
        private void WriteText(string text)
        {
            _dispatcher.Invoke(() =>
            {
                if (_viewModel != null && _viewModel.Lines != null)
                {
                    if (_viewModel.Lines.Count >= 20)
                    {
                        _viewModel.Lines.RemoveAt(0);
                    }
                    _viewModel.Lines.Add(text);
                }
            });
        }
    }
}
