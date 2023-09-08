using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces
{
    public interface image : IAttribute
    {
        int getWidth();
        int getHeight();
        void setWidth(int width);
        void setHeight(int height);


    }
 }
