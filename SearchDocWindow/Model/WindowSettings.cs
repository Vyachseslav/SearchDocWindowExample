using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchDocWindow.Model
{
    public class WindowSettings
    {
        private double _width = 600; // Минимальная ширина.
        /// <summary>
        /// Ширина окна.
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double _height = 400; // Минимальная высота.
        /// <summary>
        /// Высота окна.
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private double _top;
        /// <summary>
        /// Отступ сверху.
        /// </summary>
        public double Top
        {
            get { return _top; }
            set { _top = value; }
        }

        private double _left;
        /// <summary>
        /// Отступ слева.
        /// </summary>
        public double Left
        {
            get { return _left; }
            set { _left = value; }
        }

        private WindowState _windowState = WindowState.Normal;
        /// <summary>
        /// Состояние окна (свернуто, развернуто, нормальное).
        /// </summary>
        public WindowState WindowState
        {
            get { return _windowState; }
            set { _windowState = value; }
        }
    }
}
