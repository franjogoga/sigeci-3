using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Modelo;
using Vista;
using Controlador;
using DevComponents.DotNetBar;

namespace sigeci_3
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
