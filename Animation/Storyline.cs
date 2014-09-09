using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace DevComponents.DotNetBar.Animation
{
    internal class Storyline : Component
    {
        #region Events

        #endregion

        #region Constructor

        #endregion

        #region Implementation
        private bool _IsDisposed = false;
        /// <summary>
        /// Returns whether Storyline is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return _IsDisposed;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BackgroundWorker worker = _Worker;
                if (worker != null)
                {
                    worker.CancelAsync();
                    worker.Dispose();
                    _Worker = null;
                }
            }
            _IsDisposed = true;
            base.Dispose(disposing);
        }

        private List<Animation> _Animations = new List<Animation>();
        /// <summary>
        /// Gets the list of animations to run using this storyline.
        /// </summary>
        public List<Animation> Animations
        {
            get
            {
                return _Animations;
            }
        }

        private BackgroundWorker _Worker = null;
        /// <summary>
        /// Runs all animations from Animations list.
        /// </summary>
        public void Run()
        {
            if (_Worker != null)
                throw new InvalidOperationException("Storyline is already running animations");

            _Worker = new BackgroundWorker();
            _Worker.WorkerReportsProgress = true;
            _Worker.WorkerSupportsCancellation = true;
            _Worker.DoWork += new DoWorkEventHandler(WorkerDoWork);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _Worker.RunWorkerAsync();
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_RepeatStoryTimes > 0)
            {
                if (_Worker == null)
                {
                    _RepeatStoryTimes = 0;
                    return;
                }

                _RepeatStoryTimes--;
                _Worker.RunWorkerAsync();
            }
            else
            {
                if (_Worker != null)
                    _Worker.Dispose();
                _Worker = null;
                if (_AutoDispose && !_IsDisposed)
                    this.Dispose();
            }
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            int count = _Animations.Count;
            for (int i = 0; i < count; i++)
            {
                if (e.Cancel) return;
                _Animations[i].Start();
                while (!_Animations[i].IsCompleted)
                {
                    Thread.Sleep(50);
                    if (e.Cancel)
                    {
                        _Animations[i].Stop();
                        return;
                    }
                }
            }
        }

        private int _RepeatStoryTimes = 0;
        /// <summary>
        /// Gets or sets number of times storyline is repeated. Default value is 0 which indicates that storyline is run only once meaning not repeated.
        /// </summary>
        public int RepeatStoryTimes
        {
            get { return _RepeatStoryTimes; }
            set { _RepeatStoryTimes = value; }
        }

        private bool _AutoDispose = false;
        /// <summary>
        /// Gets or sets whether storyline is auto-disposed when finished.
        /// </summary>
        public bool AutoDispose
        {
            get { return _AutoDispose; }
            set
            {
                _AutoDispose = value;
            }
        }
        #endregion

    }
}
