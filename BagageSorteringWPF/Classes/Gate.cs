using BagageSorteringWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BagageSorteringWPF.Classes
{
    public class Gate
    {
        private BufferQueue _queue;
        private ListBox _box;
        private Dispatcher _dispatcher;
        private TextViewModel _viewModel;
        
        private bool _open = true;

        public Gate(BufferQueue queue, ListBox box,Dispatcher dispatcher)
        {
            _queue = queue;
            _box = box;
            _dispatcher = dispatcher;

            _viewModel = new TextViewModel();

            _box.DataContext = _viewModel;
        }
        public void open()
        {
            while (_open)
            {
                Bagage bagage = _queue.Consume();
                WriteText($"Gate: {bagage.Gate + 1} | BagageNr: {bagage.Key} | {bagage.Name}");
                Thread.Sleep(100);
            }
        }
        public void close()
        {
            _open = false;
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
