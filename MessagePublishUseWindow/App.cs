using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePublishUseWindow.View;

namespace MessagePublishUseWindow
{
    class App : System.Windows.Application
    {
        [System.STAThread()]
        public static void Main()
        {
            App app = new App();
            View.MainWindow mainWindow = new View.MainWindow();
            app.Run(mainWindow);
        }
    }
}
